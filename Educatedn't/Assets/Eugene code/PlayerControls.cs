using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField, Range( 0.5f, 12f )]
    private float _speed = 6.5f;

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


    private Rigidbody _rb = null;
    private Vector3 _movement = Vector3.zero;
    private bool _jumpPressed = false;



    void Start() {
        _rb = GetComponent<Rigidbody>();
    }


    private bool CheckGrounded() {
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Vector3 direction = new Vector3(0, -1, 0);
        float maxDistance = 0.6f;

        //Debug.DrawRay(origin, direction * maxDistance, Color.magenta);

        return Physics.Raycast( origin, direction, maxDistance );
    }


    
    private void Update() {
        float moveHorizontal = Input.GetAxis( "Horizontal" );
        float moveVertical = Input.GetAxis( "Vertical" );
        _movement = new Vector3( moveHorizontal, 0, moveVertical );

        if ( Input.GetKeyDown( _jumpKey ) ) {
            _jumpPressed = true;
        }
    }


    private void FixedUpdate() {
        Vector3 velocityXY = new Vector3( _rb.velocity.x, 0, _rb.velocity.z );
        if ( velocityXY.magnitude > 0.01f ) {
            transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.LookRotation( velocityXY ), Time.fixedDeltaTime * _rotationSpeed );
        }

        if ( CheckGrounded() ) {
            // JUMP
            Jump();
        }
        // MOVE
        Move();
    }

    private void Jump() {
        if ( _jumpPressed && _canJump ) {
            _jumpPressed = false;
            Vector3 velocityXZ = new Vector3( _rb.velocity.x, 0, _rb.velocity.z ).normalized;
            _rb.AddForce( new Vector3( velocityXZ.x * _jumpDist, _jumpForce, velocityXZ.z * _jumpDist ) * 10f );
        }
    }

    private void Move() {
        if ( _movement.magnitude >= 0.01f ) {
            _rb.AddForce( _movement * _speed );
        }
    }
}
