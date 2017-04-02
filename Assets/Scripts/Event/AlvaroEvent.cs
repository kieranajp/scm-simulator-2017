using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlvaroEvent : RandomEvent {

	public string Name    = "Alvaro";
	public string Avatar  = "alvaro.jpg";
	public string Message = "Alvaro deployed something. #_CRITICAL!!";

	public float Delay = 3.0f;

	public override void Fire() {
		Debug.Log (string.Format("Firing {0}!", Name));

		StartCoroutine ("DestroyAllIngredients");
	}
		
	private IEnumerator DestroyAllIngredients() {
		yield return new WaitForSeconds (Delay);

		var players = GameObject.FindObjectsOfType<PlayerPickup> ();

		foreach (var player in players) {
			player.PutDownObject ();
		}

		var ingredients = GameObject.FindObjectsOfType<Ingredient> ();

		foreach (var ingredient in ingredients) {
			GameObject.Destroy(ingredient.gameObject, 0.5f);
		}
	}
}
