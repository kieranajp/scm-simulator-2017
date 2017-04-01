using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

	// Destroys what ever gameobject collides with it
	void OnTriggerEnter2D (Collider2D col) {
		Debug.Log("There has been a collision");
		Destroy(col.gameObject);
	}
}
