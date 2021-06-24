using System;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [Header("0 - rice, 1 - tomato, 2 - mushroom, 3 - burger")]
    public Sprite[] pickupSprites = new Sprite[4];
    public GameObject inventoryItemPrefab;
    public GameObject itemNumber;
    public int slotNumber;

    private GameObject _inventoryHolder;
    private Inventory inventory;
    private int _actualItemNumber;
    
    //SETUP
    private void Start()
    {
        //SETUP: sprites, slot number
        loadIngredientSpritesFromFolder();
        checkIfSlotNumberOutOfInventorySlotArrayRange();

        //REFERENCE TO OBJECTS:
        //inventory holder + script
        _inventoryHolder = GameObject.FindGameObjectWithTag("Player");
        Debug.Assert(_inventoryHolder, $"{this.name} could not find gameObject with tag Player!");
        inventory = GameObject.FindGameObjectWithTag(_inventoryHolder.gameObject.tag).GetComponent<Inventory>();
        Debug.Assert(inventory, $"{this.name}: The player does not have an inventory!");
        //number of items
        itemNumber = this.transform.GetChild(1).gameObject;
        Debug.Assert(itemNumber, $"{this.name} has no itemNumber text.");
        //slot item sprite
        inventoryItemPrefab = this.transform.GetChild(0).gameObject;
        Debug.Assert(inventoryItemPrefab, $"Slot {slotNumber} does not have any second child! (It has to be the Inventory Item Prefab!)");
        assignSpriteToInventoryItemPrefabChild();
    }
    private void assignSpriteToInventoryItemPrefabChild() {
        try {
            inventoryItemPrefab.GetComponent<Image>().sprite = pickupSprites[ slotNumber ];
        }
        catch ( Exception e ) {
            Debug.LogError( $"{this.name} Failed. Error message: {e.Message}." );
        }
    }
    private void checkIfSlotNumberOutOfInventorySlotArrayRange()
    {
        if (transform.childCount <= 0 && slotNumber > inventory.slots.Length)
        {
            Debug.LogError("slotNumber " + slotNumber + " is out of range of the number of slots available in inventory! Change the slot's name. (e.g. Slot0, Slot1, Slot2 etc.)");
        }
    }

    //MANIPULATE SLOT VARIABLES
    public void SetItemNumber(int pNum)
    {
        _actualItemNumber = pNum;
        itemNumber.GetComponent<Text>().text = _actualItemNumber.ToString();
        //Debug.Log($"Slot: Set Item number = {_actualItemNumber}");
    }
    public void ShowItemInInventory(bool pShow)
    {
        //Debug.Log($"Slot: Show item in inventory = {pShow}");
        ShowItemNumber(pShow);
        ShowInventoryItemPrefab(pShow);
    }
    private void ShowItemNumber(bool pShow)
    {
        itemNumber.gameObject.SetActive(pShow);
    }
    private void ShowInventoryItemPrefab(bool pShow)
    {
        inventoryItemPrefab.SetActive(pShow);
        //Debug.Log($"Slot: Show inventory item prefab = {pShow}");
    }
    private void loadIngredientSpritesFromFolder()
    {
        pickupSprites = Resources.LoadAll<Sprite>("Sprites/IngredientSprites");
        Debug.Assert(pickupSprites[0], $"Sprites for ingredients in folder Assets/Resources/Sprites/IngredientSprites not found!");
    }
    
}
