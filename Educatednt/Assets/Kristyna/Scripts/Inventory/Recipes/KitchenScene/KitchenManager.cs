using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Data;

public class KitchenManager : KitchenManagerBehaviour
{
    [Space(10)] public Button[] recipeButtons;
    [Space(10)] public RecipeManager recipeManager;
    public AbilityPopup abilityPopup;

    private int _rice = 3;
    private int _tomatoe = 3;
    private int _mushroom = 3;
    private int _burger = 2;

    void Start()
    {
        if (!GameObject.FindGameObjectWithTag("RecipeManager"))
        {
            Debug.LogError("Failed to find game object with tag RecipeManager in the scene!");
            Destroy(this);
            return;
        }
        else
        {
            recipeManager = GameObject.FindGameObjectWithTag("RecipeManager").GetComponent<RecipeManager>();
            Debug.Assert(recipeManager, "RecipeManager object does not contain RecipeManager script!");
        }

        DecideActiveRecipeButtons();

        Debug.Log($"Inventory items: {_rice}, {_tomatoe}, {_mushroom}, {_burger}");
    }
    private void DecideActiveRecipeButtons()
    {
        recipeManager.CheckAvailableRecipes(_rice, _tomatoe, _mushroom, _burger);

        for(int index = 0; index < recipeButtons.Length; ++index)
        {
            if (recipeManager.recipes[index].isReady)
            {
                recipeButtons[index].gameObject.SetActive(true);
            }
            else
            {
                recipeButtons[index].gameObject.SetActive(false);
            }
        }
    }

    protected override void Upgrade()
    {
        base.Upgrade();
        _rice = data["rice"];
        _tomatoe = data["tomatoe"];
        _mushroom = data["mushroom"];
        _burger = data["burger"];

        Debug.Log($"{this}: Upgraded ingredients are now: rice = {_rice}, tomatoe = {_tomatoe}, mushroom = {_mushroom}, burger = {_burger}");
    }
    private void Update()
    {
        if (!isUpgraded && isLoaded)
        {
            // UPGRADE
            Upgrade();
        }
    }

    public void CookRecipe(int pRecipeNumber)
    {
        DeleteIngredienceFromInventory(recipeManager.recipes[pRecipeNumber].rice, recipeManager.recipes[pRecipeNumber].tomatoe, recipeManager.recipes[pRecipeNumber].mushroom, recipeManager.recipes[pRecipeNumber].burger);
        HideAllRecipeButtons();
        abilityPopup.ActivatePopup(this.GetComponent<KingsHappiness>().happinessLvl);
    }
    private void HideAllRecipeButtons()
    {
        foreach (Button button in recipeButtons)
        {
             button.gameObject.SetActive(false);
        }
    }
    private void DeleteIngredienceFromInventory(byte pRice, byte pTomatoe, byte pMushroom, byte pBurger)
    {
        //change data:
        _rice -= pRice;
        _tomatoe -= pTomatoe;
        _mushroom -= pMushroom;
        _burger -= pBurger;

        //save data:
        data["rice"] = _rice;
        data["tomatoe"] = _tomatoe;
        data["mushroom"] = _mushroom;
        data["burger"] = _burger;

        Debug.Log($"Recipe cooked! Inventory now: {_rice}, {_tomatoe}, {_mushroom}, {_burger}");
    }
    public void ChangeBackToScene(int pSceneNumber)
    {
        SceneManager.LoadScene(pSceneNumber);
    }
}