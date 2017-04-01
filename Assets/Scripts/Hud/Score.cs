using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text text;
    public Image image;
    public float BaseFlash = 1;

    public Color Color = new Color(1, 1, 1);

    private int prevScore;
    private float flashDuration = 0;
    private bool positive = true;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        int newScore = FindObjectOfType<BoxManager>().Score;

        bool change = false;
        bool plus = false;
        if (newScore != prevScore)
        {
            change = true;
        }
        if (newScore > prevScore)
        {
            plus = true;
        }
        prevScore = newScore;

        
        if (change)
        {
            text.text = prevScore.ToString();
            flashDuration = BaseFlash;
            positive = plus;
        }

        if (flashDuration <= 0)
        {
            flashDuration = 0;
            image.color = Color;
        }
        else
        { 
            var f = Mathf.PingPong(Time.time * 10, 1);
            if (positive)
            {
                image.color = new Color(f, 1, f);
            }
            else
            {
                image.color = new Color(1, f, f);
            }

            flashDuration -= Time.deltaTime;
        }
    }
}