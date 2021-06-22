using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Data;

public class KitchenManager : KitchenManagerBehaviour
{
    [Header("From best to worst.")]
    public Sprite[] kingStates = new Sprite[4];
    public byte startState = 0;
    [Space(10)] public Image kingStateImage;
    [Space(10)] public Button[] recipeButtons;

    private RecipeManager recipeManager;
    private bool _upgraded = false;

    private int _rice = 0;
    private int _tomatoe = 0;
    private int _mushroom = 0;
    private int _burger = 0;

    void Start()
    {
        kingStateImage.GetComponent<Image>().sprite = kingStates[startState];
        recipeManager = GameObject.FindGameObjectWithTag("RecipeManager").GetComponent<RecipeManager>();
        Debug.Assert(recipeManager, "RecipeManager object with script was not found in the scene!");

        //TO DO: display buttons based on what recipe is ready

    }

    //RECIPE MANAGER
    //Check recipes - loop through them

    private void Upgrade()
    {
        _rice = data["rice"];
        _tomatoe = data["tomatoe"];
        _mushroom = data["mushroom"];
        _burger = data["burger"];

        _upgraded = true;
        Debug.Log($"{this}: Upgraded ingredients are now: rice = {_rice}, tomatoe = {_tomatoe}, mushroom = {_mushroom}, burger = {_burger}");
    }
    private void Update()
    {
        if (!_upgraded && isLoaded)
        {
            // UPGRADE
            Upgrade();
        }
    }

    public void CookRecipe(int pRecipeNumber)
    {
        DeleteIngredienceFromInventory(recipeManager.recipes[pRecipeNumber].rice, recipeManager.recipes[pRecipeNumber].tomatoe, recipeManager.recipes[pRecipeNumber].mushroom, recipeManager.recipes[pRecipeNumber].burger);
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