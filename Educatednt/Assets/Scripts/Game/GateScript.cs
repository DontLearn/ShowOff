using UnityEngine;
using UnityEngine.SceneManagement;



public class GateScript : MonoBehaviour {
    public int levelToGo = -1;

    public bool locked;



    private void OnTriggerEnter( Collider other ) {
        if ( other.gameObject.CompareTag( "Player" ) && !locked ) {
            int nextLvl = ( levelToGo >= 0 ) ? levelToGo : ( SceneManager.GetActiveScene().buildIndex + 1 ) % SceneManager.sceneCountInBuildSettings;

            SceneChanger.Instance.ChangeScene( nextLvl );
        }
    }
}