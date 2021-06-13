using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //Inventory finds all existing Slots by tag and recognizes them as available space to store items.- Inventory slots are reserved as follows: 0 - Mushroom, 1 - Tomatoe, 2 - Rice
    public int slotLimit = 5;
    public GameObject itemButtonPrefab;
    [Header("0-rice, 1 - toamtoe, 2 - mushroom")]
    public Sprite[] pickupSprites = new Sprite[3];

    [HideInInspector]
    public GameObject[] slots;
    [HideInInspector]
    [SerializeField]
    public bool[] isFull;

    private Recipes _recipes; //TO DO: FIND ITTT


    //int _inventory = 321;
    //- existing method for int > string
    //To do: public > private var
    
    private byte _rice = 0;   
    private byte _tomatoe = 0;
    private byte _mushroom = 0;

    private void Awake()
    {
        _recipes = GameObject.FindGameObjectWithTag("RecipeManager").GetComponent<Recipes>();
            Debug.Assert(_recipes, "Recipe Manager with the Recipes script not found by the tag!");
        findSlotsInScene();
    }
    private bool checkIngredientAvailability(byte pIngredient, Ingredient.ingredientType pType)
    {
        if (pIngredient < slotLimit)
        {
            if (pIngredient <= 0)
            {
                //Create UI:
                itemButtonPrefab.GetComponent<Image>().sprite = pickupSprites[(int)pType];
                Instantiate(itemButtonPrefab, slots[(int)pType].transform, false);
                
                slots[(int)pType].GetComponent<Slot>().showItemNumber();

                //Debug.Log($"Inventory UI created!");
            }

            switch (pType)
            {
                case Ingredient.ingredientType.Rice:
                    ++_rice;
                    slots[(int)pType].GetComponent<Slot>().PlusOneItem();
                    break;
                case Ingredient.ingredientType.Tomatoe:
                    ++_tomatoe;
                    slots[(int)pType].GetComponent<Slot>().PlusOneItem();
                    break;
                case Ingredient.ingredientType.Mushroom:
                    ++_mushroom;
                    slots[(int)pType].GetComponent<Slot>().PlusOneItem();
                    break;
            }
            if (_rice >= 1 && _tomatoe >= 1 && _mushroom >= 1) _recipes.ReturnAvailableRecipe(_rice, _tomatoe, _mushroom);

            Debug.Log($"{pType} added to inventory! inventory: {_rice}, {_tomatoe}, {_mushroom}");
            return true;
        }
        else
        {
            Debug.Log($"Inventory is full for {pType} ingredient!");
            isFull[(int)pType] = true;
            return false;
        }
    }
    /// <summary>
    /// Returns true if item was added successfully. False the inventory is full for the specific ingredient.
    /// Ingredient pickup uses this for check if should destroy itself.
    /// </summary>
    /// <param name="pType"></param>
    /// <returns></returns>
    public bool AddItemToInventory(Ingredient.ingredientType pType)
    {
        switch (pType)
        {
            case Ingredient.ingredientType.Rice:
                if(checkIngredientAvailability(_rice, Ingredient.ingredientType.Rice))
                {
                    //check for recipes
                    //call UI IF RECIPE found or UI find on its own
                    return true;
                }
                return false;
            case Ingredient.ingredientType.Tomatoe:
                if (checkIngredientAvailability(_tomatoe, Ingredient.ingredientType.Tomatoe))
                {
                    //check for recipes
                    //call UI IF RECIPE found or UI find on its own
                    return true;
                }
                return false;
            case Ingredient.ingredientType.Mushroom:
                if (checkIngredientAvailability(_mushroom, Ingredient.ingredientType.Mushroom))
                {
                    //check for recipes
                    //call UI IF RECIPE found or UI find on its own
                    return true;
                }
                return false;
        }
        return false;
    }
    private void findSlotsInScene()
    {
        slots = GameObject.FindGameObjectsWithTag("Slot");
        if (slots.Length <= 0)
        {
            Debug.LogWarning("Inventory: There are no slots to be found!");
        }
    }
    //This called from Recipes
    public void DeleteIngredienceFromInventory(byte pRice, byte pTomatoe, byte pMushroom)
    {
        _rice -= pRice;
            slots[0].GetComponent<Slot>().MinusItems(pRice);
        _tomatoe -= pTomatoe;
            slots[1].GetComponent<Slot>().MinusItems(pTomatoe);
        _mushroom -= pMushroom;
            slots[2].GetComponent<Slot>().MinusItems(pMushroom);
        Debug.Log($"Recipe cooked! Inventory now: {_rice}, {_tomatoe}, {_mushroom}");
    }
    public void healtPickupTest()
    {
        Debug.Log("Health restored!");
    }
}
