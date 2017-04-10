using Pickable;
using UnityEngine;

public class Shredder : MonoBehaviour {

	// Destroys what ever gameobject collides with it
	void OnTriggerEnter2D (Collider2D col) {
	    if (col.GetComponent<Ingredient>() != null)
	    {
	        col.GetComponent<Ingredient>().Remove();
	    }
	}
}
