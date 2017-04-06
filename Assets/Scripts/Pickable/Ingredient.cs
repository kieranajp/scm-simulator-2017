﻿using UnityEngine;

public enum IngredientType
{
    Carrot,
    Brocolli,
    Banana,
    Chicken,
    Lime,
    Meat,
    Milk,
    Pepper,
    Spud,
    Tomato
}

public class Ingredient : Pickable {
    public IngredientType Type;

    override public void PutDown()
    {
        base.PutDown();
        var colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D c in colliders)
        {
            var box = c.GetComponent<Box.Box>();
            if (box != null)
            {
                box.AddIngredient(this);
            }
        }
    }
}
