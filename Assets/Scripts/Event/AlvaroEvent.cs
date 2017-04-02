using System.Collections;
using UnityEngine;

public class AlvaroEvent : RandomEvent {

	public float Delay = 3.0f;

	public override void Fire() {
		StartCoroutine ("DestroyAllIngredients");
	}
		
	private IEnumerator DestroyAllIngredients() {
		yield return new WaitForSeconds (Delay);

		var players = GameObject.FindObjectsOfType<PlayerPickup> ();

		foreach (var player in players) {
			player.PutDownObject ();
		}


		var boxes = GameObject.FindObjectsOfType<Box> ();
        foreach (var b in boxes) {
            if (!b.CanBePicked)
            {
                b.EmptyBox();
            }
        }

		var ingredients = GameObject.FindObjectsOfType<Ingredient> ();

		foreach (var ingredient in ingredients) {
            GameObject.Destroy(ingredient.gameObject, 0.5f);
		}


	}
}
