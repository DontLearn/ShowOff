using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PatrolPoints : PatrolAbs {
    public enum PointType {
        LOCAL,
        WORLD
    }


    [SerializeField]
    private int _startingPoint = 0;

    [SerializeField, Range( 0.1f, 0.6f )]
    private float _pointAccuracy = 0.4f;

    [SerializeField]
    private PointType _pointType = PointType.LOCAL;

    [SerializeField]
    private Vector3[] _points = null;


    private NavMeshAgent _agent = null;
    private Vector3 _startingPos = Vector3.zero;
    private int _currentPoint = 0;



    private void Start() {
        LoadComponents();

        _startingPos = transform.position;
        _currentPoint = _startingPoint;
    }


    private void LoadComponents() {
        _agent = GetComponent<NavMeshAgent>();
        Debug.Assert( _agent, $"{this}: NavMeshAgent component missing on {name}." );
    }


    private void OnDrawGizmosSelected() {
        if ( PointsExist() ) {
            for ( int i = 0; i < _points.Length; ++i ) {
                Vector3 point = PositionedPoint( i );

                Gizmos.color = new Color( 1f, 1f, 1f, 0.4f );
                Gizmos.DrawLine( point, point + Vector3.up * 1.2f );

                Gizmos.color = new Color( 1f, 0f, 0f, 0.7f );
                Gizmos.DrawSphere( point + Vector3.up * 1.4f, 0.2f );
            }
        }
    }


    public override void OnStateEnter() { /*empty function*/ }


    public override void Patrol( bool agentEnabled ) {
        // if possible..
        if ( CurrentPointExists() ) {
            // ..and within range of the target waypoint..
            if ( WithinRange() ) {
                // ..set the current waypoint to the next waypoint in line
                SetNext();
            }

            // Move character
            if ( _agent ) {
                Vector3 point = ( agentEnabled ) ? PositionedPoint( _currentPoint ) : transform.position;
                if(_agent.isActiveAndEnabled)
                _agent.SetDestination( point );
            }
        }
    }


    private void SetNext() {
        _currentPoint++;
        if ( _currentPoint >= _points.Length ) {
            _currentPoint = 0;
        }

        // Once the state changes back to patrol, this patroller starts where it left off
        _startingPoint = _currentPoint;
    }


    private Vector3 PositionedPoint( int value ) {
        switch ( _pointType ) {
            case PointType.LOCAL:
#if UNITY_EDITOR
                if ( Application.isPlaying )
                    return _points[ value ] + _startingPos;
#endif
                return _points[ value ] + transform.position;
            case PointType.WORLD:
                return _points[ value ];
            default:
                return Vector3.zero;
        }
    }


    private bool CurrentPointExists() {
        return ( PointsExist() ) ? _currentPoint >= 0 && _currentPoint < _points.Length : false;
    }


    private bool PointsExist() {
        return null != _points && _points.Length > 0;
    }

    
    private bool WithinRange() {
        Vector3 point = PositionedPoint( _currentPoint );
        return Vector3.Distance( point, transform.position ) < _pointAccuracy;
    }
}