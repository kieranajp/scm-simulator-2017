using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEditor.Animations;
using UnityEngine;

namespace Box
{
    public class Box : Pickable.Pickable
    {
        public Sprite CarryingBox;
        public Sprite ClosedBox;
        public int MaximumIngredients = 4;
        public List<Ingredient> Ingredients;
        public List<Transform> SpawnPoints;
        private bool _isClosed;
        private PlayerMovement _movement;
        private float _originalSpeed;
        public GameObject AnimationThrow;
        public bool HasBeenPicked;
        public AnimatorController ItemThrowerAnimator;

        private void Start()
        {
            CanBePicked = false;
            _movement = FindObjectOfType<PlayerMovement>();
            _originalSpeed = _movement.Speed;
        }

        public void AddIngredient(Ingredient ingredient)
        {
            if (_isClosed)
            {
                return;
            }

            ingredient.transform.parent = gameObject.transform;
            ingredient.transform.position = SpawnPoints[Ingredients.Count].transform.position;
            Ingredients.Add(ingredient);
            ingredient.CanBePicked = false;
            ingredient.FiveSecondRule = false;


            var decorators = ingredient.GetComponents<ProximityDecorator>();
            foreach (var dec in decorators)
            {
                dec.enabled = false;
            }
            var ingredientColliders = ingredient.GetComponents<Collider2D>();
            foreach (var col in ingredientColliders)
            {
                col.enabled = false;
            }

            if (Ingredients.Count == MaximumIngredients)
            {
                CloseBox();
            }
        }

        public void EmptyBox()
        {
            Ingredients.Clear();
            var children = new List<GameObject>();
            foreach (Transform child in transform)
            {
                if (child.GetComponent<Ingredient>())
                {
                    children.Add(child.gameObject);
                }
            }
            StartCoroutine(ThrowIngredients(children));
        }

        private IEnumerator ThrowIngredients(List<GameObject> ingredients)
        {
            foreach (var i in ingredients)
            {
                var container = new GameObject("_container");
                container.transform.position = i.transform.position;
                i.GetComponent<Ingredient>().enabled = false;
                i.transform.parent = container.transform;
                var animator = i.AddComponent<Animator>();
                animator.enabled = true;
                animator.runtimeAnimatorController = ItemThrowerAnimator;
                animator.Play("Throw" + Random.Range(1, 4));
            }
            yield return new WaitForSeconds(0.3f);
            foreach (var i in ingredients)
            {
                var ing = i.GetComponent<Ingredient>();
                ing.CanBePicked = true;
                ing.FiveSecondRule = true;
                ing.IsPickedUp = false;
                ing.enabled = true;
                var obj = i.transform.parent;
                i.GetComponent<Collider2D>().enabled = true;
                i.GetComponent<ProximityDecorator>().enabled = true;
                i.transform.parent = null;
                Destroy(i.GetComponent<Animator>());
                Destroy(obj.gameObject);
            }
        }

        public override void PickUp()
        {
            base.PickUp();
            HasBeenPicked = true;
            GetComponent<SpriteRenderer>().sprite = CarryingBox;
            GetComponent<SpriteRenderer>().sortingOrder = 4;
            _movement.Speed = .66f * _originalSpeed;
        }

        public override void PutDown()
        {
            base.PutDown();
            GetComponent<SpriteRenderer>().sprite = ClosedBox;
            GetComponent<SpriteRenderer>().sortingOrder = 1;
            _movement.Speed = _originalSpeed;
        }

        public void CloseBox()
        {
            _isClosed = true;
            CanBePicked = true;
            GetComponent<SpriteRenderer>().sprite = ClosedBox;
            foreach (var i in Ingredients)
            {
                i.GetComponent<SpriteRenderer>().enabled = false;
            }
            Destroy(transform.GetChild(0).GetComponent<Collider2D>());
        }

        public void ThrowBox()
        {
            PutDown();
            var obj = Instantiate(AnimationThrow, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Destroy(obj, 1f);
        }

        public bool HasIngredient(IngredientType type)
        {
            foreach (Ingredient i in Ingredients)
            {
                if (i.Type == type)
                {
                    return true;
                }
            }

            return false;
        }
    }
}