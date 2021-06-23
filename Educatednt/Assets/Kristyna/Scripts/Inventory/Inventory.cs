using UnityEngine;
using Data;


public class Inventory : InventoryBehaviour
{
    public byte stackLimit = 5;
    [HideInInspector]
    public GameObject[] slots;

    private RecipeManager _recipes;

    private int _rice = 0;
    private int _tomatoe = 0;
    private int _mushroom = 0;
    private int _burger = 0;



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
        _rice = data["rice"];
        _tomatoe = data["tomatoe"];
        _mushroom = data["mushroom"];
        _burger = data["burger"];

        Debug.Log($"{this}: Upgraded ingredients are now: rice = {_rice}, tomatoe = {_tomatoe}, mushroom = {_mushroom}, burger = {_burger}");
    }
    private void Awake()
    {
        /// PROBLEM: Game Object Find with tag causes errors because it's not present in the scene
        /// TODO: Use another way to reach out to a RecipeManager. Maybe don't make it a MonoBehaviour,
        /// just a static script. Why does a recipemanager need to be in the scene anyway?
        /// temporary code: {
        if ( !GameObject.FindGameObjectWithTag( "RecipeManager" ) ) {
            Debug.LogWarning( "this script is dependent on a RecipeManager in the scene. It's not there. Handle it" );
            enabled = false;
            return;
        }
        /// }

        _recipes = GameObject.FindGameObjectWithTag("RecipeManager").GetComponent<RecipeManager>();
            Debug.Assert(_recipes, "Recipe Manager with the Recipes script not found by the tag!");
        findSlotsInScene();
        upgradeInventoryUI();
    }
    private void findSlotsInScene()
    {
        slots = GameObject.FindGameObjectsWithTag("Slot");
        Debug.Assert(slots[0], $"No game objects with Slot tag found! num of slots = {slots.Length}");

        if (slots.Length <= 0)
        {
            Debug.LogWarning("Inventory: There are no slots to be found! Add them to the Canvas (InventoryUI > slotEmptyHolder X <- prefab)");
        }
    }
    private void upgradeInventoryUI()
    {
        if (_rice <= 0)
        {
            //Debug.Log($"Inventory: hide rice {_rice}");
            slots[(int)Ingredient.ingredientType.Rice].GetComponent<Slot>().ShowItemInInventory(false);
            slots[(int)Ingredient.ingredientType.Rice].GetComponent<Slot>().SetItemNumber(_rice);
        }
        else
        {
            //Debug.Log($"Inventory: show rice {_rice}");
            slots[(int)Ingredient.ingredientType.Rice].GetComponent<Slot>().ShowItemInInventory(true);
            slots[(int)Ingredient.ingredientType.Rice].GetComponent<Slot>().SetItemNumber(_rice);
        }

        if (_tomatoe <= 0)
        {
            //Debug.Log($"Inventory: hide _tomatoe {_tomatoe}");
            slots[(int)Ingredient.ingredientType.Tomatoe].GetComponent<Slot>().ShowItemInInventory(false);
            slots[(int)Ingredient.ingredientType.Tomatoe].GetComponent<Slot>().SetItemNumber(_tomatoe);
        }
        else
        {
            //Debug.Log($"Inventory: show rice {_tomatoe}");
            slots[(int)Ingredient.ingredientType.Tomatoe].GetComponent<Slot>().ShowItemInInventory(true);
            slots[(int)Ingredient.ingredientType.Tomatoe].GetComponent<Slot>().SetItemNumber(_tomatoe);
        }

        if (_mushroom <= 0)
        {
            //Debug.Log($"Inventory: hide rice {_mushroom}");
            slots[(int)Ingredient.ingredientType.Mushroom].GetComponent<Slot>().ShowItemInInventory(false);
            slots[(int)Ingredient.ingredientType.Mushroom].GetComponent<Slot>().SetItemNumber(_mushroom);
        }
        else
        {
            //Debug.Log($"Inventory: show rice {_mushroom}");
            slots[(int)Ingredient.ingredientType.Mushroom].GetComponent<Slot>().ShowItemInInventory(true);
            slots[(int)Ingredient.ingredientType.Mushroom].GetComponent<Slot>().SetItemNumber(_mushroom);
        }

        if (_burger <= 0)
        {
            //Debug.Log($"Inventory: hide rice {_burger}");
            slots[(int)Ingredient.ingredientType.Burger].GetComponent<Slot>().ShowItemInInventory(false);
            slots[(int)Ingredient.ingredientType.Burger].GetComponent<Slot>().SetItemNumber(_burger);
        }
        else
        {
            //Debug.Log($"Inventory: show rice {_burger}");
            slots[(int)Ingredient.ingredientType.Burger].GetComponent<Slot>().ShowItemInInventory(true);
            slots[(int)Ingredient.ingredientType.Burger].GetComponent<Slot>().SetItemNumber(_burger);
        }

    }
    
    //MANIPULATING INGREDIENTS
    public bool AddItemToInventory(Ingredient.ingredientType pType)
    {
        switch (pType)
        {
            case Ingredient.ingredientType.Rice:
                return checkIngredientAvailability(_rice, Ingredient.ingredientType.Rice);

            case Ingredient.ingredientType.Tomatoe:
                return checkIngredientAvailability(_tomatoe, Ingredient.ingredientType.Tomatoe);

            case Ingredient.ingredientType.Mushroom:
                return checkIngredientAvailability(_mushroom, Ingredient.ingredientType.Mushroom);

            case Ingredient.ingredientType.Burger:
                return checkIngredientAvailability(_burger, Ingredient.ingredientType.Burger);
        }
        return false;
    }
    private bool checkIngredientAvailability(int pIngredient, Ingredient.ingredientType pType)
    {
        if (pIngredient < stackLimit)
        {
            if(pIngredient <= 0)
            {
                slots[(int)pType].GetComponent<Slot>().ShowItemInInventory(true);//show the inventoryItemPrefab & number in slot text
            }

            IncreaseIngredient(pType);
            upgradeInventoryUI();

            if (_rice > 0 || _tomatoe > 0 || _mushroom > 0 || _burger > 0) _recipes.CheckAvailableRecipes(_rice, _tomatoe, _mushroom, _burger);

            return true;
        }
        else
        {
            Debug.Log($"Inventory is full for {pType} ingredient!");
            return false;
        }
    }
    private void IncreaseIngredient(Ingredient.ingredientType pType)
    {
        //Increase ingredient in inventory + update UI + save data
        switch (pType)
        {
            case Ingredient.ingredientType.Rice:
                data["rice"] = ++_rice;
                slots[(int)pType].GetComponent<Slot>().SetItemNumber(_rice);
                break;
            case Ingredient.ingredientType.Tomatoe:
                data["tomatoe"] = ++_tomatoe;
                slots[(int)pType].GetComponent<Slot>().SetItemNumber(_tomatoe);
                break;
            case Ingredient.ingredientType.Mushroom:
                data["mushroom"] = ++_mushroom;
                slots[(int)pType].GetComponent<Slot>().SetItemNumber(_mushroom);
                break;
            case Ingredient.ingredientType.Burger:
                data["burger"] = ++_burger;
                slots[(int)pType].GetComponent<Slot>().SetItemNumber(_burger);
                break;
        }
    }
}
