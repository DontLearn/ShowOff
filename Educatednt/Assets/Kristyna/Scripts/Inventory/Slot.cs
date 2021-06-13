using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private int _slotNumber;
    private GameObject _inventoryHolder;
    private Inventory inventory;
    private GameObject _itemNumber;
    private byte _actualItemNumber;
    private void Start()
    {
        assignSlotNumberFromName();

        _inventoryHolder = GameObject.FindGameObjectWithTag("Player");
                Debug.Assert(_inventoryHolder, $"{this.name} could not find gameObject with tag Player!");
        inventory = GameObject.FindGameObjectWithTag(_inventoryHolder.gameObject.tag).GetComponent<Inventory>();
                Debug.Assert(inventory, $"{this.name}: The player does not have an inventory!");
        _itemNumber = this.transform.GetChild(0).gameObject;
        _itemNumber.gameObject.SetActive(false);
                Debug.Assert(_itemNumber, $"{this.name} has no itemNumber text.");

        checkIfSlotNumberNotOutOfInventorySlotArrayRange();
    }
    private void assignSlotNumberFromName()
    {
        //Assign number to slot from its name:
        string number = this.name;
        number = number.Remove(0, 4);
        _slotNumber = int.Parse(number);
    }
    private void checkIfSlotNumberNotOutOfInventorySlotArrayRange()
    {
        //Check if inventory has enough room for all slots in its slots[]
        //so slots can store their items in the inventory:
        if (transform.childCount <= 0)
        {
            if (_slotNumber < inventory.slots.Length)
            {
                inventory.isFull[_slotNumber] = false;
            }
            else
            {
                Debug.LogError("slotNumber " + _slotNumber + " is out of range of the number of slots available in inventory! Change the slot's name. (e.g. Slot0, Slot1, Slot2 etc.)");
            }
        }
    }
    public void SlotIsEmpty()
    {
        //TO DO: FIX DELETING ALSO THE BUTTON ITEM

        for(int child = 0; child < this.transform.childCount; child++)
        {
            if (child == 0)//item image
            {
                this.transform.GetChild(0).gameObject.SetActive(false);
                Debug.Log($"Item image of {this.name} got destroyed!");
            }
            else//number of items
            {
                _itemNumber.gameObject.SetActive(false);
                Debug.Log($"Item bumer of {this.name} got hidden!");
            }
            
        }
    }
    public void PlusOneItem()
    {
        ++_actualItemNumber;
        _itemNumber.GetComponent<Text>().text = _actualItemNumber.ToString();
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void MinusItems(byte pNumberToRemove)
    {
        _actualItemNumber -= pNumberToRemove;
        _itemNumber.GetComponent<Text>().text = _actualItemNumber.ToString();
        SlotIsEmpty();
    }
    public void showItemNumber()
    {
        _itemNumber.gameObject.SetActive(true);
        _itemNumber.transform.SetAsLastSibling();
        //TO DO: DELETE OLD ITEM BUTTON
        //OR MAKE IT HERE AS REFERENCE, not in inventory, and just show/hide it
    }
}
