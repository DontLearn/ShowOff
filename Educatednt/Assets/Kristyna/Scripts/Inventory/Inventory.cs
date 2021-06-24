using System.Linq;
using UnityEngine;
using Data;


public class Inventory : InventoryBehaviour {
    //[HideInInspector]
    //public int HOUSTON = 0;


    public int stackLimit = 6;
    [HideInInspector]
    public Slot[] slots;

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
        GameObject[] goArray = GameObject.FindGameObjectsWithTag( "Slot" );

        Slot[] slotArray = new Slot[ goArray.Length ];

        for ( int i = 0; i < slotArray.Length; ++i ) {
            slotArray[ i ] = goArray[ i ].GetComponent<Slot>();
            if ( null == slotArray[ i ] ) {
                Debug.LogError( $"{goArray[ i ].name} does not have a Slot component." );
            }
        }

        //Slot[] testArray = ( Slot[] )slotArray.Clone();
        slots = slotArray.OrderBy( x => x.slotNumber ).ToArray();
        /*if ( testArray[0]!=slots[0] || testArray[1]!=slots[1] || testArray[2]!=slots[2] || testArray[3]!= slots[3] ) {
            HOUSTON = 1;
        }
        else {
            HOUSTON = 2;
        }*/


        Debug.Assert( slots[ 0 ], $"No game objects with Slot tag found! num of slots = {slots.Length}" );

