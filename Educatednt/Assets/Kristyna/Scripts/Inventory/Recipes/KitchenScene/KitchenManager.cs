using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KitchenManager : MonoBehaviour
{
    /// <summary>
    /// This class is supposed to take care of things in teh kitchen.
    /// Right now it should be doing more things (so they will probably have to be separated to individual scripts):
    /// like show/hide UI and it should somehow make inventory to delete some ingredience once recipe is cooked.
    /// And switching back to the (right now) prototype scene "SimpleInventory".
    /// </summary>
    [Header("From best to worst.")]
    public Sprite[] kingStates = new Sprite[4];
    public byte startState = 0;
    [Space(10)]
    public Image kingStateImage;
    [Space(10)]
    public Button[] recipeButtons;
    
    void Start()
    {        
        Debug.Log($"HELLO");
        kingStateImage.GetComponent<Image>().sprite = kingStates[2];
        //display buttons based on what recipe is ready
        
        for (int i = 0; i < 2; i++)
        {
            /*if (KitchenAvailableRecipesController.AvailableRecipes[i])
            {
                recipeButtons[i].gameObject.SetActive(true);
                Debug.Log($"Recipe{i} is ready to be cooked = true");
            }
            else
            {
                Debug.Log($"Recipe{i} is ready to be cooked = false");
                recipeButtons[i].gameObject.SetActive(false);
            }*/
            Debug.Log($"HELLO 2");
        }
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