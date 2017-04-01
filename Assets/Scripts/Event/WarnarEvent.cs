using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarnarEvent : RandomEvent {

	public float Duration = 5.0f;
	public float Penalty = -2.0f;

	public override string Name {
		get {
			return "Warnar";
		}
	}

	public override string Avatar {
		get {
			return "warnar.jpg";
		}
	}

	public override string Message {
		get {
			return string.Format ("{0} spilled his drink!", Name);
		}
	}

	public override void Fire() {
		Debug.Log (string.Format("Firing {0}!", Name));

		ChangePlayersSpeed (Penalty);

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

		ChangePlayersSpeed (Penalty * -1);
	}
}
