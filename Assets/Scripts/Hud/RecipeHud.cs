using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeHud : MonoBehaviour {
    private BoxManager bm;

    public int RecipeIndex;

    // Use this for initialization
    void Start () {
		    bm = FindObjectOfType<BoxManager>();
    }
	
	// Update is called once per frame
	void Update () {
        var recipe = bm.GetRecipe(RecipeIndex);
        var ingredients = transform.FindChild("Ingredients");
        var header = transform.FindChild("Header").FindChild("Text").GetComponent<Text>().text = bm.GetLeft(RecipeIndex) + "X";

        for (int i = 1; i < 5; i++)
        {
            ingredients.FindChild("Ing_" + i.ToString()).FindChild("Image").GetComponent<Image>().sprite = recipe.BomSprites[i-1];
        }
    }
}
