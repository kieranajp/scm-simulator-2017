using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarnarEvent : RandomEvent {

	public string Name     = "Warnar";
	public string Avatar   = "warnar.jpg";
	public string Message  = "Warnar spilled his drink again...";

	public float Duration = 5.0f;
	public float Penalty  = -3.0f;

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
