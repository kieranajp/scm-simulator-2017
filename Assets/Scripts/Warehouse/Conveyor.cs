using System.Collections.Generic;
using System.Linq;
using Pickable;
using UnityEngine;

namespace Warehouse
{
    public class Conveyor : MonoBehaviour
    {
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
        private int _numberOfPlayers;

        private void Start()
        {
            _numberOfPlayers = FindObjectOfType<Game>().NumberOfPlayers;
            if (Items.Length != ItemsWeight.Length)
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
        void Update()
        {
            SpawnIngredients(Time.deltaTime);

            foreach (var i in _items)
            {
                if (i == null)
                {
                    continue;
                }
                i.transform.Translate(Direction * Time.deltaTime * Speed, Space.World);
            }
        }

        public void ApplyWeights(Dictionary<IngredientType, int> weights)
        {
            ItemsRecipeAppliedWeights = new int[ItemsWeight.Length];

            for (var i = 0; i < Items.Length; i++)
            {
                ItemsRecipeAppliedWeights[i] = ItemsWeight[i];
                var ingredient = Items[i].GetComponent<Ingredient>();
                if (ingredient != null)
                {
                    var type = ingredient.Type;
                    if (weights.ContainsKey(type))
                    {
                        ItemsRecipeAppliedWeights[i] += weights[type];
                    }
                }
                else
                {
                    var percentage = 0.17f * _numberOfPlayers;
                    if (percentage <= Random.Range(0f, 1f))
                    {
                        ItemsRecipeAppliedWeights[i]++;
                    }
                }
            }
            _totalWeight = 0;
            _totalWeight = ItemsRecipeAppliedWeights.Sum();
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


                Instantiate(Items[i], pos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                LastSpawn = Random.Range(MinSpawnTime, MaxSpawnTime);
            }
        }
    }
}