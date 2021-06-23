using UnityEngine;

public class HealthPickup : Pickup
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            //inventory.healtPickupTest();
            Debug.Log("HealthPickup: Collided with player but did nothing, should it do something? -> code it");
            Destroy(this.gameObject);
        }
    }
}
