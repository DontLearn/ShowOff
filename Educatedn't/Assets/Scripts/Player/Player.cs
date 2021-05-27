using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    [RequireComponent( typeof( PlayerAnimator ), typeof( PlayerMovement ) )]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private Controls _controls = null;


        private PlayerAnimator _playerAnimator = null;
        private PlayerMovement _movement = null;
        private PlayerJump _jump = null;
        private PlayerAttack _attack = null;

        // controls
        private string _forwardAxis = "";
        private string _sideAxis = "";
        private KeyCode _jumpButton = KeyCode.Escape;
        private int _attackButton = -1;

        private Vector2 _axisInversion = Vector2.one;


        private void Start() {
            LoadComponents();
            LoadInput();
        }


        private void Update() {
            // Move
            Move();

            // Jump
            Jump();

            // Attack
            Attack();
        }


        private void FixedUpdate() {
            CheckVelocity();
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
            if ( _movement && moveInput.magnitude > 0 )
                _movement.SetMovementInput( moveInput );
        }


        private void CheckVelocity() {
            if ( _movement ) {
                float velXZ = _movement.GetVelocityXZ();
                float velY = _movement.GetVelocityY();

                if ( _playerAnimator ) {
                    _playerAnimator.SetVelocityXZ( velXZ );
                    _playerAnimator.SetVelocityY( velY );
                }


                if ( _attack ) {
                    if ( velY >= -0.15f )
                        _attack.SetActiveHitbox( Combat.MultiAttack.Hitbox.FRONT );
                    else
                        _attack.SetActiveHitbox( Combat.MultiAttack.Hitbox.BELOW );
                }
            }
        }

        
        private void Jump() {
            if ( _jump && Input.GetKeyDown( _jumpButton ) && IsGrounded() ) {
                _jump.JumpPressed();
            }
        }


        private void Attack() {
            if ( Input.GetMouseButtonDown( _attackButton ) ) {
                if ( _attack )
                    _attack.AttackPressed();

                if ( _playerAnimator )
                    _playerAnimator.TriggerAttack();
            }
        }


        private bool IsGrounded() {
            Vector3 origin = transform.position + Vector3.up * 0.4f;
            Vector3 direction = new Vector3( 0, -1, 0 );
            float maxDistance = 0.6f;
            int layerMask = 1;

            bool grounded = Physics.Raycast( origin, direction, maxDistance, layerMask );


            if ( _playerAnimator )
                _playerAnimator.SetGrounded( grounded );

            return grounded;
        }
    }
}