using UnityEngine;
using Data;


public class Inventory : InventoryBehaviour {
    public byte stackLimit = 5;
    [HideInInspector]
    public GameObject[] slots;

    private RecipeManager _recipes;

    private int _rice = 0;
    private int _tomato = 0;
    private int _mushroom = 0;
    private int _burger = 0;



    //SETUP
    private void Update() {
        if ( !isUpgraded && isLoaded ) {
            // UPGRADE
            Upgrade();
        }
    }


    protected override void Upgrade() {
        base.Upgrade();
        _rice = data[ Data.RICE ];
        _tomato = data[ Data.TOMATO ];
        _mushroom = data[ Data.MUSHROOM ];
        _burger = data[ Data.BURGER ];

        upgradeInventoryUI();
        
        Debug.Log( $"{this}: Upgraded ingredients are now: rice = {_rice}, tomato = {_tomato}, mushroom = {_mushroom}, burger = {_burger}" );
    }


    protected override void Awake() {
        base.Awake();

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

        _recipes = GameObject.FindGameObjectWithTag( "RecipeManager" ).GetComponent<RecipeManager>();
        Debug.Assert( _recipes, "Recipe Manager with the Recipes script not found by the tag!" );
        findSlotsInScene();
        upgradeInventoryUI();
    }


    private void findSlotsInScene() {
        slots = GameObject.FindGameObjectsWithTag( "Slot" );
        Debug.Assert( slots[ 0 ], $"No game objects with Slot tag found! num of slots = {slots.Length}" );

        if ( slots.Length <= 0 ) {
            Debug.LogWarning( "Inventory: There are no slots to be found! Add them to the Canvas (InventoryUI > slotEmptyHolder X <- prefab)" );
        }
    }


    private void upgradeInventoryUI() {
        if ( _rice <= 0 ) {
            //Debug.Log($"Inventory: hide rice {_rice}");
            slots[ ( int )Ingredient.ingredientType.Rice ].GetComponent<Slot>().ShowItemInInventory( false );
            slots[ ( int )Ingredient.ingredientType.Rice ].GetComponent<Slot>().SetItemNumber( _rice );
        }
        else {
            //Debug.Log($"Inventory: show rice {_rice}");
            slots[ ( int )Ingredient.ingredientType.Rice ].GetComponent<Slot>().ShowItemInInventory( true );
            slots[ ( int )Ingredient.ingredientType.Rice ].GetComponent<Slot>().SetItemNumber( _rice );
        }

        if ( _tomato <= 0 ) {
            //Debug.Log($"Inventory: hide tomato {_tomato}");
            slots[ ( int )Ingredient.ingredientType.Tomato ].GetComponent<Slot>().ShowItemInInventory( false );
            slots[ ( int )Ingredient.ingredientType.Tomato ].GetComponent<Slot>().SetItemNumber( _tomato );
        }
        else {
            //Debug.Log($"Inventory: show tomato {_tomato}");
            slots[ ( int )Ingredient.ingredientType.Tomato ].GetComponent<Slot>().ShowItemInInventory( true );
            slots[ ( int )Ingredient.ingredientType.Tomato ].GetComponent<Slot>().SetItemNumber( _tomato );
        }

        if ( _mushroom <= 0 ) {
            //Debug.Log($"Inventory: hide mushroom {_mushroom}");
            slots[ ( int )Ingredient.ingredientType.Mushroom ].GetComponent<Slot>().ShowItemInInventory( false );
            slots[ ( int )Ingredient.ingredientType.Mushroom ].GetComponent<Slot>().SetItemNumber( _mushroom );
        }
        else {
            //Debug.Log($"Inventory: show mushroom {_mushroom}");
            slots[ ( int )Ingredient.ingredientType.Mushroom ].GetComponent<Slot>().ShowItemInInventory( true );
            slots[ ( int )Ingredient.ingredientType.Mushroom ].GetComponent<Slot>().SetItemNumber( _mushroom );
        }

        if ( _burger <= 0 ) {
            //Debug.Log($"Inventory: hide burger {_burger}");
            slots[ ( int )Ingredient.ingredientType.Burger ].GetComponent<Slot>().ShowItemInInventory( false );
            slots[ ( int )Ingredient.ingredientType.Burger ].GetComponent<Slot>().SetItemNumber( _burger );
        }
        else {
            //Debug.Log($"Inventory: show burger {_burger}");
            slots[ ( int )Ingredient.ingredientType.Burger ].GetComponent<Slot>().ShowItemInInventory( true );
            slots[ ( int )Ingredient.ingredientType.Burger ].GetComponent<Slot>().SetItemNumber( _burger );
        }
    }


