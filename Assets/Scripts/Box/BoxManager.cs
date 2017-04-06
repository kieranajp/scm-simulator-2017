using System.Collections.Generic;
using UnityEngine;
using Warehouse;

namespace Box
{
    public class BoxManager : MonoBehaviour {

        private BoxRecipe[] _recipes;
        private int[] _recipesLeft;
        public int[] PlayerGoods;
        public int[] PlayerWrongs;
        public int TotalGood;
        public int TotalBad;

        public IngredientType[] IngredientTypes;
        public Sprite[] AllSprites;

        public int IngredientsPerBox = 4;

        public int NumRecipes = 2;

        public int MinSameRecipe = 2;
        public int MaxSameRecipe = 4;
        public int WinningScore = 3;

        public int Score;

        void Start () {
            PlayerGoods = new int[4];
            PlayerWrongs = new int[4];
            _recipes = new BoxRecipe[NumRecipes];
            _recipesLeft = new int[NumRecipes];

            for (int i = 0; i < NumRecipes; i++)
            {
                _recipes[i] = GenerateRecipe();
                _recipesLeft[i] = Random.Range(MinSameRecipe, MaxSameRecipe);
            }
            ApplyWeights();
        }

        public void ProccessBox(Player.Player player, Box box)
        {
            var pIndex = int.Parse(player.ToString().Substring(1, 1)) - 1;
            for (int i = 0; i < NumRecipes; i++)
            {
                if (ValidateBox(box, _recipes[i]))
                {
                    PlayerGoods[pIndex]++;
                    Score += 1;
                    RecipeComplete(i);
                    TotalGood++;
                    return;
                }
            }

            PlayerWrongs[pIndex]++;
            Score -= 1;
            TotalBad++;
        }

        public void RecipeComplete(int index)
        {
            _recipesLeft[index]--;

            if(_recipesLeft[index] <= 0)
            {
                _recipes[index] = GenerateRecipe();
                _recipesLeft[index] = Random.Range(MinSameRecipe, MaxSameRecipe);
            }

            ApplyWeights();
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
            return _recipes[index];
        }

        public int GetLeft(int index)
        {
            return _recipesLeft[index];
        }

        private void ApplyWeights()
        {
            Dictionary<IngredientType, int> weights = new Dictionary<IngredientType, int>();
            foreach(BoxRecipe br in _recipes)
            {
                foreach(IngredientType it in br.Bom)
                {
                    if (weights.ContainsKey(it))
                    {
                        weights[it]++;
                    }
                    else
                    {
                        weights.Add(it, 1);
                    }
                }
            }

            FindObjectOfType<Conveyor>().ApplyWeights(weights);
        }
    }
}
