using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public byte stackLimit = 5;
    public GameObject itemButtonPrefab;
    [Header("0-rice, 1 - toamtoe, 2 - mushroom, 3 - burger")]
    public Sprite[] pickupSprites = new Sprite[4];

    [HideInInspector]
    public GameObject[] slots;
    [HideInInspector]
    [SerializeField]
    public bool[] isFull;

    private RecipeManager _recipes;

    private byte _rice = 0;   
    private byte _tomatoe = 0;
    private byte _mushroom = 0;
    private byte _burger = 0;

    private void Awake()
    {
        _recipes = GameObject.FindGameObjectWithTag("RecipeManager").GetComponent<RecipeManager>();
            Debug.Assert(_recipes, "Recipe Manager with the Recipes script not found by the tag!");
        findSlotsInScene();
    }
    private bool checkIngredientAvailability(byte pIngredient, Ingredient.ingredientType pType)
    {
        if (pIngredient < stackLimit)
        {
            if (pIngredient <= 0)
            {
                //Create UI:
                itemButtonPrefab.GetComponent<Image>().sprite = pickupSprites[(int)pType];
                Instantiate(itemButtonPrefab, slots[(int)pType].transform, false);
                
                slots[(int)pType].GetComponent<Slot>().showItemNumber();
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
                case Ingredient.ingredientType.Burger:
                    ++_burger;
                    slots[(int)pType].GetComponent<Slot>().PlusOneItem();
                    break;
            }
            if (_rice >= 1 || _tomatoe >= 1 || _mushroom >= 1 || _burger >= 1) _recipes.CheckAvailableRecipes(_rice, _tomatoe, _mushroom, _burger);

            Debug.Log($"{pType} added to inventory! inventory: {_rice}, {_tomatoe}, {_mushroom}, {_burger}");
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
                    return true;
                }
                return false;
            case Ingredient.ingredientType.Tomatoe:
                if (checkIngredientAvailability(_tomatoe, Ingredient.ingredientType.Tomatoe))
                {
                    return true;
                }
                return false;
            case Ingredient.ingredientType.Mushroom:
                if (checkIngredientAvailability(_mushroom, Ingredient.ingredientType.Mushroom))
                {
                    return true;
                }
                return false;
            case Ingredient.ingredientType.Burger:
                if (checkIngredientAvailability(_burger, Ingredient.ingredientType.Burger))
                {
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
    public void DeleteIngredienceFromInventory(byte pRice, byte pTomatoe, byte pMushroom, byte pBurger)
    {
        _rice -= pRice;
            slots[0].GetComponent<Slot>().MinusItems(pRice);
        _tomatoe -= pTomatoe;
            slots[1].GetComponent<Slot>().MinusItems(pTomatoe);
        _mushroom -= pMushroom;
            slots[2].GetComponent<Slot>().MinusItems(pMushroom);
        _burger -= pBurger;
            slots[3].GetComponent<Slot>().MinusItems(pBurger);
        Debug.Log($"Recipe cooked! Inventory now: {_rice}, {_tomatoe}, {_mushroom}, {_burger}");
    }
    public void healtPickupTest()
    {
        Debug.Log("Health restored!");
    }
}
