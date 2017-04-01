using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlvaroEvent : RandomEvent {

	public float Delay = 3.0f;

	public string Name    = "Alvaro";
	public string Avatar  = "alvaro.jpg";
	public string Message = "Alvaro deployed something. #_CRITICAL!!";

	public override void Fire() {
		Debug.Log (string.Format("Firing {0}!", Name));

		StartCoroutine ("DestroyAllIngredients");
	}
		
	private IEnumerator DestroyAllIngredients() {
		yield return new WaitForSeconds (Delay);

		var players = GameObject.FindObjectsOfType<PlayerPickup> ();
		players[0].PutDownObject();
		players[1].PutDownObject();
		players[2].PutDownObject();
		players[3].PutDownObject();

		foreach (var player in players) {
			player.PutDownObject ();
		}

		var ingredients = GameObject.FindObjectsOfType<Ingredient> ();

		foreach (var ingredient in ingredients) {
			GameObject.Destroy (ingredient);
		}
	}
}
