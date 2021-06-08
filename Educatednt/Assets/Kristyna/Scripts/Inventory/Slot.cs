using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private int slotNumber;
    private GameObject inventoryHolder;
    private Inventory inventory;
    private void Start()
    {
        string number = this.name;
        number = number.Remove(0, 4);
        slotNumber = int.Parse(number);

        inventoryHolder = GameObject.FindGameObjectWithTag("Player");
        Debug.Assert(inventoryHolder, "Slot: Slot[" + slotNumber+"] could not find gameObject with tag Player!");
        inventory = GameObject.FindGameObjectWithTag(inventoryHolder.gameObject.tag).GetComponent<Inventory>();
        Debug.Assert(inventory, "Slot: The player does not have an inventory!");
    }

    public void Update()
    {
        if (transform.childCount <= 0)
        {
            if (slotNumber < inventory.slots.Length)
            {
                inventory.isFull[slotNumber] = false;
            }
            else
            {
                Debug.LogError("slotNumber " +slotNumber+" is out of range of the slots available in inventory! Change the slot's name. (e.g. Slot0, Slot33)");
            }
        }
    }
    public void DestroyItem(GameObject pObject)
    {
        //TO DO: TELL INVENTORY TO DELETE AN ITEM FROM LIST OF Inventory Items

        foreach (Transform child in transform)
        {
            Debug.Log("slot's child destroy!");
            GameObject.Destroy(child.gameObject);
        }
    }
    //public void DropItem()
    //{
    //    foreach (Transform child in transform)
    //    {
    //        Debug.Log("slot's child destroy!");
    //        GameObject.Destroy(child.gameObject);
    //    }
    //}

    public void UseItem()
    {
        Debug.Log("Item in slot used!");       
    }
}
