using Box;
using UnityEngine;
using UnityEngine.UI;

namespace Hud
{
    public class RecipeHud : MonoBehaviour {
        private BoxManager _bm;

        public int RecipeIndex;

        // Use this for initialization
        private void Start () {
            _bm = FindObjectOfType<BoxManager>();
        }
	
        // Update is called once per frame
        private void Update () {
            var recipe = _bm.GetRecipe(RecipeIndex);
            var ingredients = transform.FindChild("Ingredients");
            transform.FindChild("Header").FindChild("Text").GetComponent<Text>().text = _bm.GetLeft(RecipeIndex) + "X";

            for (var i = 1; i < 5; i++)
            {
                ingredients.FindChild("Ing_" + i.ToString()).FindChild("Image").GetComponent<Image>().sprite = recipe.BomSprites[i-1];
            }
        }
    }
}
