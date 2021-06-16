using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RecipeManager : MonoBehaviour
{    
    [SerializeField]
    public static List<RecipeSerializable> recipes = new List<RecipeSerializable>();
    [SerializeField]
    private Inventory _inventory;

    public void CheckAvailableRecipes(byte pRice, byte pTomatoe, byte pMushroom, byte pBurger)
    {
        for (int recipeNum = 0; recipeNum < recipes.Count; recipeNum++)
        {
            //check all three ingredients:
            if (pRice >= recipes[recipeNum].rice &&
                pTomatoe >= recipes[recipeNum].tomatoe &&
                pMushroom >= recipes[recipeNum].mushroom &&
                pBurger >= recipes[recipeNum].burger)
            {
                recipes[recipeNum].isReadyToCook = true;
                Debug.Log($"{recipes[recipeNum].name} available to cook!");
            }
            else
            {
                recipes[recipeNum].isReadyToCook = false;
            }
        }
    }
    public void CookRecipe(int pRecipeNumber)
    {
        _inventory.GetComponent<Inventory>().DeleteIngredienceFromInventory(recipes[pRecipeNumber].rice, recipes[pRecipeNumber].tomatoe, recipes[pRecipeNumber].mushroom, recipes[pRecipeNumber].burger);
    }
}
