using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class LoadData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PersistentData.Instance.LoadAllPersistentItems();
    }

   
}
