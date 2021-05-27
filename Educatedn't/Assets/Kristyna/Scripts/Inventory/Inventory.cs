using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public bool[] isFull; //slot is already full
    public GameObject[] slots;
    public List<GameObject> ingredients;
    
    public void AddIngredienceInventory(GameObject pItem)
    {
        ingredients.Add(pItem);
        Debug.Log(pItem.name + " ingredience added to inventory. " + ingredients.Count);
    }

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
