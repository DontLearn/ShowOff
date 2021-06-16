using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KitchenManager : MonoBehaviour
{
    [Header("From best to worst.")]
    public Sprite[] kingStates = new Sprite[4];
    public byte startState = 0;
    [Space(10)]
    public Image kingStateImage;
    [Space(10)]
    public Button[] recipeButtons;
    
    void Start()
    {
        Console.WriteLine($"HELLO");
        kingStateImage.GetComponent<Image>().sprite = kingStates[0];
/*
        //display buttons based on what recipe is ready
        for (int i = 0; i < RecipeManager.recipes.Count; i++)
        {
            if (RecipeManager.recipes[i].isReadyToCook)
            {
                recipeButtons[i].gameObject.SetActive(true);
                Console.WriteLine($"Recipe{i} is ready to be cooked = true");
            }
            else
            {
                Console.WriteLine($"Recipe{i} is ready to be cooked = false");
                recipeButtons[i].gameObject.SetActive(false);
            }
        }*/
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeBackToScene(int pSceneNumber)
    {
        SceneManager.LoadScene(pSceneNumber);
    }
}