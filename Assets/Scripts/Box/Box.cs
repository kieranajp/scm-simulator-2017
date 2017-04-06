using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Box
{
    public class Box : Pickable {

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

        private void Start()
        {
            FiveSecondRule = false;
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
                Destroy(dec);
            }
            var ingredientColliders = ingredient.GetComponents<Collider2D>();
            foreach (var col in ingredientColliders)
            {
                Destroy(col);
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
            foreach (Transform child in transform) {
                if (child.GetComponent<Ingredient>())
                {
                    children.Add(child.gameObject);
                }
            }
            children.ForEach(Destroy);
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
            foreach(Ingredient i in Ingredients)
            {
                if (i.Type == type) {
                    return true;
                }
            }

            return false;
        }
    }
}
