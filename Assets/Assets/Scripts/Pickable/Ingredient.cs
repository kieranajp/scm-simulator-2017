using UnityEngine;

public enum IngredientType
{
    Carrot,
    Brocolli,
    Banana
}

public class Ingredient : MonoBehaviour {

    public bool IsPickedUp;
    public bool IsUsed;
    public IngredientType Type;

    public void PickUp()
    {
        IsPickedUp = true;
    }

    public void PutDown()
    {
        IsUsed = true;
    }
}
