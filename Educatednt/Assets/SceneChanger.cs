using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Data;

public class SceneChanger
{
    public static SceneChanger Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SceneChanger();
            }
            return _instance;
        }
    }


    private static SceneChanger _instance = null;


    public void ChangeScene(int sceneNumber)
    {
        PersistentData.Instance.SaveAllPersistentItems();
        SceneManager.LoadScene(sceneNumber);
        
    }
}
