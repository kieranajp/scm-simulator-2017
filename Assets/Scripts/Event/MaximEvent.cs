using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaximEvent : RandomEvent {

	public float Boost = 2.0f;
	public float Duration = 5.0f;

	public override string Name {
		get {
			return "Maxim";
		}
	}

	public override string Avatar {
		get {
			return "maxim.jpg";
		}
	}

	public override string Message {
		get {
			return "01110100 01100011 01110000 01100100 01110101 01101101 01110000";
		}
	}

	public override void Fire() {
		Debug.Log (string.Format("Firing {0}!", Name));

		ChangePlayersSpeed (Boost);

		StartCoroutine ("ResetSpeed");
	}

	private void ChangePlayersSpeed(float value) {
		var players = GameObject.FindObjectsOfType<PlayerMovement> ();

		foreach (PlayerMovement player in players) {
			player.Speed += value;
		}
	}

	private IEnumerator ResetSpeed() {
		yield return new WaitForSeconds (Duration);

		ChangePlayersSpeed (Boost * -1);
	}
}
