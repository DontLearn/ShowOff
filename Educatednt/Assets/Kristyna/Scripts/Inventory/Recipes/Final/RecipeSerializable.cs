using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[System.Serializable]
public class RecipeSerializable
{
    public string name;
    public List<IngredientSerializable> ingredients;
    public int recipeTotalValue;
}
