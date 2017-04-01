using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour {

    private BoxRecipe[] recipes;
    private int[] recipesLeft;

    public IngredientType[] IngredientTypes;
    public Sprite[] AllSprites;

    public int IngredientsPerBox = 4;

    public int NumRecipes = 2;

    public int MinSameRecipe = 2;
    public int MaxSameRecipe = 4;

    // Move out of here?
    public int Score = 0;

    // Use this for initialization
    void Start () {
        recipes = new BoxRecipe[NumRecipes];
        recipesLeft = new int[NumRecipes];

        for (int i = 0; i < NumRecipes; i++)
        {
            recipes[i] = GenerateRecipe(); 
            recipesLeft[i] = Random.Range(MinSameRecipe, MaxSameRecipe);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ProccessBox(Box box)
    {
        for (int i = 0; i < NumRecipes; i++)
        {
            if (ValidateBox(box, recipes[i]))
            {
                Score += 1;
                return;
            }
        }

        Score -= 1;
        return;
    }

    public bool ValidateBox(Box box, BoxRecipe br)
    {
        foreach(IngredientType type in br.Bom)
        {
            if (!box.HasIngredient(type))
            {
                return false;
            }
        }

        return true;
    }

    private BoxRecipe GenerateRecipe()
    {
        IngredientType[] ingredients = new IngredientType[IngredientsPerBox];
        Sprite[] sprites = new Sprite[IngredientsPerBox];

        Dictionary<int, bool> usedIngredients = new Dictionary<int, bool>();

        for (int i = 0; i < IngredientsPerBox; i++)
        {

            int index;
            while(true)
            {
                index = Random.Range(0, IngredientTypes.Length);
                if (!usedIngredients.ContainsKey(index))
                {
                    usedIngredients.Add(index, true);
                    break;
                }
            }

            ingredients[i] = IngredientTypes[index];
            sprites[i] = AllSprites[index];
        }
        return new BoxRecipe(ingredients, sprites);
    }

    public BoxRecipe GetRecipe(int index)
    {
        return recipes[index];
    }

    public int GetLeft(int index)
    {
        return recipesLeft[index];
    }
}
