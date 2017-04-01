using UnityEngine;

public class PlayerPickup : PlayerBehaviour {

    Pickable pickedObject;
    Pickable candidate;

    public bool IsCarrying = false;

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

        if (IsCarrying)
        {
            pickedObject.transform.position = transform.position + new Vector3(0, 0.5f);
        }
    }

    private void PutDownObject(Pickable ingredient)
    {
        ingredient.PutDown();
        IsCarrying = false;
        pickedObject = null;
        candidate = null;
    }

    private void PickUpObject(Pickable ingredient)
    {
        if (ingredient != null && ingredient.CanBePicked) {
            ingredient.PickUp();
            pickedObject = ingredient;
            IsCarrying = true;
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
