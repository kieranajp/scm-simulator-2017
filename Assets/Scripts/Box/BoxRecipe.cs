using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRecipe
{

    public IngredientType[] Bom;
    public Sprite[] BomSprites;

    public int NumIngredients = 4;

    // Use this for initialization
    public BoxRecipe(IngredientType[] types, Sprite[] sprites) {
        Bom = types;
        BomSprites = sprites;
	}

}
