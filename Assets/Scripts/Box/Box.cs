using System.Collections.Generic;
using UnityEngine;

public class Box : Pickable {

    public int MaximumIngredients = 4;
    public List<Ingredient> Ingredients;
    public List<Transform> SpawnPoints;
    private bool _isClosed;

    private void Start()
    {
        CanBePicked = false;
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

    public void CloseBox()
    {
        _isClosed = true;
        CanBePicked = true;
        Destroy(transform.GetChild(0).GetComponent<Collider2D>());
    }
}
