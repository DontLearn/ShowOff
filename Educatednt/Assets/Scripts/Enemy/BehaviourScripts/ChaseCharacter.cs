using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChaseCharacter : ChaseAbs {
    [SerializeField, Range( 1f, 10f )]
    private float _jumpRange = 8f;


    private NavMeshAgent _agent = null;
    private Animator _animator = null;
    private float _jumpMargin = 0.5f;



    private void Start() {
        LoadComponents();
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay( transform.position + Vector3.up, transform.forward * ( _jumpRange - _jumpMargin - 0.4f ) );
        Gizmos.DrawSphere( transform.position + Vector3.up + transform.forward * ( _jumpRange - _jumpMargin ), 0.4f );
    }


    private void LoadComponents() {
        _agent = GetComponent<NavMeshAgent>();
        Debug.Assert( _agent, $"{this}: NavMeshAgent component missing on {name}." );

        _animator = GetComponent<Animator>();
        if ( !_animator ) {
            _animator = GetComponentInChildren<Animator>();
        }
        Debug.Assert( _animator, $"{this}: Animator component missing on {name}." );

        /// If jump is disabled:
        /*if ( GetComponent<Combat.Attack>() is Combat.RadialAttack ) {
            Combat.RadialAttack radialAttack = GetComponent<Combat.RadialAttack>();
            _jumpRange = radialAttack.Radius;
        }*/
    }


    public override void Chase( Transform target, bool agentEnabled ) {
        if ( target ) {
            if ( _agent && _agent.isActiveAndEnabled ) {
                Vector3 point = target.position;
                _agent.SetDestination( point );
            }
            if ( Vector3.Distance( transform.position, target.position ) <= _jumpRange - _jumpMargin ) {
                _animator.SetTrigger( "Jump" );
            }
        }
    }
}
