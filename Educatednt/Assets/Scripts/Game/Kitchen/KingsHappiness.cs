using UnityEngine;
using UnityEngine.UI;
using Data;


public class KingsHappiness : KingBehaviour {
    [Tooltip( "Pass KingStateImg UI from canvas" )]
    public Image kingStateImage;

    [Header( "level 1, 2, 3" )]
    public Sprite[] kingStates;
    public byte startState = 1; //start on sad happpines img

    private int _happinessLvl;
    private int _burgersEaten;
    public Slider progressBar;



    protected override void Upgrade() {
        base.Upgrade();
        //load data
        _happinessLvl = happiness;
        _burgersEaten = burgersEaten;
    }


    private void Start() {
        kingStateImage.GetComponent<Image>().sprite = kingStates[ startState ];

        progressBar.maxValue = 3;
        _happinessLvl = 1;
        progressBar.value = _happinessLvl;
    }


    private void Update() {
        if ( !isUpgraded && isLoaded ) {
            // UPGRADE
            Upgrade();
        }

        if ( null != kingStateImage ) {
            Image img = kingStateImage.GetComponent<Image>();
            if ( null != img && null != kingStates && _happinessLvl >= 0 && _happinessLvl < kingStates.Length ) {
                img.sprite = kingStates[ _happinessLvl ];

                switch ( _happinessLvl ) {
                    case 1:
                        Debug.Log( "Get higher jump" );
                        //Show popup with good/bad outcome + text with what ability you get
                        //change players ability via variable
                        break;
                    case 2:
                        Debug.Log( "Get bounce attack" );
                        break;
                    case 3:
                        Debug.Log( "Get better knife damage" );
                        break;
                }
                return;
            }
        }

        Debug.LogWarning( $"{this}: Upgrade failed." );
    }


    public void NormalFoodEaten() {
        progressBar.value = ++_happinessLvl;

        happiness = _happinessLvl;//sava data
    }


    public void BurgerEaten() {
        if ( _burgersEaten < 1 ) {
            _happinessLvl++;
        }
        else {
            _happinessLvl = 0;
            Debug.Log( "King is sick because of you! You are fired!" );
        }

        _burgersEaten++;

        //sava data
        burgersEaten = _burgersEaten;
        happiness = _happinessLvl;

        progressBar.value = _happinessLvl;
    }
}
