using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class RunFromCharacter : ChaseAbs {
    private NavMeshAgent _agent = null;



    private void Start() {
        LoadComponents();
    }


    private void LoadComponents() {
        _agent = GetComponent<NavMeshAgent>();
        Debug.Assert( _agent, $"{this}: NavMeshAgent component missing on {name}." );
    }


    public override void Chase( Transform target, bool agentEnabled ) {
        if ( target ) {
            if ( _agent ) {
                Vector3 point = ( agentEnabled ) ? transform.position + ( transform.position - target.position ).normalized * 1.5f : transform.position;
                _agent.SetDestination( point );
            }
        }
    }
}
