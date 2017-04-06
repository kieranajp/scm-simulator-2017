using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;
using Utility;

public class PlayerPickup : MonoBehaviour {

    public AudioClip PickupSound;
    public AudioClip PutDownSound;
    public Pickable PickedUpObject;
    public List<Pickable> candidates;
    private PlayerBehaviour player;

    public bool IsCarrying = false;

    private void Start()
    {
        candidates = new List<Pickable>();
        player = GetComponent<PlayerBehaviour>();
    }

    private void Update()
    {
        if (PlayerInput.GetButtonDown("A", player.Player))
        {
            if (PickedUpObject != null) {
                PutDownObject();
            } else {
                PickUpClosestObject(candidates);
            }
        }

        if (IsCarrying)
        {
            PickedUpObject.transform.position = transform.position + new Vector3(0, 0.5f);
        }
    }

    public void PutDownObject()
    {
        if (! IsCarrying) {
            return;
        }
        AudioSource.PlayClipAtPoint(PutDownSound, FindObjectOfType<AudioSource>().transform.position);
        PickedUpObject.PutDown();
        candidates.Remove(PickedUpObject);
        IsCarrying = false;
        PickedUpObject = null;
    }

    public void PickUpClosestObject(List<Pickable> ingredients)
    {
        if (ingredients.Count == 0) return;

        var ingredient = ingredients.OrderBy((t) => { return (t.transform.position - transform.position).sqrMagnitude; }).First();
        if (ingredient != null && ingredient.CanBePicked && !ingredient.IsPickedUp) {
            ingredient.PickUp();
            PickedUpObject = ingredient;
            IsCarrying = true;
            AudioSource.PlayClipAtPoint(PickupSound, FindObjectOfType<AudioSource>().transform.position);
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
