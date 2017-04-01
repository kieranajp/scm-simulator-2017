using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text text;
    public Image image;
    public float BaseFlash = 1;

    public Color ShitOKColor = new Color(0, 1, 0);
    public Color ShitNotOKColor = new Color(1, 0, 0);
    public Color ShitColor = new Color(1, 1, 1);

    public Color color = new Color(1, 1, 1);

    private int prevScore;
    private float flash = 0;

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
            flash = BaseFlash;
            color = ShitNotOKColor;
            if (plus)
            {
                color = ShitOKColor;
            }
        }

        if(flash <= 0)
        {
            flash = 0;
            image.color = ShitColor;
        }
        else
        { 
            var f = Mathf.PingPong(flash * 10, 1);
            var c = color;
            c.a = f;
            image.color = color;
            flash -= Time.deltaTime;
        }
    }
}
