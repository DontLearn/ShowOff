using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    [SerializeField]
    public List<RecipeSerializable> recipes = new List<RecipeSerializable>();
    [SerializeField]
    private Inventory _inventory;

    private void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        Debug.Assert(_inventory, "InventoryNotFound_PlayerMissing");
    }

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
                //note the recipe at 0 as available to cook.
                //So, Kitchen scene can show/hide cook buttons.
                //KitchenAvailableRecipesController.AvailableRecipes[recipeNum] = true;
                Debug.Log($"{recipes[recipeNum].name} available to cook!");
            }
            else
            {
                //KitchenAvailableRecipesController.AvailableRecipes[recipeNum] = false;
            }
        }
    }
    public void CookRecipe(int pRecipeNumber)
    {
        _inventory.GetComponent<Inventory>().DeleteIngredienceFromInventory(recipes[pRecipeNumber].rice, recipes[pRecipeNumber].tomatoe, recipes[pRecipeNumber].mushroom, recipes[pRecipeNumber].burger);
    }
}
