using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecipeManager : MonoBehaviour
{
    [SerializeField]
    [Header("0- soup, 1 - pilaf, 2 - friedRice, 3 - burger")]
    public List<RecipeSerializable> recipes = new List<RecipeSerializable>();

    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private PopupReadyRecipe _recipePopup;
    private bool[] wasPopupDisplayed = new bool[] { false, false, false, false};

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "KitchenScene")
        {
            if (!GameObject.FindGameObjectWithTag("Player"))
            {
                Debug.LogError("Failed to find game object with tag Player in the scene!");
                Destroy(this);
                return;
            }

            _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
            Debug.Assert(_inventory, "InventoryNotFound_PlayerMissing");

            wasPopupDisplayed = new bool[recipes.Count];
        }
    }

    public void CheckAvailableRecipes(int pRice, int pTomatoe, int pMushroom, int pBurger)
    {
        for (int index = 0; index < recipes.Count; ++index)
        {
            recipes[index].isReady = (pRice >= recipes[index].rice &&
                              pTomatoe >= recipes[index].tomatoe &&
                              pMushroom >= recipes[index].mushroom &&
                              pBurger >= recipes[index].burger);

            if (recipes[index].isReady)
            {
                if (!wasPopupDisplayed[index])
                {
                    Debug.Log($"Recipe {recipes[index].name} ready!");
                    _recipePopup.ActivatePopup(recipes[index].name);

                    wasPopupDisplayed[index] = true;
                }
            }
        }
    }
}