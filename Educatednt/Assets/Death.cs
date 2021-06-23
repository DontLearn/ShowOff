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
        if (health.HitPoints <= 0)
        {
            gameObject.SetActive(false);
            Died();
        }
    }

    private void Died()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        GameObject newpickup = Instantiate(pickUp, pos, transform.rotation);
    }
}
