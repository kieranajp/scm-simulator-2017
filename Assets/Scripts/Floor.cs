using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floor : MonoBehaviour {

    public RawImage floorTexture;

    public float Duration = 10;
    public float flashDuration = 0;

    private float speed = 1;
    private Color basicColor;

    // Use this for initialization
    void Start () {
        basicColor = floorTexture.color;
        flashDuration = Duration;
	}
	
	// Update is called once per frame
	void Update () {
		if(flashDuration >= 0)
        {
            float r = Mathf.PingPong(Time.time * (speed + 1), 1);
            float g = Mathf.PingPong(Time.time * (speed + 2), 1);
            float b = Mathf.PingPong(Time.time * (speed + 3), 1);
            floorTexture.color = new Color(r, g, b);
            flashDuration -= Time.deltaTime;
        }
        else
        {
            floorTexture.color = basicColor;
        }
	}

    void GoCrazy (float duration, float s)
    {
        speed = s;
        flashDuration = duration;
    }
}
