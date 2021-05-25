using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [HideInInspector]
    public string name;

    [ExecuteInEditMode]
    public void Awake()
    {
        name = this.transform.name;
    }
}
