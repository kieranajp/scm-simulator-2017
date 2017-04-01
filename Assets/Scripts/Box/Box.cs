using System.Collections.Generic;
using UnityEngine;

public class Box : Pickable {

    public Sprite carryingBox;
    public Sprite closedBox;
    public int MaximumIngredients = 4;
    public List<Ingredient> Ingredients;
    public List<Transform> SpawnPoints;
    private bool _isClosed;
    private PlayerMovement movement;
    private float _originalSpeed;
    public GameObject AnimationThrow;
    public bool HasBeenPicked;

    private void Start()
    {
        CanBePicked = false;
        movement = FindObjectOfType<PlayerMovement>();
        _originalSpeed = movement.Speed;
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

    public override void PickUp()
    {
        base.PickUp();
        HasBeenPicked = true;
        GetComponent<SpriteRenderer>().sprite = carryingBox;
        GetComponent<SpriteRenderer>().sortingOrder = 4;
        movement.Speed = .66f * _originalSpeed;
    }

    public override void PutDown()
    {
        base.PutDown();
        GetComponent<SpriteRenderer>().sprite = closedBox;
        GetComponent<SpriteRenderer>().sortingOrder = 1;
        movement.Speed = _originalSpeed;
    }

    public void CloseBox()
    {
        _isClosed = true;
        CanBePicked = true;
        GetComponent<SpriteRenderer>().sprite = closedBox;
        foreach (var i in Ingredients)
        {
            i.GetComponent<SpriteRenderer>().enabled = false;
        }
        Destroy(transform.GetChild(0).GetComponent<Collider2D>());
    }

    public void ThrowBox()
    {
        FindObjectOfType<BoxManager>().ProccessBox(this);
        this.PutDown();
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
