using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    public string name;
    [SerializeField]
    public List<Ingredient> ingredients;

    [ExecuteInEditMode]
    public void Awake()
    {
        for (int i = 0; i < ingredients.Count; i ++)
        {
            if (ingredients[i].GetComponent<Ingredient>() == null)
            {
                Debug.LogError("Ingredient["+i+"] in recipe "+name+" is not valid ingredient! Ingredient script is missing on this ingredient.");
            }
        }
    }
}
