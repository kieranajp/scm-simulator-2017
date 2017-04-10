using System.Collections;
using Pickable;
using UnityEngine;

namespace Event
{
    public class AlvaroEvent : RandomEvent
    {
        public GameObject Explosion;
        public float Delay = 3.0f;

        public override void Fire() {
            StartCoroutine (DestroyAllIngredients());
        }
		
        private IEnumerator DestroyAllIngredients() {
            yield return new WaitForSeconds (Delay);

            var ingredients = FindObjectsOfType<Ingredient> ();

            foreach (var ingredient in ingredients) {
                if (ingredient.IsPickedUp || !ingredient.CanBePicked)
                {
                    continue;
                }
                ingredient.Explode();
            }

            var boxes = FindObjectsOfType<Box.Box> ();
            foreach (var b in boxes) {
                if (!b.CanBePicked)
                {
                    b.EmptyBox();
                }
            }

            yield return new WaitForSeconds(0.3f);
            GetComponent<AudioSource>().Play();
        }
    }
}
