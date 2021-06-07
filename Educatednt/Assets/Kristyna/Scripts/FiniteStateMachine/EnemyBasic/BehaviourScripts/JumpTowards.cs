using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class JumpTowards : JumpAbs {
    [SerializeField, Range( 0.1f, 10f )]
    private float _jumpSpeed = 4.5f;

    [SerializeField, Range( 0.5f, 7f )]
    private float _maxHeight = 4f;

    [SerializeField, Range( 0.25f, 2.5f )]
    private float _landOffset = 4f / 3f;

    
    private Animator _animator = null;
    private Vector3 _targetPos = Vector3.zero;
    private float _bodyMargin = 0.5f;
    private float _minimumLandOffset = 0.25f;
    private bool _active = false;

    // For calculation
    private Vector2 _direction = Vector2.zero;
    private float _speedCounter = 0f;
    private float _distanceCounter = 0f;
    private float _timeSpeed = 0f;



    private void Start() {
        LoadComponents();
    }


    private void LoadComponents() {
        _animator = GetComponent<Animator>();
        Debug.Assert( _animator, $"{this}: Animator component missing on {name}." );
    }


    private void OnDrawGizmosSelected() {
        // Draw landing position
        Gizmos.color = new Color( 1, 0, 1, 0.6f );
        Gizmos.DrawRay( _targetPos, Vector3.up * 2.75f );
        Gizmos.DrawSphere( _targetPos + Vector3.up * 3f, 0.25f );
    }


    private void FixedUpdate() {
        /// TODO: jump from pos a to pos b in arc
        /// for now, just skips the jump
        //_animator.SetTrigger( "Attack" );


        /// Get trajectory based on starting pos, landing pos, and angle
        if ( _active ) {
            Vector3 displacement = CalculateTrajectory();
            transform.position += displacement;
        }
    }


    public override void OnStateEnter( params object[] args ) {
        _active = true;
        Vector3 startPos = transform.position;

        if ( args[ 0 ] is Transform ) {
            _targetPos = ( ( Transform )args[ 0 ] ).position;

            float dist = Vector3.Distance( startPos, _targetPos );
            float offset = ( dist - _bodyMargin < _landOffset ) ? Mathf.Max( dist - _bodyMargin, _minimumLandOffset ) : _landOffset;
            _targetPos += ( startPos - _targetPos ).normalized * offset;

            _direction = new Vector2( ( _targetPos - startPos ).x, ( _targetPos - startPos ).z );

            _speedCounter = 0f;
            _distanceCounter = dist;
            if ( _jumpSpeed > Mathf.Epsilon ) {
                _timeSpeed = ( _distanceCounter / _jumpSpeed / Time.fixedDeltaTime );
            }
        }
        else Debug.LogWarning( $"{this}: OnStateEnter method on JumpTowards script only uses Transform as first parameter." );
    }


    public override void OnStateExit() {
        _active = false;
    }


    private Vector3 CalculateTrajectory() {
        _speedCounter += _timeSpeed;
        Vector3 displacement = new Vector3( _direction.x, 0, _direction.y ) * _timeSpeed;


        return displacement;
    }
}