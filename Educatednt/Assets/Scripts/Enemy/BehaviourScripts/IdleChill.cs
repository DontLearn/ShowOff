using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class IdleChill : IdleAbs {
    [SerializeField]
    private Vector2 _ChillTime = new Vector2( 0.2f, 1.7f );


    private Animator _animator = null;
    //private NavMeshAgent _agent = null;
    private float _chillTimer = 0f;



    private void Start() {
        LoadComponents();
    }


    private void LoadComponents() {
        _animator = GetComponent<Animator>();
        Debug.Assert( _animator, $"{this}: Animator component missing on {name}." );

        /*_agent = GetComponent<NavMeshAgent>();
        Debug.Assert( _agent, $"{this}: NavMeshAgent component missing on {name}." );*/
    }


    public override void OnStateEnter() {
        _chillTimer = Random.Range( _ChillTime.x, _ChillTime.y );
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
        _chillTimer = Mathf.Max( _chillTimer - Time.deltaTime, 0f );
        return _chillTimer <= Mathf.Epsilon;
    }
}