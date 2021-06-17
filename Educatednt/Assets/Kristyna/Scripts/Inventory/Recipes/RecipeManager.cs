using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RecipeManager : MonoBehaviour
{    
    public static List<RecipeSerializable> availableRecipes = new List<RecipeSerializable>();//used in Kitchen scene to access this data
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
        if (availableRecipes.Count > 0) availableRecipes.Clear();

        for (int recipeNum = 0; recipeNum < recipes.Count; recipeNum++)
        {
            //check all three ingredients:
            if (pRice >= recipes[recipeNum].rice &&
                pTomatoe >= recipes[recipeNum].tomatoe &&
                pMushroom >= recipes[recipeNum].mushroom &&
                pBurger >= recipes[recipeNum].burger)
            {
                availableRecipes.Add(recipes[recipeNum]);
                Debug.Log($"{recipes[recipeNum].name} available to cook! Available recipes list.count = {availableRecipes.Count}");
            }
        }
    }
    public void CookRecipe(int pRecipeNumber)
    {
        _inventory.GetComponent<Inventory>().DeleteIngredienceFromInventory(recipes[pRecipeNumber].rice, recipes[pRecipeNumber].tomatoe, recipes[pRecipeNumber].mushroom, recipes[pRecipeNumber].burger);
    }
}
