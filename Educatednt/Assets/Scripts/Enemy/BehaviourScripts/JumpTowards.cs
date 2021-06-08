using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class JumpTowards : JumpAbs {
    //[SerializeField, Range( 0.1f, 10f )]
    //private float _jumpSpeed = 4.5f;
    
    [SerializeField, Range( 10f, 80f )]
    private float initialAngle = 25f;

    [SerializeField, Range( 0.25f, 2.5f )]
    private float _landOffset = 0.5f;
    
    private Animator _animator = null;
    private Vector3 _startPos = Vector3.zero;
    private Vector3 _targetPos = Vector3.zero;
    private float _bodyMargin = 0.5f;
    private float _minimumLandOffset = 0.25f;
    private bool _active = false;

    // For calculation
    private Rigidbody _rb = null;
    private Vector2 _direction = Vector2.zero;
    private float _timeLeft = 0f;



    private void Start() {
        LoadComponents();
    }


    private void LoadComponents() {
        _animator = GetComponent<Animator>();
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
        /// TODO: jump from pos a to pos b in arc
        /// for now, just skips the jump
        //_animator.SetTrigger( "Attack" );


        /// Get trajectory based on starting pos, landing pos, and angle
        if ( _active ) {
            /*Vector3 displacement = CalculateTrajectory( out float height );

            if ( displacement.magnitude < Vector3.Distance( transform.position, _targetPos ) ) {
                transform.position = new Vector3(
                    transform.position.x + displacement.x,
                    _startPos.y + height,
                    transform.position.z + displacement.z
                );
            }
            else {
                transform.position = _targetPos;
                _animator.SetTrigger( "Attack" );
            }*/

            float dist = Vector3.Distance( transform.position, _targetPos );
            if ( dist < 1.4f ) {
                Debug.Log( "ATTAACK" );
                _animator.SetTrigger( "Attack" );
            }
        }
    }


    public override void OnStateEnter( params object[] args ) {
        _active = true;
        _startPos = transform.position;

        if ( args[ 0 ] is Transform ) {
            _targetPos = ( ( Transform )args[ 0 ] ).position;

            float dist = Vector3.Distance( _startPos, _targetPos );
            float offset = ( dist - _bodyMargin < _landOffset ) ? Mathf.Max( dist - _bodyMargin, _minimumLandOffset ) : _landOffset;
            _targetPos += ( _startPos - _targetPos ).normalized * offset;

            CalculateTrajectory();


            /*Vector3 diffXZ = new Vector3( _targetPos.x - _startPos.x, 0, _targetPos.z - _startPos.z );
            _direction = new Vector2( diffXZ.x, diffXZ.z ).normalized;

            // amount of seconds necessary to displace
            _timeLeft = diffXZ.magnitude / _jumpSpeed;
            Debug.Log( $"{this}: amount of time needed for the jump: {_timeLeft}." );
            Debug.Log( $"{this}: speed per fixed time frame: {_jumpSpeed * Time.fixedDeltaTime}." );*/
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
        _rb.velocity = finalVelocity;


        /*
        //Calculate the first jump time
        float sqrtTerm = Mathf.Sqrt( 2f * gravity.y * jumpPoint.deltaPosition.y + maxYVelocity * agent.maxSpeed );
        float time = ( maxYVelocity - sqrtTerm ) / gravity.y;

        //Check if we can use it, otherwise try the other time
        if ( !CheckJumpTime( time ) ) {
            time = ( maxYVelocity + sqrtTerm ) / gravity.y;
        }*/
        //return new Vector3( _direction.x, 0, _direction.y ) * _jumpSpeed * Time.fixedDeltaTime;
    }


    /*
    //Private helper method for the CalculateTarget function
    private bool CheckJumpTime( float time ) {
        //Calculate the planar speed
        float vx = jumpPoint.deltaPosition.x / time;
        float vz = jumpPoint.deltaPosition.z / time;
        float speedSq = vx * vx + vz * vz;

        //Check it to see if we have a valid solution
        if ( speedSq < agent.maxSpeed * agent.maxSpeed ) {
            target.GetComponent<Agent>().velocity = new Vector3( vx, 0f, vz );
            canAchieve = true;
            return true;
        }
        return false;
    }*/
}