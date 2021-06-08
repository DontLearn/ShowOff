using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(RecipesAll))]
public class IngredientsSerializableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        RecipesAll script = (RecipesAll)target;
    }
}
