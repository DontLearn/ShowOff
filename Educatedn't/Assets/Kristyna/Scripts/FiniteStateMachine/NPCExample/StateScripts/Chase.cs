using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : NPCBaseClassFSM
{
	[Space(10)]
	[Header("ChaseState")]
	public float chaseSpeed = 10.0f;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateEnter(animator, stateInfo, layerIndex);
		NPC.GetComponent<NPCAI>().PlayerNotVisible();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//Move NPC
		agent.SetDestination(opponent.transform.position);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}
}
