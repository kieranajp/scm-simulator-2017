using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class WarnarEvent : RandomEvent {

	public float Duration = 5.0f;
	public float Penalty  = -3.0f;

	public override void Fire() {
		ChangePlayersSpeed (Penalty);
        GetComponent<AudioSource>().Play();
		var players = GameObject.FindObjectsOfType<PlayerMovement> ();

		foreach (PlayerMovement player in players) {
            player.GetComponent<SpriteRenderer>().color = Color.blue;
		}

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

		var players = GameObject.FindObjectsOfType<PlayerMovement> ();

		foreach (PlayerMovement player in players) {
            player.GetComponent<SpriteRenderer>().color = Color.white;
		}

		ChangePlayersSpeed (Penalty * -1);
	}
}
