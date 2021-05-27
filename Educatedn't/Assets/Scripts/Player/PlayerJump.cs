using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player {
    public class PlayerJump : MonoBehaviour {
        [SerializeField]
        private bool _canJump = false;

        [SerializeField, Range( 16f, 38f )]
        private float _jumpForce = 32f;

        [SerializeField, Range( 0f, 8f )]
        private float _jumpDash = 1.8f;

        [SerializeField, Range( 0f, 1f )]
        private float _dashSensitivity = 0.4f;


        private Rigidbody _rb = null;
        private bool _jumpPressed = false;



        void Start() {
            _rb = GetComponent<Rigidbody>();
            Debug.Assert( null != _rb, $"{name} is missing a RigidBody component." );
        }


        private void FixedUpdate() {
            // JUMP
            Jump();
        }


        public void JumpPressed() {
            _jumpPressed = true;
        }


        private void Jump() {
            if ( _jumpPressed && _canJump ) {
                _jumpPressed = false;
                Vector3 velocityXZ = new Vector3( _rb.velocity.x, 0, _rb.velocity.z );

                if ( velocityXZ.magnitude > _dashSensitivity * 0.5f ) {
                    velocityXZ = velocityXZ.normalized;
                    _rb.AddForce( new Vector3( velocityXZ.x * _jumpDash * 10f, _jumpForce, velocityXZ.z * _jumpDash * 10f ) * 10f );
                }
                else
                    _rb.AddForce( new Vector3( 0, _jumpForce * 10f ) );
            }
        }
    }
}