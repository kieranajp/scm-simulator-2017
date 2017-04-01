using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPickup : PlayerBehaviour {

    Pickable pickedObject;
    public List<Pickable> candidates;

    public bool IsCarrying = false;

    private void Start()
    {
        candidates = new List<Pickable>();
    }

    private void Update()
    {
        if (PlayerInput.GetButtonDown("A", Player))
        {
            if (pickedObject != null) {
                PutDownObject(pickedObject);
            } else {
                PickUpClosestObject(candidates);
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
        candidates.Remove(ingredient);
        IsCarrying = false;
        pickedObject = null;
    }

    private void PickUpClosestObject(List<Pickable> ingredients)
    {
        if (ingredients.Count == 0) return;

        var ingredient = ingredients.OrderBy((t) => { return (t.transform.position - transform.position).sqrMagnitude; }).First();
        if (ingredient != null && ingredient.CanBePicked) {
            ingredient.PickUp();
            pickedObject = ingredient;
            IsCarrying = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var pickables = collision.GetComponents<Pickable>();
        candidates = new List<Pickable>(pickables);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var pickables = collision.GetComponents<Pickable>();
        if (pickables != null)
        {
            foreach (var p in pickables)
            {
                candidates.Remove(p);
            }
        }
    }
}
