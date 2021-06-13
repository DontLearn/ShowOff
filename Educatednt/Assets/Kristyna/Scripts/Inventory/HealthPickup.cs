using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            inventory.healtPickupTest();
            Destroy(this.gameObject);
        }
    }
}
