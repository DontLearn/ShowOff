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

        [SerializeField, Range(0f, 1f)]
        private float _percentageJumpForce = 0.6f;


        private Rigidbody _rb = null;
        private bool _jumpPressed = false;


        private float _force = 0f;

        




        protected override void Awake() {
            base.Awake();

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
            _jumpForce = data[ Data.JUMPFORCE ];
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
                    _rb.AddForce( new Vector3( velocityXZ.x * _jumpDash, _force, velocityXZ.z * _jumpDash ) * 100f );
                }
                else {
                    _rb.AddForce( new Vector3( 0, _force * 100f ) );
                }
            }
        }


        public void SetLevel( int level ) {
            _force = ( level == 0 ) ? _jumpForce * _percentageJumpForce : _jumpForce;
            if (_jumpForce > 0)
            {
                Debug.Log($"{this}: Actual amount of jump force: {_force / _jumpForce * 100f}%.");

            }
           
        }


        public override void Load( PersistentData persistentData ) {
            base.Load( persistentData );
            int jumpForce;
            if ( !persistentData.TryGetIntData( Data.JUMPFORCE.ToString(), out jumpForce ) ) {
                Debug.LogError( $"{this} Can't parse {Data.JUMPFORCE}, not an int." );
            }
            _jumpForce = jumpForce;

            data[ Data.JUMPFORCE ] = ( int )_jumpForce;
            Debug.Log( $"{this}: Loaded jump force to {_jumpForce}." );
        }


        public override void Save( PersistentData persistentData ) {
            persistentData.SetIntData( Data.JUMPFORCE.ToString(), ( int )_jumpForce );
            Debug.Log( $"{this}: Saved jump force to {_jumpForce}." );
        }
    }
}