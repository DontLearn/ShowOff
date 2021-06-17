using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyFodDoor : MonoBehaviour
{
    public GameObject gate;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            gate.GetComponent<GateScript>().locked = false;
            gameObject.SetActive(false);
        }
    }
}
