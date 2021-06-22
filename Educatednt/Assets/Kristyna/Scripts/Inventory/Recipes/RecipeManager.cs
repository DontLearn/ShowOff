using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    [SerializeField]
    [Header("0- soup, 1 - pilaf, 2 - friedRice, 3 - burger")]
    public List<RecipeSerializable> recipes = new List<RecipeSerializable>();
    [SerializeField]
    private Inventory _inventory;

    private void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        Debug.Assert(_inventory, "InventoryNotFound_PlayerMissing");
    }

    public void CheckAvailableRecipes(int pRice, int pTomatoe, int pMushroom, int pBurger)
    {
        foreach (RecipeSerializable recipe in recipes)
        {
            recipe.isReady = (pRice >= recipe.rice &&
                              pTomatoe >= recipe.tomatoe &&
                              pMushroom >= recipe.mushroom &&
                              pBurger >= recipe.burger);
        }
        //TO DO: make popup for player to know that a recipe is ready
    }
}