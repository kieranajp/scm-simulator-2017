using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FedeEvent : RandomEvent {

	public override void Fire() {
		var players = GameObject.FindObjectsOfType<PlayerMovement> ();

		Vector3 newPosition = players [players.Length - 1].transform.position;

		foreach (var player in players) {
			Vector3 oldPosition = player.transform.position;

			player.transform.position = newPosition;

			newPosition = oldPosition;
		}
	}
}
