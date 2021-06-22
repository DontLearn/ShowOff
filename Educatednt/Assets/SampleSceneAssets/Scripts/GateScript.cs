using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GateScript : MonoBehaviour
{
   
    
    public int levelToGo = -1;

    public bool locked;
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !locked)
        {
            //Debug.Log("WTF DUDE");

           
            

            int nextLvl = (levelToGo >= 0) ? levelToGo : (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;

            SceneChanger.Instance.ChangeScene(nextLvl);

        }
    }
}
