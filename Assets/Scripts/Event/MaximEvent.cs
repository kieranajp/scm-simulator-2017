using System.Collections;
using Player;
using UnityEngine;

public class MaximEvent : RandomEvent {

	public float Boost    = 3.0f;
	public float Duration = 5.0f;

	public override void Fire() {
		ChangePlayersSpeed (Boost);
        ResetCollision(true);
		StartCoroutine ("ResetSpeed");
	}

	private void ChangePlayersSpeed(float value) {
		var players = GameObject.FindObjectsOfType<PlayerMovement> ();

		foreach (PlayerMovement player in players) {
			player.Speed += value;
		}
	}

    private void ResetCollision(bool ignore)
    {
		var players = GameObject.FindObjectsOfType<PlayerMovement> ();

        for (var i = 0; i < players.Length; i++)
        {
            for (var j = 0; j < players.Length; j++)
            {
                Physics2D.IgnoreCollision(players[i].GetComponent<Collider2D>(), players[j].GetComponent<Collider2D>(), ignore);
            }
        }

    }

	private IEnumerator ResetSpeed() {
		yield return new WaitForSeconds (Duration);

        ResetCollision(false);

		ChangePlayersSpeed (Boost * -1);
	}
}
