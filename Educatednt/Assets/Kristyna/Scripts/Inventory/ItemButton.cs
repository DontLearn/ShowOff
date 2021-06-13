using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public void DestroyItemButton()
    {
        GetComponentInParent<Slot>().SlotIsEmpty();//destroy this game object + tell inventory
    }
}