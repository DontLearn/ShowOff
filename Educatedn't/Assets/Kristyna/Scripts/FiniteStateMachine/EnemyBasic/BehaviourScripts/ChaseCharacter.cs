using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChaseCharacter : ChaseAbs {
    private NavMeshAgent _agent = null;
    private Animator _animator = null;
    private float _attackRange = 2f;
    private float _attackMargin = 0.5f;



    private void Start() {
        LoadComponents();
    }


    private void LoadComponents() {
        _agent = GetComponent<NavMeshAgent>();
        Debug.Assert( _agent, $"{this}: NavMeshAgent component missing on {name}." );

        _animator = GetComponent<Animator>();
        Debug.Assert( _animator, $"{this}: Animator component missing on {name}." );

        if ( GetComponent<Combat.Attack>() is Combat.RadialAttack ) {
            Combat.RadialAttack radialAttack = GetComponent<Combat.RadialAttack>();
            _attackRange = radialAttack.Radius;
        }
    }


    public override void Chase( Transform target, bool agentEnabled ) {
        if ( target ) {
            if ( _agent ) {
                Vector3 point = ( agentEnabled ) ? target.position : transform.position;
                _agent.SetDestination( point );
            }
            if ( Vector3.Distance( transform.position, target.position ) <= _attackRange - _attackMargin ) {
                _animator.SetTrigger( "Jump" );
            }
        }
    }
}
