using UnityEngine;
using UnityEngine.UI;
using Data;


public class KingsHappiness : KingBehaviour {
    [Tooltip( "Pass KingStateImg UI from canvas" )]
    public Image kingStateImage;

    [Header( "level 1, 2, 3" )]
    public Sprite[] kingStates;
    public byte startState = 0;

    public int happinessLvl;
    private int _burgersEaten;
    public Slider progressBar;


    protected override void Upgrade() {
        base.Upgrade();
        //load data
        happinessLvl = data[ Data.HAPPINESS ];
        _burgersEaten = data[ Data.BURGERS_EATEN ];
    }


    private void Start() {
        kingStateImage.GetComponent<Image>().sprite = kingStates[ startState ];

        progressBar.maxValue = 3;
        happinessLvl = 0;
        progressBar.value = happinessLvl;
    }


    private void Update() {
        if ( !isUpgraded && isLoaded ) {
            // UPGRADE
            Upgrade();
        }

        if ( null != kingStateImage ) {
            Image img = kingStateImage.GetComponent<Image>();
            if ( null != img && null != kingStates && happinessLvl >= 0 && happinessLvl < kingStates.Length ) {
                img.sprite = kingStates[ happinessLvl ];

                switch ( happinessLvl ) {
                    case 0:
                        break;
                    case 1:
                        Debug.Log( "Get higher jump" );
                        LevelUpPlayer();
                        break;
                    case 2:
                        Debug.Log( "Get bounce attack" );
                        LevelUpPlayer();
                        break;
                    case 3:
                        Debug.Log( "Get better knife damage" );
                        LevelUpPlayer();
                        break;
                }
                return;
            }
        }

        Debug.LogWarning( $"{this}: Upgrade failed." );
    }

    private void LevelUpPlayer()
    {
        int playerLevel;
        if (!PersistentData.Instance.TryGetIntData(PlayerBehaviour.Data.UPGRADE.ToString(), out playerLevel))
        {
            Debug.LogError($"{this} Can't parse {PlayerBehaviour.Data.UPGRADE}, not an int.");
        }
        PersistentData.Instance.SetIntData(PlayerBehaviour.Data.UPGRADE.ToString(), ++playerLevel);
    }
    public void NormalFoodEaten() {
        progressBar.value = ++happinessLvl;
    }


    public void BurgerEaten() {
        if ( _burgersEaten < 1 ) {
            happinessLvl++;
        }
        else {
            happinessLvl = 0;
            Debug.Log( "King is sick because of you! You are fired!" );
        }

        _burgersEaten++;

        progressBar.value = happinessLvl;
    }


    public override void Load( PersistentData persistentData ) {
        base.Load( persistentData );

        if ( !persistentData.TryGetIntData( Data.HAPPINESS.ToString(), out happinessLvl ) ) {
            Debug.LogError( $"{this} Can't parse {Data.HAPPINESS}, not an int." );
        }
        if ( !persistentData.TryGetIntData( Data.BURGERS_EATEN.ToString(), out _burgersEaten ) ) {
            Debug.LogError( $"{this} Can't parse {Data.BURGERS_EATEN}, not an int." );
        }

        data[ Data.HAPPINESS ] = happinessLvl;
        Debug.Log( $"{this}: Loaded king's happiness to {happinessLvl}." );

        data[ Data.BURGERS_EATEN ] = _burgersEaten;
        Debug.Log( $"{this}: Loaded king's burger amount to {_burgersEaten}." );
    }


    public override void Save( PersistentData persistentData ) {
        persistentData.SetIntData( Data.HAPPINESS.ToString(), happinessLvl );
        Debug.Log( $"{this}: Saved king's happiness to {happinessLvl}." );

        persistentData.SetIntData( Data.BURGERS_EATEN.ToString(), _burgersEaten );
        Debug.Log( $"{this}: Saved king's burger amount to {_burgersEaten}." );
    }
}
