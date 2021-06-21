using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Health health;
    public GameObject pickUp;
    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.hitPoints <= 0)
        {
            gameObject.SetActive(false);
            Died();
        }
    }

    private void Died()
    {
        GameObject newpickup = Instantiate(pickUp, transform.position, transform.rotation);
    }
}