    //MANIPULATING INGREDIENTS
    public bool AddItemToInventory( Ingredient.ingredientType pType ) {
        switch ( pType ) {
            case Ingredient.ingredientType.Rice:
                return checkIngredientAvailability( _rice, Ingredient.ingredientType.Rice );

            case Ingredient.ingredientType.Tomato:
                return checkIngredientAvailability( _tomato, Ingredient.ingredientType.Tomato );

            case Ingredient.ingredientType.Mushroom:
                return checkIngredientAvailability( _mushroom, Ingredient.ingredientType.Mushroom );

            case Ingredient.ingredientType.Burger:
                return checkIngredientAvailability( _burger, Ingredient.ingredientType.Burger );
        }
        return false;
    }


    private bool checkIngredientAvailability( int pIngredient, Ingredient.ingredientType pType ) {
        if ( pIngredient < stackLimit ) {
            if ( pIngredient <= 0 ) {
                slots[ ( int )pType ].GetComponent<Slot>().ShowItemInInventory( true );//show the inventoryItemPrefab & number in slot text
            }

            IncreaseIngredient( pType );
            upgradeInventoryUI();

            if ( _rice > 0 || _tomato > 0 || _mushroom > 0 || _burger > 0 ) _recipes.CheckAvailableRecipes( _rice, _tomato, _mushroom, _burger );

            return true;
        }
        else {
            Debug.Log( $"Inventory is full for {pType} ingredient!" );
            return false;
        }
    }


    private void IncreaseIngredient( Ingredient.ingredientType pType ) {
        //Increase ingredient in inventory + update UI + save data
        switch ( pType ) {
            case Ingredient.ingredientType.Rice:
                data[ Data.RICE ] = ++_rice;
                slots[ ( int )pType ].GetComponent<Slot>().SetItemNumber( _rice );
                break;
            case Ingredient.ingredientType.Tomato:
                data[ Data.TOMATO ] = ++_tomato;
                slots[ ( int )pType ].GetComponent<Slot>().SetItemNumber( _tomato );
                break;
            case Ingredient.ingredientType.Mushroom:
                data[ Data.MUSHROOM ] = ++_mushroom;
                slots[ ( int )pType ].GetComponent<Slot>().SetItemNumber( _mushroom );
                break;
            case Ingredient.ingredientType.Burger:
                data[ Data.BURGER ] = ++_burger;
                slots[ ( int )pType ].GetComponent<Slot>().SetItemNumber( _burger );
                break;
        }
    }


    public override void Load( PersistentData persistentData ) {
        base.Load( persistentData );

        Debug.Log( "Loading inventory.." );

        if ( !persistentData.TryGetIntData( Data.BURGER.ToString(), out _burger ) ) {
            Debug.LogError( $"{this} Can't parse {Data.BURGER}, not an int." );
        }
        if ( !persistentData.TryGetIntData( Data.RICE.ToString(), out _rice ) ) {
            Debug.LogError( $"{this} Can't parse {Data.RICE}, not an int." );
        }
        if ( !persistentData.TryGetIntData( Data.MUSHROOM.ToString(), out _mushroom ) ) {
            Debug.LogError( $"{this} Can't parse {Data.MUSHROOM}, not an int." );
        }
        if ( !persistentData.TryGetIntData( Data.TOMATO.ToString(), out _tomato ) ) {
            Debug.LogError( $"{this} Can't parse {Data.TOMATO}, not an int." );
        }

        data[ Data.BURGER ] = _burger;
        data[ Data.RICE ] = _rice;
        data[ Data.MUSHROOM ] = _mushroom;
        data[ Data.TOMATO ] = _tomato;

        Debug.Log( $"{this}: Loaded burgers to {_burger}." );
        Debug.Log( $"{this}: Loaded rice to {_rice}." );
        Debug.Log( $"{this}: Loaded mushrooms to {_mushroom}." );
        Debug.Log( $"{this}: Loaded tomatoes to {_tomato}." );
    }


    public override void Save( PersistentData persistentData ) {
        Debug.Log( "Saving inventory.." );

        persistentData.SetIntData( Data.BURGER.ToString(), _burger );
        persistentData.SetIntData( Data.RICE.ToString(), _rice );
        persistentData.SetIntData( Data.MUSHROOM.ToString(), _mushroom );
        persistentData.SetIntData( Data.TOMATO.ToString(), _tomato );

        Debug.Log( $"{this}: Saved burgers to {_burger}." );
        Debug.Log( $"{this}: Saved rice to {_rice}." );
        Debug.Log( $"{this}: Saved mushrooms to {_mushroom}." );
        Debug.Log( $"{this}: Saved tomatoes to {_tomato}." );
    }
}