using UnityEngine;
using Data;


namespace Player
{
    [RequireComponent( typeof( PlayerAnimator ), typeof( PlayerMovement ) )]
    public class Player : PlayerBehaviour
    {
        [SerializeField]
        private Controls _controls = null;


        private PlayerAnimator _playerAnimator = null;
        private PlayerMovement _movement = null;
        private PlayerJump _jump = null;
        private PlayerAttack _attack = null;
        private int _upgradeLevel = 0;
        private bool _upgraded = false;

        // controls
        private string _forwardAxis = "";
        private string _sideAxis = "";
        private KeyCode _jumpButton = KeyCode.Escape;
        private int _attackButton = -1;

        private Vector2 _axisInversion = Vector2.one;
        private bool _isGrounded = true;


        private void Start() {
            LoadComponents();
            LoadInput();
        }


        private void Update() {
            if ( !_upgraded && isLoaded ) {
                // UPGRADE
                Upgrade();
            }


            // Move
            Move();

            // Jump
            Jump();

            // Attack
            Attack();
        }


        private void FixedUpdate() {
            // Check Ground
            CheckGround();

            // Check Velocity
            CheckVelocity();
        }


        private void Upgrade() {
            _upgradeLevel = data[ "upgrade" ];
            _upgraded = true;

            Debug.Log( $"{this}: Upgraded player's ability level. Is now {_upgradeLevel}." );

            if ( _upgradeLevel < 1 ) {
                _jump = null;
            }
            if ( _upgradeLevel < 2 ) {
                if ( _attack ) {
                    _attack.SetDiveAvailable( false );
                }
            }
        }


        private void LoadInput() {
            if ( _controls ) {
                _forwardAxis = PlayerInput.GetAxis( _controls.Forward );
                if ( _forwardAxis.Contains( "Minus" ) ) {
                    _forwardAxis = _forwardAxis.Remove( 0, 6 );
                    Debug.Log( _forwardAxis );
                    _axisInversion.x = -1;
                }
                else {
                    _axisInversion.x = 1;
                }
                _sideAxis = PlayerInput.GetAxis( _controls.Side );
                if ( _sideAxis.Contains( "Minus" ) ) {
                    _sideAxis = _sideAxis.Remove( 0, 6 );
                    Debug.Log( _sideAxis );
                    _axisInversion.y = -1;
                }
                else {
                    _axisInversion.y = 1;
                }
                _jumpButton = PlayerInput.GetKey( _controls.Jump );
                _attackButton = PlayerInput.GetMouseButton( _controls.Attack );
                Debug.Log( "Player Input successfully loaded :)" );
            }
        }


        private void LoadComponents() {
            _playerAnimator = GetComponent<PlayerAnimator>();
            Debug.Assert( null != _playerAnimator, $"PlayerAnimator component missing on {name}." );

            _movement = GetComponent<PlayerMovement>();
            Debug.Assert( null != _movement, $"PlayerMovement component missing on {name}." );

            _jump = GetComponent<PlayerJump>();
            Debug.Assert( null != _jump, $"PlayerJump component not present on {name}." );

            _attack = GetComponent<PlayerAttack>();
            Debug.Assert( null != _attack, $"PlayerAttack component not present on {name}." );
        }


        private void Move() {
            Vector2 moveInput = new Vector2(
                Input.GetAxis( _forwardAxis ) * _axisInversion.x,
                Input.GetAxis( _sideAxis ) * _axisInversion.y
            );
            if ( _movement )
                _movement.SetMovementInput( moveInput );
        }

        
        private void Jump() {
            if ( _jump && Input.GetKeyDown( _jumpButton ) && _isGrounded ) {
                _jump.JumpPressed();
            }
        }


        private void Attack() {
            if ( Input.GetMouseButtonDown( _attackButton ) && _attack ) {
                if ( _playerAnimator && _playerAnimator.TriggerAttack() ) {
                    _attack.AttackPressed();
                }
            }
        }


        private void CheckGround() {
            Vector3 origin = transform.position + Vector3.up * 0.4f;
            Vector3 direction = new Vector3( 0, -1, 0 );
            float maxDistance = 0.7f;
            int layerMask = 1;

            _isGrounded = Physics.Raycast( origin, direction, maxDistance, layerMask );


            if ( _playerAnimator )
                _playerAnimator.SetGrounded( _isGrounded );

            if ( _attack )
                _attack.SetGrounded( _isGrounded );
        }


        private void CheckVelocity() {
            if ( _movement ) {
                float velXZ = _movement.GetVelocityXZ();
                float velY = _movement.GetVelocityY();

                if ( _playerAnimator ) {
                    if ( velXZ > Mathf.Epsilon )
                        _playerAnimator.SetVelocityXZ( velXZ );
                    _playerAnimator.SetVelocityY( velY );
                }


                if ( _attack ) {
                    if ( _isGrounded )
                        _attack.SetActiveHitbox( Combat.MultiAttack.Hitbox.FRONT );
                    else if ( velY <= 0.4f )
                        _attack.SetActiveHitbox( Combat.MultiAttack.Hitbox.BELOW );

                }
            }
        }
    }
}