using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public float RoundTime = 45;
    public float TimeLeft = 0;
    public float CriticalTime = 10;

    public Text text;
    public Image image;

    private bool _isDone = false;

    // Use this for initialization
    void Start () {
        TimeLeft = RoundTime;
	}

	// Update is called once per frame
	void Update () {
        if (_isDone) { return; }

        TimeLeft -= Time.deltaTime;
        text.text = GenerateTime(TimeLeft);
        if (TimeLeft < 0)
        {
            _isDone = true;
            gameObject.BroadcastMessage("OnTimerExpire", SendMessageOptions.DontRequireReceiver);
            return;
        }

        if (TimeLeft < CriticalTime)
        {
            var flash = Mathf.PingPong(Time.time * 10, 1);
            image.color = new Color(1, flash, flash);
        }
    }

    public void AddTime(float time) {
        TimeLeft += time;
    }

    private void Reset()
    {
        _isDone = false;
        TimeLeft = RoundTime;
    }

    private string GenerateTime(float timeLeft)
    {
        var minutes = (int)(timeLeft / 60);
        var seconds = 0;
        if (minutes != 0) {
            seconds = Mathf.CeilToInt(timeLeft - minutes * 60);
        } else {
            seconds = Mathf.CeilToInt(timeLeft);
        }
        var str = string.Format(minutes.ToString().PadLeft(2, '0') + ":" + seconds.ToString().PadLeft(2, '0'));
        return str;
    }
}
