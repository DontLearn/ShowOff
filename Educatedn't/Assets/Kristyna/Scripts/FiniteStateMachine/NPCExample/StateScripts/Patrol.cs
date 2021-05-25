using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : NPCBaseClassFSM
{

	[Space(10)]
	[Header("PatrolState")]
	public float patrolSpeed = 3.0f;

	private GameObject[] _waypoints;
	private int _currentWP;

	void Awake()
	{
		_waypoints = GameObject.FindGameObjectsWithTag("wp");
	}

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateEnter(animator, stateInfo, layerIndex);
		_currentWP = 0;
		NPC.GetComponent<NPCAI>().PlayerNotVisible();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (_waypoints.Length == 0) return;
		if (Vector3.Distance(_waypoints[_currentWP].transform.position, NPC.transform.position) < accuracy)
		{
			_currentWP++;
			if (_currentWP >= _waypoints.Length)
			{
				_currentWP = 0;
			}
			startingWP = _currentWP;//once change of state back to patrol, npc starts where it left off
			Debug.Log("CurrentWP = " + _currentWP + "| wp name = " + _waypoints[_currentWP].name);
		}

		//Move NPC
		agent.SetDestination(_waypoints[_currentWP].transform.position);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}
}
