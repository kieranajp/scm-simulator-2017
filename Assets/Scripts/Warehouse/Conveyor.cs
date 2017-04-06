using System.Collections.Generic;
using UnityEngine;

namespace Warehouse
{
    public class Conveyor : MonoBehaviour {
        public Transform SpawnPoint;

        private HashSet<Pickable.Pickable> _items = new HashSet<Pickable.Pickable>();

        public Vector3 Direction = new Vector3(0, -1, 0);
        public float MinSpawnTime = 1;
        public float MaxSpawnTime = 2;

        public float Speed = 1f;

        public GameObject[] Items;
        public int[] ItemsWeight;

        public int[] ItemsRecipeAppliedWeights;

        private int _totalWeight;

        public float LastSpawn;

        private void Start () {
            if(Items.Length != ItemsWeight.Length)
            {
                Debug.LogError("Conveyor Items and Itemsweight are not same length");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var i = collision.GetComponent<Pickable.Pickable>();
            if (i != null)
            {
                _items.Add(i);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var i = collision.GetComponent<Pickable.Pickable>();
            _items.Remove(i);

        }
        // Update is called once per frame
        void Update () {
            SpawnIngredients(Time.deltaTime);

            foreach(var i in _items)
            {
                if (i == null) {
                    continue;
                }
                i.transform.Translate(Direction * Time.deltaTime * Speed, Space.World);
            }

        }

        public void ApplyWeights(Dictionary<IngredientType, int> weights)
        {
            ItemsRecipeAppliedWeights = new int[ItemsWeight.Length];

            for(int i = 0; i < Items.Length; i++)
            {
                ItemsRecipeAppliedWeights[i] = ItemsWeight[i];
                Ingredient ingredient = Items[i].GetComponent<Ingredient>();
                if(ingredient != null)
                {
                    IngredientType type = ingredient.Type;
                    if (weights.ContainsKey(type))
                    {
                        ItemsRecipeAppliedWeights[i] = ItemsRecipeAppliedWeights[i] + weights[type];
                    }
                }
            }
            _totalWeight = 0;
            foreach (int weight in ItemsRecipeAppliedWeights)
            {

                _totalWeight += weight;

            }
        }

        void SpawnIngredients(float delta)
        {
            LastSpawn -= delta;
            if (LastSpawn < 0)
            {
                var rngnum = Random.Range(0, _totalWeight);

                int i = 0;
                int curWeight = ItemsRecipeAppliedWeights[0];
                while (rngnum > curWeight)
                {
                    i++;
                    curWeight += ItemsRecipeAppliedWeights[i];
                }


                var pos = SpawnPoint.position;
                pos.x += Random.Range(-.1f, .1f);


                Instantiate(Items[i], pos, Quaternion.Euler(0,0,Random.Range(0, 360)));
                LastSpawn = Random.Range(MinSpawnTime, MaxSpawnTime);
            }
        }
    }
}
