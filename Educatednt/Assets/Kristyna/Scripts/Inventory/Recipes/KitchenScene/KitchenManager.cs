using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Data;

public class KitchenManager : InventoryBehaviour
{
    public Button[] recipeButtons;
    [Space(10)]
    public RecipeManager recipeManager;
    [Space(10)] 
    public AbilityPopup abilityPopup;
    [Space(10)]
    public GameObject backButton;



    private int _rice = 5;
    private int _tomato = 5;
    private int _mushroom = 5;
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
        showBackButton(false);
        Debug.Log($"Inventory items: {_rice}, {_tomato}, {_mushroom}, {_burger}");
    }
    private void DecideActiveRecipeButtons()
    {
        recipeManager.CheckAvailableRecipes(_rice, _tomato, _mushroom, _burger);

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

    //SETUP
    private void Update()
    {
        if (!isUpgraded && isLoaded)
        {
            // UPGRADE
            Upgrade();
        }
    }


    protected override void Upgrade()
    {
        base.Upgrade();
        _rice = data[Data.RICE];
        _tomato = data[Data.TOMATO];
        _mushroom = data[Data.MUSHROOM];
        _burger = data[Data.BURGER];


        Debug.Log($"{this}: Upgraded ingredients are now: rice = {_rice}, tomato = {_tomato}, mushroom = {_mushroom}, burger = {_burger}");
    }

    public void CookRecipe(int pRecipeNumber)
    {
        DeleteIngredienceFromInventory(recipeManager.recipes[pRecipeNumber].rice, recipeManager.recipes[pRecipeNumber].tomatoe, recipeManager.recipes[pRecipeNumber].mushroom, recipeManager.recipes[pRecipeNumber].burger);
        HideAllRecipeButtons();
        abilityPopup.ActivatePopup(this.GetComponent<KingsHappiness>().happinessLvl);
        showBackButton(true);
    }
    private void showBackButton(bool pShow)
    {
        backButton.gameObject.SetActive(pShow);
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
        _tomato -= pTomatoe;
        _mushroom -= pMushroom;
        _burger -= pBurger;

        //save data:
        data[Data.RICE] = _rice;
        data[Data.TOMATO] = _tomato;
        data[Data.MUSHROOM] = _mushroom;
        data[Data.BURGER] = _burger;

        Debug.Log($"Recipe cooked! Inventory now: {_rice}, {_tomato}, {_mushroom}, {_burger}");
    }
    public void ChangeBackToScene(int pSceneNumber)
    {
        SceneChanger.Instance.ChangeScene(pSceneNumber);
    }

    public override void Load(PersistentData persistentData)
    {
        base.Load(persistentData);

        Debug.Log("Loading inventory..");

        if (!persistentData.TryGetIntData(Data.BURGER.ToString(), out _burger))
        {
            Debug.LogError($"{this} Can't parse {Data.BURGER}, not an int.");
        }
        if (!persistentData.TryGetIntData(Data.RICE.ToString(), out _rice))
        {
            Debug.LogError($"{this} Can't parse {Data.RICE}, not an int.");
        }
        if (!persistentData.TryGetIntData(Data.MUSHROOM.ToString(), out _mushroom))
        {
            Debug.LogError($"{this} Can't parse {Data.MUSHROOM}, not an int.");
        }
        if (!persistentData.TryGetIntData(Data.TOMATO.ToString(), out _tomato))
        {
            Debug.LogError($"{this} Can't parse {Data.TOMATO}, not an int.");
        }

        data[Data.BURGER] = _burger;
        data[Data.RICE] = _rice;
        data[Data.MUSHROOM] = _mushroom;
        data[Data.TOMATO] = _tomato;

        Debug.Log($"{this}: Loaded burgers to {_burger}.");
        Debug.Log($"{this}: Loaded rice to {_rice}.");
        Debug.Log($"{this}: Loaded mushrooms to {_mushroom}.");
        Debug.Log($"{this}: Loaded tomatoes to {_tomato}.");
    }


    public override void Save(PersistentData persistentData)
    {
        Debug.Log("Saving inventory..");

        persistentData.SetIntData(Data.BURGER.ToString(), _burger);
        persistentData.SetIntData(Data.RICE.ToString(), _rice);
        persistentData.SetIntData(Data.MUSHROOM.ToString(), _mushroom);
        persistentData.SetIntData(Data.TOMATO.ToString(), _tomato);

        Debug.Log($"{this}: Saved burgers to {_burger}.");
        Debug.Log($"{this}: Saved rice to {_rice}.");
        Debug.Log($"{this}: Saved mushrooms to {_mushroom}.");
        Debug.Log($"{this}: Saved tomatoes to {_tomato}.");
    }
}