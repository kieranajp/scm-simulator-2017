using Pickable;
using UnityEngine;

namespace Box
{
    public class BoxRecipe
    {
        public IngredientType[] Bom;
        public Sprite[] BomSprites;

        public int NumIngredients = 4;

        public BoxRecipe(IngredientType[] types, Sprite[] sprites) {
            Bom = types;
            BomSprites = sprites;
        }
    }
}
