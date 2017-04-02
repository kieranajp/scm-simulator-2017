using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class RandomEvent : MonoBehaviour {

    public Sprite Avatar;
    public string Message;

	public abstract void Fire ();
}
