using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class TutorialManager : MonoBehaviour {
    public int stage;
    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;
    public GameObject gate;



    // Start is called before the first frame update
    void Start() {
        if ( !PersistentData.Instance.TryGetIntData( PlayerBehaviour.Data.UPGRADE.ToString(), out int playerLevel ) ) {
            Debug.LogError( $"{this} Can't parse {PlayerBehaviour.Data.UPGRADE}, not an int." );
        }

        Debug.Log( playerLevel + "ZALUPA" );
        stage = playerLevel;
        DefineStage();
    }


    public void DefineStage() {
        switch ( stage ) {
            case 0:
                stage1.SetActive( true );
                stage2.SetActive( false );
                stage3.SetActive( false );
                break;
            case 1:
                stage1.SetActive( false );
                stage2.SetActive( true );
                stage3.SetActive( false );
                gate.GetComponent<GateScript>().locked = true;
                break;
            case 2:
                stage1.SetActive( false );
                stage2.SetActive( false );
                stage3.SetActive( true );
                gate.GetComponent<GateScript>().locked = true;
                break;
        }
    }
}