using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KitchenManager : MonoBehaviour
{
    [Header("From best to worst.")]
    public Sprite[] kingStates = new Sprite[4];
    public byte startState = 0;
    [Space(10)]
    public Image kingStateImage;
    // Start is called before the first frame update
    void Start()
    {
        kingStateImage.GetComponent<Image>().sprite = kingStates[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeBackToScene(int pSceneNumber)
    {
        SceneManager.LoadScene(pSceneNumber);
    }
}
