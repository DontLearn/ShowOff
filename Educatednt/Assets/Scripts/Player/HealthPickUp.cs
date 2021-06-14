using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    private Health health;
    private HealthBar _healthBar;
    public int hitpointsBust;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("—” ¿");

            health = other.gameObject.GetComponent<Health>();
            health.hitPoints = health.hitPoints + hitpointsBust;
            int hitpoints = health.hitPoints;
            _healthBar = other.gameObject.GetComponentInChildren<HealthBar>();
            _healthBar.SetHealth(hitpoints);
            Destroy(gameObject);
        }
    }
}
