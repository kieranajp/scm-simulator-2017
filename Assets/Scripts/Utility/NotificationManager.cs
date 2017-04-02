using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {

    public Image Avatar;
    public Text Title;

    public RandomEvent[] Events;
    private bool _wasFired;
    private float _duration = 4f;
    private float _time = 0;
    private RectTransform self;

    public void OnEventFired(RandomEvent e) {
        self = GetComponent<RectTransform>();
        Title.text = e.Message;
        Avatar.sprite = e.Avatar;
        StopAllCoroutines();
        StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        while (self.localScale.y < 1)
        {
            self.localScale = new Vector3(1, Mathf.Lerp(self.localScale.y, 1.1f, Time.deltaTime * 3), 1);
            yield return null;
        }

        self.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(_duration);
        yield return Hide();
    }

    private IEnumerator Hide()
    {

        while (self.localScale.y > 0)
        {
            yield return null;
            self.localScale = new Vector3(1, Mathf.Lerp(self.localScale.y, -0.1f, Time.deltaTime * 3), 1);
        }

        self.localScale = new Vector3(1, 0, 1);
    }

}