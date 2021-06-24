using UnityEngine;
using UnityEngine.UI;
using Data;
using UnityEngine.SceneManagement;
using System;


public class KingsHappiness : KingBehaviour {
    [Tooltip( "Pass KingStateImg UI from canvas" )]
    public Image kingStateImage;

    [Header( "level 1, 2, 3" )]
    public Sprite[] kingStates;
    public byte startState = 0;

    public int happinessLvl;
    private int _burgersEaten;
    public Slider progressBar;

    public bool eaten;
    


    protected override void Upgrade() {
        base.Upgrade();
        //load data
        happinessLvl = data[ Data.HAPPINESS ];
        _burgersEaten = data[ Data.BURGERS_EATEN ];
        if (null != progressBar)
        progressBar.value = happinessLvl;
    }


    protected override void Awake()
    {
        base.Awake();
        if (null != kingStateImage && null != kingStates && startState >= 0 && startState<kingStates.Length)
        {
            kingStateImage.sprite = kingStates[startState]; 
        }

        if (null != progressBar)
        {
            progressBar.maxValue = 3;
            //happinessLvl = 0;
            progressBar.value = happinessLvl;
            // eaten = false;
        }



    }


    private void Update() {
        if ( !isUpgraded && isLoaded ) {
            // UPGRADE
            Upgrade();
             
        }


       
    }

    private void eat()
    {
        if (!eaten )
        {
            
            Debug.Log(happinessLvl + "SUKABLIAT");
            eaten = true;
            switch (happinessLvl)
            {
                case 0:
                    break;
                case 1:
                    Debug.Log("Get higher jump");
                    LevelUpPlayer();


                    break;
                case 2:
                    Debug.Log("Get bounce attack");
                    LevelUpPlayer();

                    break;
                case 3:
                    Debug.Log("Get better knife damage");
                    LevelUpPlayer();

                    break;
            }
            return;
        }

        if (null != kingStateImage)
        {
            Image img = kingStateImage.GetComponent<Image>();
            if(img != null && null != kingStates && happinessLvl >= 0 && happinessLvl < kingStates.Length)
            {
                img.sprite = kingStates[Math.Min( happinessLvl, kingStates.Length -1) ];
            }
            
        }
    }

    private void LevelUpPlayer()
    {
        Debug.LogWarning("PLAYER LEVEL" + PlayerBehaviour.Data.UPGRADE.ToString());
        int playerLevel;
        if (!PersistentData.Instance.TryGetIntData(PlayerBehaviour.Data.UPGRADE.ToString(), out playerLevel))
        {
            Debug.LogError($"{this} Can't parse {PlayerBehaviour.Data.UPGRADE}, not an int.");
        }
        PersistentData.Instance.SetIntData(PlayerBehaviour.Data.UPGRADE.ToString(), ++playerLevel);
        Debug.LogWarning("PLAYER LEVEL" + PlayerBehaviour.Data.UPGRADE.ToString());
    }
    public void NormalFoodEaten() {
        Debug.Log("EAT food");
        progressBar.value = ++happinessLvl;
        eat();
    }


    public void BurgerEaten() {
        Debug.Log("EAT BURGER");
        
        if ( ++_burgersEaten < 2 ) {
            happinessLvl++;
            eat();
        }else if (_burgersEaten > 2)
        {
            //load menu
            LoadMenu();
            return;
        }
        

        

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

    private void LoadMenu()
    {
        PersistentData.Instance.Reset();
        SceneManager.LoadScene(0);
    }
}
