using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

    public string[] Announcements;
    public Text announcement;
    private float _currentTime = 0;
    private int currentAnnouncement = 0;
    private Quaternion originalRotation;
    private int _currentSkipFrames = 0;
    private int _skipFrames = 4;

    // Use this for initialization
    void Start () {
        Time.timeScale = 0;
        originalRotation = announcement.transform.rotation;
    }

    private void Update()
    {
        if (currentAnnouncement == Announcements.Length) {
            announcement.text = "";
            Time.timeScale = 1;
            this.enabled = false;
            return;
        }

        if (_currentSkipFrames++ > _skipFrames)
        {
            _currentSkipFrames = 0;
        }

        _currentTime += Time.unscaledDeltaTime;

        announcement.transform.rotation = Quaternion.Euler(0, 0, originalRotation.eulerAngles.z + UnityEngine.Random.Range(0, 13));
        
        if (_currentTime > 1)
        {
            _currentTime = 0;
            Next();
        }
    }

    private void Next()
    {
        announcement.text = Announcements[currentAnnouncement++];
    }
}
