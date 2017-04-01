using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour {

    public GameObject Box;

    public float SpawnDelay = 3f;
    public float delay = 0;

    private Box currentBox;

    // Use this for initialization
    void Start () {
        currentBox = Instantiate(Box, transform.position, Quaternion.identity).GetComponent<Box>();
    }
	
	// Update is called once per frame
	void Update () {
        if (currentBox == null || currentBox.HasBeenPicked)
        {
            delay += Time.deltaTime;
        }

		if (delay > SpawnDelay) {
            currentBox = Instantiate(Box, transform.position, Quaternion.identity).GetComponent<Box>();
            delay = 0f;
        }
	}
}
