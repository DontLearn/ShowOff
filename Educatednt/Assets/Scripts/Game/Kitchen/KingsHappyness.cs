using UnityEngine;
using UnityEngine.UI;
using Data;

public class KingsHappyness : KingBehaviour
{
    [Tooltip("Pass KingStateImg UI from canvas")]
    public Image kingStateImage;

    [Header("level 1, 2, 3")]
    public Sprite[] kingStates;
    public byte startState = 1;//start on sad happpines img

    private int _happynessLvl;
    private int _burgersEaten;
    public Slider progressBar;

    private bool _upgraded = false;

    private void Upgrade()
    {
        //load data
        _happynessLvl = happiness;
        _burgersEaten = burgersEaten;

        _upgraded = true;
    }
    void Start()
    {
        kingStateImage.GetComponent<Image>().sprite = kingStates[startState];

        progressBar.maxValue = 3;
        _happynessLvl = 1;
        progressBar.value = _happynessLvl;
    }
    
    void Update()
    {
        if (!_upgraded && isLoaded)
        {
            // UPGRADE
            Upgrade();
        }

        if (_happynessLvl == 0)
        {
            kingStateImage.GetComponent<Image>().sprite = kingStates[0];
        }
        else if(_happynessLvl == 1)
        {
            kingStateImage.GetComponent<Image>().sprite = kingStates[1];
            Debug.Log("Get higher jump");
        }else if(_happynessLvl == 2)
        {
            kingStateImage.GetComponent<Image>().sprite = kingStates[2];
            Debug.Log("Get bounce attack");
        }else if(_happynessLvl == 3)
        {
            kingStateImage.GetComponent<Image>().sprite = kingStates[3];
            Debug.Log("Get better knife damage");
        }
    }

    public void NormalFoodEaten()
    {
        _happynessLvl++;
        progressBar.value = _happynessLvl;

        happiness = _happynessLvl;//sava data
    }

    public void BurgerEaten()
    {
        if (_burgersEaten < 1)
        {
            _happynessLvl++;
        }
        else
        {
            _happynessLvl = 0;
            Debug.Log("King is sick because of you! You are fired!");
        }

        _burgersEaten++;

        //sava data
        burgersEaten = _burgersEaten;
        happiness = _happynessLvl;

        progressBar.value = _happynessLvl;
    }


}
