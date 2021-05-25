using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    /// <summary>
    /// This script makes a gameObject a pickable item to an inventory
    /// </summary>

    public Sprite itemButtonSprite;
    public GameObject itemButton;
    public GameObject inventoryHolder;

    private string tag;
    private Inventory inventory;//where to store it

    void Start()
    {
        tag = inventoryHolder.gameObject.tag;
        inventory = GameObject.FindGameObjectWithTag(tag).GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag))
        {
            //Check if any slot is free
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    //Pickup is an ingredient:
                    if (this.GetComponent<Ingredient>() != null)
                    {
                        inventory.AddIngredienceInventory(this.gameObject);
                        inventory.isFull[i] = true;

                        itemButton.GetComponent<Image>().sprite = itemButtonSprite;
                        Instantiate(itemButton, inventory.slots[i].transform, false);//create itemButton at slot's position
                    }
                    //Pickup is not an ingredietn e.g. Health pickup
                    else
                    {
                        //Check which pickup it is and do an action
                        if (name == "Health")
                        {
                            inventory.healtPickupTest();
                        }                        
                    }

                    Destroy(this.gameObject);
                    break;
                }
            }
        }
    }
}
