using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class WanderArea : PatrolAbs {
    [SerializeField, Range( 1.5f, 6f )]
    private float _radius = 3.5f;

    [SerializeField, Range( 0.1f, 0.6f )]
    private float _pointAccuracy = 0.2f;


    private Animator _animator = null;
    private NavMeshAgent _agent = null;
    private Vector3 _startingPos = Vector3.zero;
    private Vector3 _targetPoint = Vector3.zero;



    private void Start() {
        LoadComponents();

        _startingPos = transform.position;
        _targetPoint = GetRandomPoint();
    }


    private void LoadComponents() {
        _animator = GetComponent<Animator>();
        if ( !_animator ) {
            _animator = GetComponentInChildren<Animator>();
        }
        Debug.Assert( _animator, $"{this}: Animator component missing on {name}." );

        _agent = GetComponent<NavMeshAgent>();
        Debug.Assert( _agent, $"{this}: NavMeshAgent component missing on {name}." );
    }


    private void OnDrawGizmosSelected() {
        Vector3 point = PositionedPoint();
        Gizmos.color = new Color( 0f, 1f, 1f, 0.3f );
        Gizmos.DrawWireSphere( point, _radius );
    }


    public override void OnStateEnter() {
        _targetPoint = GetRandomPoint();
    }


    private Vector3 PositionedPoint() {
#if UNITY_EDITOR
        if ( Application.isPlaying )
            return _startingPos;
#endif
        return transform.position;
    }


    public override void Patrol( bool agentEnabled ) {
        // Select random point in points
        if ( WithinRange() ) {
            _animator.SetTrigger( "Chill" );
        }
        else {
            // Move character
            if ( _agent ) {
                Vector3 point = ( agentEnabled ) ? _targetPoint : transform.position;
                _agent.SetDestination( point );
            }
        }
    }


    private bool WithinRange() {
        return Vector3.Distance( _targetPoint, transform.position ) < _pointAccuracy;
    }


    private Vector3 GetRandomPoint() {
        Vector2 random = Random.insideUnitCircle * _radius;
        return _startingPos + new Vector3( random.x, 0f, random.y );
    }
}