using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int slotNumber;
    public GameObject inventoryHolder;

    private string tag;
    private Inventory inventory;
    private void Start()
    {
        tag = inventoryHolder.gameObject.tag;
        inventory = GameObject.FindGameObjectWithTag(tag).GetComponent<Inventory>();
    }

    public void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.isFull[slotNumber] = false;
        }
    }
    public void DestroyItem()
    {
        foreach (Transform child in transform)
        {
            Debug.Log("slot's child destroy!");
            GameObject.Destroy(child.gameObject);
        }
    }
    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            //child.GetComponent<DropItemBackInScene>().SpawnDroppedItemInScene();
            Debug.Log("slot's child destroy!");
            GameObject.Destroy(child.gameObject);
        }
    }

    public void UseItem()
    {
        Debug.Log("Item in slot used!");       
    }
}
