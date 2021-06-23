using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTutorial : MonoBehaviour
{
    public int lvl;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(lvl);
        Debug.Log("уси гюксою");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
