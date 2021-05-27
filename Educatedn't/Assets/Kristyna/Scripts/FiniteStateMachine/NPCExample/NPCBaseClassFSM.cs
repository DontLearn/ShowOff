using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBaseClassFSM : StateMachineBehaviour
{
    [Header("General:")]
    public GameObject NPC;
    public GameObject opponent;
    [Header("Patrol:")]
    public int startingWP = 0;//from what WP should NPC start patrolling

    [HideInInspector]
    public NavMeshAgent agent;
    public float accuracy;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        agent = NPC.GetComponent<NavMeshAgent>();
        accuracy = agent.GetComponent<NavMeshAgent>().stoppingDistance;
        opponent = NPC.GetComponent<NPCAI>().GetPlayer();
    }
}
