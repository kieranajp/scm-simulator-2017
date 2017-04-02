using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour {
    public Transform SpawnPoint;

    private HashSet<Pickable> items = new HashSet<Pickable>();
    private HashSet<Pickable> takenOutIngredients = new HashSet<Pickable>();

    public Vector3 Direction = new Vector3(0, -1, 0);
    public float MinSpawnTime = 1;
    public float MaxSpawnTime = 2;

    public float Speed = 1f;

    public GameObject[] Items;
    public int[] ItemsWeight;

    public int[] itemsRecipeAppliedWeights;

    private int totalWeight = 0;

    public float lastSpawn;

    // Use this for initialization
    void Start () {
        if(Items.Length != ItemsWeight.Length)
        {
            Debug.LogError("Conveyor Items and Itemsweight are not same length");
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var i = collision.GetComponent<Pickable>();
        if (i != null)
        {
            this.items.Add(i);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var i = collision.GetComponent<Pickable>();
        this.items.Remove(i);

    }
    // Update is called once per frame
    void Update () {
        spawnIngredients(Time.deltaTime);

        foreach(var i in items)
        {
            if (i == null) {
                continue;
            }
            i.transform.Translate(Direction * Time.deltaTime * Speed, Space.World);
        }

	}

    public void ApplyWeights(Dictionary<IngredientType, int> weights)
    {
        itemsRecipeAppliedWeights = new int[ItemsWeight.Length];

        for(int i = 0; i < Items.Length; i++)
        {
            itemsRecipeAppliedWeights[i] = ItemsWeight[i];
            Ingredient ingredient = Items[i].GetComponent<Ingredient>();
            if(ingredient != null)
            {
                IngredientType type = ingredient.Type;
                if (weights.ContainsKey(type))
                {
                    itemsRecipeAppliedWeights[i] = itemsRecipeAppliedWeights[i] + weights[type];
                }
            }
        }
        totalWeight = 0;
        foreach (int weight in itemsRecipeAppliedWeights)
        {

            totalWeight += weight;

        }
    }

    void spawnIngredients(float delta)
    {
        lastSpawn -= delta;
        if (this.lastSpawn < 0)
        {
            var rngnum = Random.Range(0, totalWeight);

            int i = 0;
            int curWeight = itemsRecipeAppliedWeights[0];
            while (rngnum > curWeight)
            {
                i++;
                curWeight += itemsRecipeAppliedWeights[i];
            }


            var pos = SpawnPoint.position;
            pos.x += Random.Range(-.1f, .1f);


            GameObject.Instantiate(Items[i], pos, Quaternion.Euler(0,0,Random.Range(0, 360)));
            this.lastSpawn = Random.Range(MinSpawnTime, MaxSpawnTime);
        }
    }
}
