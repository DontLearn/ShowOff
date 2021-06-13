using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Pickup : MonoBehaviour
{
    protected Inventory inventory;
    protected string playerTag = "Player";

    void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Inventory>();
    }
    public abstract void OnTriggerEnter(Collider other);
}
