using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;


namespace Player {
    [RequireComponent( typeof( PlayerAnimator ), typeof( Rigidbody ) )]
    public class PlayerAttack : MonoBehaviour {
        [SerializeField, Range( 0f, 10f )]
        private float _downForce = 5f;

        [SerializeField, Range( 5f, 25f )]
        private float _bounceForce = 17f;

        [SerializeField, Range( 0f, 18f )]
        private float _bounceDash = 11.5f;

        [SerializeField, Range( 0.01f, 0.99f )]
        private float _slow = 0.7f;


        private Combat.Attack _attack = null;
        private MultiAttack.Hitbox _activeHitbox = MultiAttack.Hitbox.FRONT;
        private PlayerAnimator _playerAnimator = null;
        private Rigidbody _rb = null;
        private bool _isGrounded = true;
        private bool _attackPressed = false;



        private void Start() {
            LoadComponents();
        }


        private void LoadComponents() {
            _attack = GetComponent<Combat.Attack>();
            Debug.Assert( null != _attack, $"Attack component missing on {name}." );

            _playerAnimator = GetComponent<PlayerAnimator>();
            Debug.Assert( null != _playerAnimator, $"PlayerAnimator component missing on {name}." );

            _rb = GetComponent<Rigidbody>();
            Debug.Assert( null != _rb, $"RigidBody component missing on {name}." );
        }


        public void SetGrounded( bool pIsGrounded ) {
            _isGrounded = pIsGrounded;
        }


        public void AttackPressed() {
            _attackPressed = true;

            if ( !_isGrounded ) {
                float velY = _rb.velocity.y;
                if ( velY <= 0.4f ) {
                    _rb.velocity = new Vector3(
                        _rb.velocity.x * ( 1f - Mathf.Pow( _slow, 2f ) ),
                        _rb.velocity.y - _downForce,
                        _rb.velocity.z * ( 1f - Mathf.Pow( _slow, 2f ) )
                    );
                }
            }
        }


        public void SetActiveHitbox( MultiAttack.Hitbox hitbox ) {
            _activeHitbox = hitbox;
            if ( _activeHitbox == MultiAttack.Hitbox.FRONT )
                _attackPressed = false;
            if ( _attack && _attack is MultiAttack )
                ( ( MultiAttack )_attack ).SetActiveHitbox( hitbox );
        }


        private void FixedUpdate() {
            if ( _attack ) {
                float velY = _rb.velocity.y;
                if ( _attack is MultiAttack && !_isGrounded && velY <= 0.4f ) {
                    DiveAttack();
                }
                else {
                    FrontalAttack();
                }
            }
        }


        private void FrontalAttack() {
            if ( _attackPressed ) {
                _attackPressed = false;
                _attack.Strike();
            }
        }


        private void DiveAttack() {
            if ( _attackPressed ) {
                _rb.velocity = new Vector3(
                    _rb.velocity.x * ( 1f - Mathf.Pow( _slow, 2f ) ),
                    _rb.velocity.y,
                    _rb.velocity.z * ( 1f - Mathf.Pow( _slow, 2f ) )
                );

                _rb.AddForce( Vector3.down * _downForce * 10f );

                // Always attacking, checking for hits
                if ( _attack.Strike() ) {
                    _attackPressed = false;
                    Bounce();
                }
            }
        }


        private void Bounce() {
            Vector3 velocityXZ = new Vector3( _rb.velocity.x, 0, _rb.velocity.z ).normalized;
            _rb.velocity = velocityXZ;

            _rb.AddForce( 100f * new Vector3(
                velocityXZ.x * _bounceDash,
                _bounceForce,
                velocityXZ.z * _bounceDash )
            );
        }
    }
}
