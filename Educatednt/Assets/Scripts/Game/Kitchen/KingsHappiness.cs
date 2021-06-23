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
        _happinessLvl = data[ Data.HAPPINESS ];
        _burgersEaten = data[ Data.BURGERS_EATEN ];
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

        progressBar.value = _happinessLvl;
    }


    public override void Load( PersistentData persistentData ) {
        base.Load( persistentData );

        if ( !persistentData.TryGetIntData( Data.HAPPINESS.ToString(), out _happinessLvl ) ) {
            Debug.LogError( $"{this} Can't parse {Data.HAPPINESS}, not an int." );
        }
        if ( !persistentData.TryGetIntData( Data.BURGERS_EATEN.ToString(), out _burgersEaten ) ) {
            Debug.LogError( $"{this} Can't parse {Data.BURGERS_EATEN}, not an int." );
        }

        data[ Data.HAPPINESS ] = _happinessLvl;
        Debug.Log( $"{this}: Loaded king's happiness to {_happinessLvl}." );

        data[ Data.BURGERS_EATEN ] = _burgersEaten;
        Debug.Log( $"{this}: Loaded king's burger amount to {_burgersEaten}." );
    }


    public override void Save( PersistentData persistentData ) {
        persistentData.SetIntData( Data.HAPPINESS.ToString(), _happinessLvl );
        Debug.Log( $"{this}: Saved king's happiness to {_happinessLvl}." );

        persistentData.SetIntData( Data.BURGERS_EATEN.ToString(), _burgersEaten );
        Debug.Log( $"{this}: Saved king's burger amount to {_burgersEaten}." );
    }
}
