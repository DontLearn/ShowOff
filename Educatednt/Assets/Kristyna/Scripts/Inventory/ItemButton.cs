using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public void DestroyItemButton()
    {
        Debug.Log("ItemButton destroyed!");
        GameObject.Destroy(this.gameObject);
    }
}
