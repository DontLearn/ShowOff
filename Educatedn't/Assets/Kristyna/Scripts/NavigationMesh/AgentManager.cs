using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public string agentTag = "Player";
    public GameObject[] agents;

    // Start is called before the first frame update
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag(agentTag);    
    }

    void Update()
    {
        ClickToChangeAgentDestination();
    }

    private void ClickToChangeAgentDestination()
    {
        //Click on map
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            //Check click position
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                foreach (GameObject agent in agents)
                {
                    agent.GetComponent<AIControl>().agent.SetDestination(hit.point);//Move agent to click position
                }
            }
        }
    }
}
