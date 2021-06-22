using UnityEngine;

[System.Serializable]
public class RecipeSerializable
{
    public string name;
    public byte rice;
    public byte tomatoe;
    public byte mushroom;
    public byte burger;
    [HideInInspector]
    public bool isReady;
}