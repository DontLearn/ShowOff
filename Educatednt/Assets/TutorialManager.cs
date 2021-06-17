using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public int stageNumber1to3;
    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;
    public GameObject gate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void DefineStage()
    {
        if (stageNumber1to3 == 1)
        {
            stage1.SetActive(true);
            stage2.SetActive(false);
            stage3.SetActive(false);
        }
        else if (stageNumber1to3 == 2)
        {
            stage1.SetActive(false);
            stage2.SetActive(true);
            stage3.SetActive(false);
            gate.GetComponent<GateScript>().locked = true;
        }
        else if (stageNumber1to3 == 3)
        {
            stage1.SetActive(false);
            stage2.SetActive(false);
            stage3.SetActive(true);
            gate.GetComponent<GateScript>().locked = true;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
       
    }
}