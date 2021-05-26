using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField, Range( 0.5f, 12f )]
    private float _speed = 4f;

    [SerializeField, Range( 3f, 8f )]
    private float _rotationSpeed = 5f;

    [SerializeField]
    private bool _canJump = false;

    [SerializeField]
    private KeyCode _jumpKey = KeyCode.Space;

    [SerializeField, Range( 12f, 34f )]
    private float _jumpForce = 23f;

    [SerializeField, Range( 0f, 10f )]
    private float _jumpDist = 5f;

    [SerializeField]
    private Combat.Attack _attackForward = null;

    [SerializeField]
    private Combat.Attack _attackBelow = null;


    private Rigidbody _rb = null;
    private Vector3 _movement = Vector3.zero;
    private bool _jumpPressed = false;
    private bool _attackPressed = true;



    void Start() {
        _rb = GetComponent<Rigidbody>();
    }


    private bool IsGrounded() {
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Vector3 direction = new Vector3(0, -1, 0);
        float maxDistance = 0.6f;
        int layerMask = 1;


        return Physics.Raycast( origin, direction, maxDistance, layerMask );
    }


    
    private void Update() {
        float moveHorizontal = Input.GetAxis( "Horizontal" );
        float moveVertical = Input.GetAxis( "Vertical" );
        _movement = new Vector3( moveHorizontal, 0, moveVertical );

        if ( Input.GetKeyDown( _jumpKey ) ) {
            _jumpPressed = true;
        }
        if ( Input.GetMouseButtonDown( 0 ) && null != _attackForward && null != _attackBelow ) {
            _attackPressed = true;
        }
    }


    private void FixedUpdate() {
        // ROTATE
        Rotate();

        if ( IsGrounded() ) {
            // JUMP
            Jump();
        }
        // MOVE
        Move();

        // ATTACK
        Attack();
    }


    private void Rotate() {
        Vector3 velocityXZ = new Vector3( _rb.velocity.x, 0, _rb.velocity.z );
        if ( velocityXZ.magnitude > 0.01f ) {
            transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.LookRotation( velocityXZ ), Time.fixedDeltaTime * _rotationSpeed );
        }
    }


    private void Jump() {
        if ( _jumpPressed && _canJump ) {
            _jumpPressed = false;
            Vector3 velocityXZ = new Vector3( _rb.velocity.x, 0, _rb.velocity.z ).normalized;
            _rb.AddForce( new Vector3( velocityXZ.x * _jumpDist, _jumpForce, velocityXZ.z * _jumpDist ) * 10f );
        }
    }


    private void Bounce() {
        Vector3 velocityXZ = new Vector3( _rb.velocity.x, 0, _rb.velocity.z ).normalized;
        _rb.AddForce( new Vector3( velocityXZ.x * _jumpDist, _jumpForce, velocityXZ.z * _jumpDist ) * 5f );
    }


    private void Move() {
        if ( _movement.magnitude >= 0.01f ) {
            _rb.AddForce( _movement * _speed );
        }
    }

    private void Attack() {
        if ( _attackPressed ) {
            _attackPressed = false;
            if ( IsGrounded() || _rb.velocity.y > 0 ) {
                _attackForward.Strike();
                Debug.Log( "Swoosh!" );
            }
            else {
                _rb.velocity = new Vector3( _rb.velocity.x, 0, _rb.velocity.z );
                _attackBelow.Strike();
                Bounce();
                Debug.Log( "Bump!" );
            }
        }
    }
}