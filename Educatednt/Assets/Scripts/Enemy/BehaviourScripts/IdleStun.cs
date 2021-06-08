using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class IdleStun : IdleAbs {
    [SerializeField, Range( 0f, 4f )]
    private float _stunTime = 1.8f;


    private Animator _animator = null;
    private NavMeshAgent _agent = null;
    private float _stunTimer = 0f;



    private void Start() {
        LoadComponents();
    }


    private void LoadComponents() {
        _animator = GetComponent<Animator>();
        Debug.Assert( _animator, $"{this}: Animator component missing on {name}." );

        _agent = GetComponent<NavMeshAgent>();
        Debug.Assert( _agent, $"{this}: NavMeshAgent component missing on {name}." );
    }


    public override void OnStateEnter() {
        _stunTimer = _stunTime;
    }


    public override void Idle( bool agentEnabled ) {
        if ( CountDown() ) {
            if ( _animator )
                _animator.SetTrigger( "Wake" );
        }
        /*if ( _agent && !agentEnabled ) {
            _agent.SetDestination( transform.position );
        }*/
    }


    private bool CountDown() {
        _stunTimer = Mathf.Max( _stunTimer - Time.deltaTime, 0f );
        return _stunTimer <= Mathf.Epsilon;
    }
}