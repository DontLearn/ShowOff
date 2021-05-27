using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseCharacter : ChaseAbs
{
    public string targetTag = "Player";

    private NavMeshAgent _agent;
    private GameObject _target;

    private void Start()
    {
        _agent = this.gameObject.GetComponent<NavMeshAgent>();
        Debug.Assert(_agent, "ChaseCharacter: NavMeshAgent not found!");
        _target = GameObject.FindGameObjectWithTag(targetTag);
        Debug.Assert(_target, "ChaseCharacter: GameObject with tag '"+targetTag+"' not found!");
    }
    public override void Chase(GameObject pCharacter)
    {
        _agent.SetDestination(_target.transform.position);
    }
}
