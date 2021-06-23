using UnityEngine;
using Data;


namespace Player {
    public class PlayerJump : PlayerBehaviour {
        [SerializeField, Range( 5f, 30f )]
        private float _jumpForce = 21f;

        [SerializeField, Range( 0f, 18f )]
        private float _jumpDash = 4f;

        [SerializeField, Range( 0f, 1f )]
        private float _dashSensitivity = 0.4f;


        private Rigidbody _rb = null;
        private bool _jumpPressed = false;



        void Start() {
            _rb = GetComponent<Rigidbody>();
            Debug.Assert( null != _rb, $"{name} is missing a RigidBody component." );
        }


        private void Update() {
            if ( !isUpgraded && isLoaded ) {
                // UPGRADE
                Upgrade();
            }
        }


        private void FixedUpdate() {
            // JUMP
            Jump();
        }


        protected override void Upgrade() {
            base.Upgrade();
            _jumpForce = data[ "jumpForce" ];
            Debug.Log( $"{this}: Upgraded jump force. Is now {_jumpForce}." );
        }


        public void JumpPressed() {
            _jumpPressed = true;
        }


        private void Jump() {
            if ( _jumpPressed ) {
                _jumpPressed = false;
                Vector3 velocityXZ = new Vector3( _rb.velocity.x, 0, _rb.velocity.z );

                if ( velocityXZ.magnitude > _dashSensitivity * 0.5f ) {
                    velocityXZ = velocityXZ.normalized;
                    _rb.AddForce( new Vector3( velocityXZ.x * _jumpDash, _jumpForce, velocityXZ.z * _jumpDash ) * 100f );
                }
                else {
                    _rb.AddForce( new Vector3( 0, _jumpForce * 100f ) );
                }
            }
        }
    }
}