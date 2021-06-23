using UnityEngine;

public class Ingredient : Pickup
{
    public ingredientType value = ingredientType.Rice;
    public enum ingredientType
    {
        Rice = 0,
        Tomato = 1,
        Mushroom = 2,
        Burger = 3
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            if (inventory.AddItemToInventory(this.value))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
