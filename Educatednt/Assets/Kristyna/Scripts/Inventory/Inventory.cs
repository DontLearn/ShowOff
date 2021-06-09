using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public bool[] isFull; //slot is already full
    public GameObject[] slots;
    public List<GameObject> inventoryItems;
    [HideInInspector]
    public GameObject recipeManager;
    
    public void AddIngredienceInventory(GameObject pItem)
    {
        inventoryItems.Add(pItem);
        Debug.Log(pItem.name + " ingredience added to inventory. " + inventoryItems.Count);

        recipeManager = GameObject.FindGameObjectWithTag("RecipeManager");
        Debug.Assert(recipeManager, "GameObject with tag RecipeManager not found! Cannot check for recipe matches!");
        recipeManager.GetComponent<RecipesAll>().ReturnAvailableRecipe(inventoryItems);
        //TO DO: if a recipe availabale show popup with option to craft it
    }
    //TO DO: Method for deleting items in the inventory once you cook a recipe
    public void DeleteIngredienceFromInventory()
    {
        //When you create a meal delete the used ingredients.
        //Or when you just destroy it by UI cross button:
        // --> find first item with this name and delete it
    }
    private void CheckIfRecipeReady()
    {
        //Do you gave enough ingredients to make a recipe
    }
    public void healtPickupTest()
    {
        Debug.Log("Health restored!");
    }
}
