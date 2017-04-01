using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour {
    public Transform SpawnPoint;

    private HashSet<Ingredient> ingredients = new HashSet<Ingredient>();

    public GameObject[] Ingredients;

    public float lastSpawn;

    // Use this for initialization
    void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var i = collision.GetComponent<Ingredient>();
        if (i != null)
        {
            this.ingredients.Add(i);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var i = collision.GetComponent<Ingredient>();
        this.ingredients.Remove(i);

    }
    // Update is called once per frame
    void Update () {
        spawnIngredients(Time.deltaTime);

        foreach(var i in ingredients)
        {
            i.transform.Translate(new Vector3(0, -1 * Time.deltaTime, 0));
        }
	}

    void spawnIngredients(float delta)
    {
        lastSpawn -= delta;
        if (this.lastSpawn < 0)
        {
            var i = Random.Range(0, Ingredients.Length);
            var pos = SpawnPoint.position;
            pos.x += Random.Range(-.5f, .5f);

            GameObject.Instantiate(Ingredients[i], pos, new Quaternion());
            this.lastSpawn = Random.Range(2, 5);
        }
    }
}
