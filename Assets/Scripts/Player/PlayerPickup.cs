using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

namespace Player
{
    public class PlayerPickup : MonoBehaviour {

        public AudioClip PickupSound;
        public AudioClip PutDownSound;
        public Pickable.Pickable PickedUpObject;
        public List<Pickable.Pickable> Candidates;
        private PlayerBehaviour _playerBehaviour;

        public bool IsCarrying;

        private void Start()
        {
            Candidates = new List<Pickable.Pickable>();
            _playerBehaviour = GetComponent<PlayerBehaviour>();
        }

        private void Update()
        {
            if (PlayerInput.GetButtonDown("A", _playerBehaviour.Player))
            {
                if (PickedUpObject != null) {
                    PutDownObject();
                } else {
                    PickUpClosestObject(Candidates);
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
            Candidates.Remove(PickedUpObject);
            IsCarrying = false;
            PickedUpObject = null;
        }

        public void PickUpClosestObject(List<Pickable.Pickable> ingredients)
        {
            if (ingredients.Count == 0) return;

            var ingredient = ingredients.OrderBy(t => (t.transform.position - transform.position).sqrMagnitude).First();
            if (ingredient != null && ingredient.CanBePicked && !ingredient.IsPickedUp && ingredient.enabled) {
                ingredient.PickUp();
                PickedUpObject = ingredient;
                IsCarrying = true;
                AudioSource.PlayClipAtPoint(PickupSound, FindObjectOfType<AudioSource>().transform.position);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            var pickables = collision.GetComponents<Pickable.Pickable>();
            Candidates = new List<Pickable.Pickable>(pickables);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var pickables = collision.GetComponents<Pickable.Pickable>();
            if (pickables == null) return;

            foreach (var p in pickables)
            {
                Candidates.Remove(p);
            }
        }
    }
}
