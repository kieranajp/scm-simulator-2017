using System;
using UnityEngine;

public class PlayerPickup : PlayerBehaviour {

    Ingredient pickedObject;
    Ingredient candidate;

    private bool hasObject = false;

    private void Update()
    {
        if (PlayerInput.GetButton("A", Player))
        {
            if (pickedObject != null) {
                PutDownObject(pickedObject);
            } else {
                PickUpObject(candidate);
            }
        }
    }

    private void PutDownObject(Ingredient pickedObject)
    {
        throw new NotImplementedException();
    }

    private void PickUpObject(Ingredient candidate)
    {
        if (candidate != null) {
            pickedObject = candidate;
            hasObject = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ingredient>() != null)
        {
            candidate = collision.GetComponent<Ingredient>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        candidate = null;
    }
}
