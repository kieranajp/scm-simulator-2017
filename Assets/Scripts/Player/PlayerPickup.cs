using UnityEngine;

public class PlayerPickup : PlayerBehaviour {

    Pickable pickedObject;
    Pickable candidate;

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

    private void PutDownObject(Pickable ingredient)
    {
        ingredient.PutDown();
        hasObject = false;
        pickedObject = null;
        candidate = null;
    }

    private void PickUpObject(Pickable ingredient)
    {
        if (ingredient != null && ingredient.CanBePicked) {
            ingredient.PickUp();
            pickedObject = ingredient;
            hasObject = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pickable = collision.GetComponent<Pickable>();
        if (pickable != null)
        {
            if (pickable.CanBePicked)
            {
                candidate = pickable;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        candidate = null;
    }
}
