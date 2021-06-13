using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Recipes : MonoBehaviour
{
    [SerializeField]
    public List<RecipeSerializable> recipes = new List<RecipeSerializable>();
    [SerializeField]
    private Inventory _inventory;
    public void ReturnAvailableRecipe(byte pRice, byte pTomatoe, byte pMushroom)
    {
        foreach (RecipeSerializable recipe in recipes)
        {
            //check all three ingredients:
            if (pRice >= recipe.rice &&
                pTomatoe >= recipe.tomatoe &&
                pMushroom >= recipe.mushroom)
            {
                Debug.Log($"{recipe.name} available to cook!");
                //let UI know -> ask Eugene
                CookRecipe(recipe);
            }
        }
    }

    public void CookRecipe(RecipeSerializable pRecipe)
    {
        _inventory.GetComponent<Inventory>().DeleteIngredienceFromInventory(pRecipe.rice, pRecipe.tomatoe, pRecipe.mushroom);
    }
}
