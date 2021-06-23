using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [Header("0-rice, 1 - toamtoe, 2 - mushroom, 3 - burger")]
    public Sprite[] pickupSprites = new Sprite[4];
    public GameObject _inventoryItemPrefab;
    public GameObject _itemNumber;

    public int _slotNumber;
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
        _itemNumber = this.transform.GetChild(1).gameObject;
        Debug.Assert(_itemNumber, $"{this.name} has no itemNumber text.");
        //slot item sprite
        _inventoryItemPrefab = this.transform.GetChild(0).gameObject;
        Debug.Assert(_inventoryItemPrefab, $"Slot {_slotNumber} does not have any second child! (It has to be the Inventory Item Prefab!)");
        assignSpriteToInventoryItemPrefabChild();
    }
    private void assignSpriteToInventoryItemPrefabChild()
    {
        switch (_slotNumber)
        {
            case 0:
                _inventoryItemPrefab.GetComponent<Image>().sprite = pickupSprites[0];
                //Debug.Log($"Slot: Slot {_slotNumber} has inventory item prefab = {0}");
                break;
            case 1:
                _inventoryItemPrefab.GetComponent<Image>().sprite = pickupSprites[1];
                //Debug.Log($"Slot: Slot {_slotNumber} has inventory item prefab = {1}");
                break;
            case 2:
                _inventoryItemPrefab.GetComponent<Image>().sprite = pickupSprites[2];
                //Debug.Log($"Slot: Slot {_slotNumber} has inventory item prefab = {2}");
                break;
            case 3:
                _inventoryItemPrefab.GetComponent<Image>().sprite = pickupSprites[3];
                //Debug.Log($"Slot: Slot {_slotNumber} has inventory item prefab = {3}");
                break;
        }
    }
    private void checkIfSlotNumberOutOfInventorySlotArrayRange()
    {
        if (transform.childCount <= 0 && _slotNumber > inventory.slots.Length)
        {
            Debug.LogError("slotNumber " + _slotNumber + " is out of range of the number of slots available in inventory! Change the slot's name. (e.g. Slot0, Slot1, Slot2 etc.)");
        }
    }

    //MANIPULATE SLOT VARIABLES
    public void SetItemNumber(int pNum)
    {
        _actualItemNumber = pNum;
        _itemNumber.GetComponent<Text>().text = _actualItemNumber.ToString();
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
        _itemNumber.gameObject.SetActive(pShow);
    }
    private void ShowInventoryItemPrefab(bool pShow)
    {
        _inventoryItemPrefab.gameObject.SetActive(pShow);
        //Debug.Log($"Slot: Show inventory item prefab = {pShow}");
    }
    private void loadIngredientSpritesFromFolder()
    {
        pickupSprites = Resources.LoadAll<Sprite>("Sprites/IngredientSprites");
        Debug.Assert(pickupSprites[0], $"Sprites for ingredients in folder Assets/Resources/Sprites/IngredientSprites not found!");
    }
    
}
