using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player {
    [RequireComponent( typeof( Rigidbody ) )]
    public class PlayerMovement : MonoBehaviour {
        [SerializeField, Range( 6f, 32f )]
        private float _speed = 13f;

        [SerializeField, Range( 25f, 90f )]
        private float _acceleration = 65f;

        [SerializeField, Range( 3f, 7f )]
        private float _rotationSpeed = 5f;


        private Rigidbody _rb = null;
        private Vector2 _movement = Vector3.zero;



        void Start() {
            LoadComponents();
        }


        private void LoadComponents() {
            _rb = GetComponent<Rigidbody>();
            Debug.Assert( null != _rb, $"RigidBody component missing on {name}." );
        }


        private void FixedUpdate() {
            // ROTATE
            Rotate();

            // MOVE
            Move();
        }


        public void SetMovementInput( Vector2 movementInput ) {
            _movement = new Vector2( movementInput.x, movementInput.y );
        }


        public float GetVelocityXZ() {
            if ( null != _rb ) {
                return new Vector2( _rb.velocity.x, _rb.velocity.z ).magnitude;
            }
            else return 0;
        }


        public float GetVelocityY() {
            if ( null != _rb ) {
                return _rb.velocity.y;
            }
            else return 0;
        }


        private void Rotate() {
            Vector3 velocityXZ = new Vector3( _rb.velocity.x, 0, _rb.velocity.z );
            if ( Mathf.Abs( _rb.velocity.x ) > 0.03f || Mathf.Abs( _rb.velocity.z ) > 0.03f ) {
                transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.LookRotation( velocityXZ.normalized ), Time.fixedDeltaTime * _rotationSpeed );
            }
        }


        private void Move() {
            if ( Mathf.Abs( _movement.x ) > 0.03f || Mathf.Abs( _movement.y ) > 0.03f ) {
                _rb.AddForce( new Vector3( _movement.x, 0, _movement.y ) * _acceleration );
                Vector3 velocityXZ = new Vector3( _rb.velocity.x, 0, _rb.velocity.z );
                if ( velocityXZ.magnitude > _speed ) {
                    velocityXZ = velocityXZ.normalized * _speed;
                    _rb.velocity = new Vector3( velocityXZ.x, _rb.velocity.y, velocityXZ.z );
                }
            }
        }
    }
}