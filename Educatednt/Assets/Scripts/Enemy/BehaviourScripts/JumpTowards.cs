using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class JumpTowards : JumpAbs {[SerializeField, Range( 10f, 80f )]
    private float initialAngle = 25f;

    [SerializeField, Range( 0.25f, 2.5f )]
    private float _landOffset = 0.5f;

    [SerializeField, Range( 0.4f, 1.4f )]
    private float _velocityCutoff = 0.7f;

    
    private Rigidbody _rb = null;
    private Animator _animator = null;
    private Vector3 _startPos = Vector3.zero;
    private Vector3 _targetPos = Vector3.zero;
    private float _bodyMargin = 0.5f;
    private float _minimumLandOffset = 0.25f;
    private bool _active = false;



    private void Start() {
        LoadComponents();
    }


    private void LoadComponents() {
        _animator = GetComponent<Animator>();
        if ( !_animator ) {
            _animator = GetComponentInChildren<Animator>();
        }
        Debug.Assert( _animator, $"{this}: Animator component missing on {name}." );

        _rb = GetComponent<Rigidbody>();
        Debug.Assert( _rb, $"{this}: Rigidbody component missing on {name}." );
    }


    private void OnDrawGizmosSelected() {
        // Draw starting position
        Gizmos.color = new Color( 0, 1, 1, 0.6f );
        Gizmos.DrawRay( _startPos, Vector3.up * 2.75f );
        Gizmos.DrawSphere( _startPos + Vector3.up * 3f, 0.25f );

        // Draw landing position
        Gizmos.color = new Color( 1, 0, 1, 0.6f );
        Gizmos.DrawRay( _targetPos, Vector3.up * 2.75f );
        Gizmos.DrawSphere( _targetPos + Vector3.up * 3f, 0.25f );
    }


    private void FixedUpdate() {
        /// Get trajectory based on starting pos, landing pos, and angle
        if ( _active ) {
            if ( _rb.velocity.magnitude <= _velocityCutoff ) {
                _animator.SetTrigger( "Attack" );
            }
        }
    }


    public override void OnStateEnter( params object[] args ) {
        _startPos = transform.position;

        if ( args[ 0 ] is Transform ) {
            _targetPos = ( ( Transform )args[ 0 ] ).position;

            float dist = Vector3.Distance( _startPos, _targetPos );
            float offset = ( dist - _bodyMargin < _landOffset ) ? Mathf.Max( dist - _bodyMargin, _minimumLandOffset ) : _landOffset;
            _targetPos += ( _startPos - _targetPos ).normalized * offset;

            CalculateTrajectory();
        }
        else Debug.LogWarning( $"{this}: OnStateEnter method on JumpTowards script only uses Transform as first parameter." );
    }


    public override void OnStateExit() {
        _active = false;
    }


    private void CalculateTrajectory() {
        float gravity = Physics.gravity.magnitude;
        // Selected angle in radians
        float angle = initialAngle * Mathf.Deg2Rad;

        // Positions of this object and the target on the same plane
        Vector3 planarTarget = new Vector3( _targetPos.x, 0, _targetPos.z );
        Vector3 planarPostion = new Vector3( _startPos.x, 0, _startPos.z );

        // Planar distance between objects
        float distance = Vector3.Distance( planarTarget, planarPostion );
        // Distance along the y axis between objects
        float yOffset = transform.position.y - _targetPos.y;

        float initialVelocity = ( 1 / Mathf.Cos( angle ) ) * Mathf.Sqrt( ( 0.5f * gravity * Mathf.Pow( distance, 2 ) ) / ( distance * Mathf.Tan( angle ) + yOffset ) );

        Vector3 velocity = new Vector3( 0, initialVelocity * Mathf.Sin( angle ), initialVelocity * Mathf.Cos( angle ) );

        // Rotate our velocity to match the direction between the two objects
        float angleBetweenObjects = Vector3.SignedAngle( Vector3.forward, planarTarget - planarPostion, Vector3.up );
        Vector3 finalVelocity = Quaternion.AngleAxis( angleBetweenObjects, Vector3.up ) * velocity;

        // Fire!
        if ( finalVelocity.magnitude > Mathf.Epsilon * 3f ) {
            _rb.velocity = finalVelocity;
            _active = true;
        }
        else {
            _animator.SetTrigger( "Attack" );
        }
    }
}