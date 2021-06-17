using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateScript : MonoBehaviour
{
    private int amountOfLvls;
    public bool toKitchen;
    public int levelToGo;

    public bool locked;
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !locked)
        {
           

            if (SceneManager.GetActiveScene().buildIndex < 4 && !toKitchen)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if(!toKitchen)
            {
                SceneManager.LoadScene(0);
            } else
            {
                SceneManager.LoadScene(levelToGo);
            }
        }
    }
}
