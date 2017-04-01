using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEvent : RandomEvent {

	public float Extension = 5.0f;

	public string Name     = "Rocket";
	public string Avatar   = "rocket.jpg";
	public string Message  = "Another round of seed funding has been completed! Time extended!";

	public override void Fire() {
		Debug.Log (string.Format("Firing {0}!", Name));

		var timer = GameObject.FindObjectOfType<Timer> ();

		timer.AddTime (Extension);
	}
}
