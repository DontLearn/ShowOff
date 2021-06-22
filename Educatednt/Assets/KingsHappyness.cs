using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KingsHappyness : MonoBehaviour
{
    public int happynessLvl;
    public int burgersEaten;
    public bool KingIsFull;
    public Slider progressBar;
    // Start is called before the first frame update
    void Start()
    {
        KingIsFull = false;
        progressBar.maxValue = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(happynessLvl == 1 && KingIsFull)
        {
            Debug.Log("Get higher jump");
        }else if(happynessLvl == 2 && KingIsFull)
        {
            Debug.Log("Get bounce attack");
        }else if(happynessLvl == 3 && KingIsFull)
        {
            Debug.Log("Get better knife damage");
        }

      
    }

    public void NormalFoodEaten()
    {
        if (!KingIsFull)
        {
            happynessLvl++;
            KingIsFull = true;
            progressBar.value = happynessLvl;
        }
    }

    public void BurgerEaten()
    {
       if (!KingIsFull)
        {
            if (burgersEaten < 1)
            {
                happynessLvl++;
            }
            else
            {
                Debug.Log("King is sick because of you! You are fired!");
            }
            burgersEaten++;
            progressBar.value = happynessLvl;
            KingIsFull = true;
        }
        
    }


}
