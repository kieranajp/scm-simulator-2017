using UnityEngine;

public class PlayerPickup : PlayerBehaviour {

    Ingredient pickedObject;
    Ingredient candidate;

    private bool hasObject = false;

    private void Update()
    {
        if (PlayerInput.GetButtonDown("A", Player))
        {
            if (pickedObject != null) {
                PutDownObject(pickedObject);
            } else {
                PickUpObject(candidate);
            }
        }

        if (hasObject)
        {
            pickedObject.transform.position = transform.position + new Vector3(0, 0.5f);
        }
    }

    private void PutDownObject(Ingredient ingredient)
    {
        ingredient.PutDown();
        hasObject = false;
        pickedObject = null;
        candidate = null;
    }

    private void PickUpObject(Ingredient ingredient)
    {
        if (ingredient != null) {
            pickedObject = ingredient;
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
