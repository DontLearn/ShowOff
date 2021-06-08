using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RecipesAll : MonoBehaviour
{
    [SerializeField]
    public List<RecipeSerializable> recipes = new List<RecipeSerializable>();
    [SerializeField]
    public List<IngredientSerializable> allIngredients = new List<IngredientSerializable>();

    [HideInInspector]
    public List<GameObject> allIngredientObjects = new List<GameObject>();
    private void OnValidate()//when change in script properties
    {
        checkIfAllIngredientsAreValidIngredients();
        getAllObjectsFromIngredientsList();

        CountValueOfEveryRecipe();
    }
    public void checkIfAllIngredientsAreValidIngredients()
    {
        if (allIngredients.Count != 0)
        {
            for (int i = 0; i < allIngredients.Count; i++)
            {
                if (allIngredients[i].item != null)
                {
                    if (allIngredients[i].item.GetComponent<Ingredient>() == null)
                    {
                        Debug.LogError("GameObject in ingredient "+i+" is not a valid ingredient! (missing ingredient script)");
                    }
                }
                else
                {
                    Debug.LogError("GameObject in ingredient " + i + " not found!");
                }
            }
        }
    }
    //TO DO: this method might not be needed
    public void getAllObjectsFromIngredientsList()
    {
        allIngredientObjects.Clear();
        if (allIngredients.Count != 0)
        {
            foreach (IngredientSerializable i in allIngredients)
            {
                if (i.item != null)
                {
                    allIngredientObjects.Add(i.item);
                }
            }
            Debug.Log("Got all gameObjects from the list of ingredients. AllIngObjs.Count = " + allIngredientObjects.Count);
        }
        else
        {
            Debug.Log("The list of ingredients is empty.");
        }
    }

    //TO DO: count the value of the ingredient based on value of the same ingredient in the list of all ingredients (find match, get value)
    private void CountValueOfEveryRecipe()
    {
        if (recipes.Count != 0)
        {
            foreach (RecipeSerializable r in recipes)
            {
                if (r.ingredients.Count != 0)
                {
                    if (r.recipeTotalValue != 0) r.recipeTotalValue = 0;//delete old value
                    foreach (IngredientSerializable i in r.ingredients)
                    {
                        r.recipeTotalValue += i.value;
                    }
                }
            }
        }
    }

    //TO DO: make this return a recipe if match found
    public void ReturnAvailableRecipe(List<GameObject> pItems)
    {
        //Check if list of ingredients contain any of the given items
        //find the ingredient in the allIngredients list based on name
        //get the value of this ingredient and add it to the total number
        //find recipe with matching number from ingredients

        //count total value of ingredients in the list:
        int itemsTotalValue = 0;
        foreach (GameObject g in pItems)
        {
            if (allIngredientObjects.Contains(g))
            {
                foreach (IngredientSerializable ing in allIngredients)
                {
                    if (ing.item.name == g.name)
                    {
                        itemsTotalValue += ing.value;
                    }
                }
            }
            else
            {
                Debug.Log("No ingredients in inventory.");
            }
        }
        Debug.Log("All ingredients in inventory have a value of " +itemsTotalValue);

        //find recipe match:
        foreach (RecipeSerializable r in recipes)
        {
            if(r.recipeTotalValue == itemsTotalValue)
            {
                Debug.Log("Congratulations! You can cook " +r.name+ " recipe!");
                //TO DO: return this recipe
            }
        }
    }
}
