using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : Pickup
{
    public ingredientType value = ingredientType.Mushroom;
    public enum ingredientType
    {
        Rice = 0,
        Tomatoe = 1,
        Mushroom = 2
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            if (inventory.AddItemToInventory(this.value))
            {
                //Debug.Log("iNGREDIENT CAN GO TO INVENORY");
                Destroy(this.gameObject);
            }
            else
            {
                //Debug.Log("iNGREDIENT cannot store in INVENORY");
            }
        }
    }
}
