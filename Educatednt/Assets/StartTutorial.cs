using UnityEngine;
using UnityEngine.SceneManagement;


public class StartTutorial : MonoBehaviour
{
    public int lvl;



    void Awake() {
        SceneManager.LoadScene( lvl );
    }
}