        if ( slots.Length <= 0 ) {
            Debug.LogWarning( "Inventory: There are no slots to be found! Add them to the Canvas (InventoryUI > slotEmptyHolder X <- prefab)" );
        }
        else {
            for ( int i = 0; i < slots.Length; ++i ) {
                Debug.Log( $"{this}: Loaded slot [{i}] with number [{slots[ i ].slotNumber}]." );
            }
        }
    }


    private void upgradeInventoryUI() {
        if ( null != slots && ( int )Ingredient.ingredientType.RICE >= 0 && ( int )Ingredient.ingredientType.RICE < slots.Length ) {
            if ( _rice <= 0 ) {
                //Debug.Log($"Inventory: hide rice {_rice}");
                slots[ ( int )Ingredient.ingredientType.RICE ].ShowItemInInventory( false );
                slots[ ( int )Ingredient.ingredientType.RICE ].SetItemNumber( _rice );
            }
            else {
                //Debug.Log($"Inventory: show rice {_rice}");
                slots[ ( int )Ingredient.ingredientType.RICE ].ShowItemInInventory( true );
                slots[ ( int )Ingredient.ingredientType.RICE ].SetItemNumber( _rice );
            }
        }

        if ( null != slots && ( int )Ingredient.ingredientType.TOMATO >= 0 && ( int )Ingredient.ingredientType.TOMATO < slots.Length ) {
            if ( _tomato <= 0 ) {
                //Debug.Log($"Inventory: hide tomato {_tomato}");
                slots[ ( int )Ingredient.ingredientType.TOMATO ].ShowItemInInventory( false );
                slots[ ( int )Ingredient.ingredientType.TOMATO ].SetItemNumber( _tomato );
            }
            else {
                //Debug.Log($"Inventory: show tomato {_tomato}");
                slots[ ( int )Ingredient.ingredientType.TOMATO ].ShowItemInInventory( true );
                slots[ ( int )Ingredient.ingredientType.TOMATO ].SetItemNumber( _tomato );
            }
        }


        if ( null != slots && ( int )Ingredient.ingredientType.MUSHROOM >= 0 && ( int )Ingredient.ingredientType.MUSHROOM < slots.Length ) {
            if ( _mushroom <= 0 ) {
                //Debug.Log($"Inventory: hide mushroom {_mushroom}");
                slots[ ( int )Ingredient.ingredientType.MUSHROOM ].ShowItemInInventory( false );
                slots[ ( int )Ingredient.ingredientType.MUSHROOM ].SetItemNumber( _mushroom );
            }
            else {
                //Debug.Log($"Inventory: show mushroom {_mushroom}");
                slots[ ( int )Ingredient.ingredientType.MUSHROOM ].ShowItemInInventory( true );
                slots[ ( int )Ingredient.ingredientType.MUSHROOM ].SetItemNumber( _mushroom );
            }
        }

        if ( null != slots && ( int )Ingredient.ingredientType.BURGER >= 0 && ( int )Ingredient.ingredientType.BURGER < slots.Length ) {
            if ( _burger <= 0 ) {
                //Debug.Log($"Inventory: hide burger {_burger}");
                slots[ ( int )Ingredient.ingredientType.BURGER ].ShowItemInInventory( false );
                slots[ ( int )Ingredient.ingredientType.BURGER ].SetItemNumber( _burger );
            }
            else {
                //Debug.Log($"Inventory: show burger {_burger}");
                slots[ ( int )Ingredient.ingredientType.BURGER ].ShowItemInInventory( true );
                slots[ ( int )Ingredient.ingredientType.BURGER ].SetItemNumber( _burger );
            }
        }
    }


    //MANIPULATING INGREDIENTS
    public bool AddItemToInventory( Ingredient.ingredientType pType ) {
        switch ( pType ) {
            case Ingredient.ingredientType.RICE:
                return checkIngredientAvailability( _rice, Ingredient.ingredientType.RICE );

            case Ingredient.ingredientType.TOMATO:
                return checkIngredientAvailability( _tomato, Ingredient.ingredientType.TOMATO );

            case Ingredient.ingredientType.MUSHROOM:
                return checkIngredientAvailability( _mushroom, Ingredient.ingredientType.MUSHROOM );

            case Ingredient.ingredientType.BURGER:
                return checkIngredientAvailability( _burger, Ingredient.ingredientType.BURGER );
        }
        return false;
    }


    private bool checkIngredientAvailability( int pIngredient, Ingredient.ingredientType pType ) {
        if ( pIngredient < stackLimit ) {
            if ( pIngredient <= 0 ) {
                int intType = ( int )pType;
                if ( null != slots && intType >= 0 && intType < slots.Length ) {
                    Slot slotComponent = slots[ intType ];
                    if ( null != slotComponent ) {
                        slotComponent.ShowItemInInventory( true );//show the inventoryItemPrefab & number in slot
                    }
                }
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
        int intType = ( int )pType;
        switch ( pType ) {
            case Ingredient.ingredientType.RICE:
                data[ Data.RICE ] = ++_rice;
                if ( null != slots && intType >= 0 && intType < slots.Length ) {
                    Slot slotComponent = slots[ intType ];
                    if ( null != slotComponent )
                        slotComponent.SetItemNumber( _rice );
                }
                else {
                    Debug.LogError( $"{this}: out of range or slots not existing." );
                }
                break;
            case Ingredient.ingredientType.TOMATO:
                data[ Data.TOMATO ] = ++_tomato;
                if ( null != slots && intType >= 0 && intType < slots.Length ) {
                    Slot slotComponent = slots[ intType ];
                    if ( null != slotComponent )
                        slotComponent.SetItemNumber( _tomato );
                }
                else {
                    Debug.LogError( $"{this}: out of range or slots not existing." );
                }
                break;
            case Ingredient.ingredientType.MUSHROOM:
                data[ Data.MUSHROOM ] = ++_mushroom;
                if ( null != slots && intType >= 0 && intType < slots.Length ) {
                    Slot slotComponent = slots[ intType ];
                    if ( null != slotComponent )
                        slotComponent.SetItemNumber( _mushroom );
                }
                else {
                    Debug.LogError( $"{this}: out of range or slots not existing." );
                }
                break;
            case Ingredient.ingredientType.BURGER:
                data[ Data.BURGER ] = ++_burger;
                if ( null != slots && intType >= 0 && intType < slots.Length ) {
                    Slot slotComponent = slots[ intType ];
                    if ( null != slotComponent )
                        slotComponent.SetItemNumber( _burger );
                }
                else {
                    Debug.LogError( $"{this}: out of range or slots not existing." );
                }
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