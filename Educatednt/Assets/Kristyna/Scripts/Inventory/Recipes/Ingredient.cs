using UnityEngine;

public class Ingredient : Pickup
{
    public ingredientType Value => _value;
    
    
    public enum ingredientType {
        RICE = 0,
        TOMATO = 1,
        MUSHROOM = 2,
        BURGER = 3
    }

    [SerializeField]
    private ingredientType _value = ingredientType.RICE;





    public override void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == playerTag) {
            if (inventory.AddItemToInventory(this._value)) {
                Destroy(this.gameObject);
            }
        }
    }
}
