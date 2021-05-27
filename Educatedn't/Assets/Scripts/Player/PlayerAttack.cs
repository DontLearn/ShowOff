using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;


namespace Player {
    [RequireComponent( typeof( PlayerAnimator ), typeof( Rigidbody ) )]
    public class PlayerAttack : MonoBehaviour {
        [SerializeField, Range( 0f, 12f )]
        private float _diveForce = 8f;

        [SerializeField, Range( 16f, 38f )]
        private float _bounceForce = 28f;

        [SerializeField, Range( 0f, 6f )]
        private float _bounceDash = 1.5f;


        private Combat.Attack _attack = null;
        [SerializeField]
        private MultiAttack.Hitbox _activeHitbox = MultiAttack.Hitbox.FRONT;
        private PlayerAnimator _playerAnimator = null;
        private Rigidbody _rb = null;
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


        public void AttackPressed() {
            _attackPressed = true;
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
                if ( _attack is MultiAttack ) {
                    switch ( _activeHitbox ) {
                        case MultiAttack.Hitbox.FRONT:
                            FrontalAttack();
                            break;
                        case MultiAttack.Hitbox.BELOW:
                            DiveAttack();
                            break;
                    }
                }
                else {
                    _attack.Strike();
                    _attackPressed = false;
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
            _rb.AddForce( Vector3.down * _diveForce );

            if ( _attackPressed ) {
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
            _rb.AddForce( new Vector3( velocityXZ.x * _bounceDash * 10f, _bounceForce, velocityXZ.z * _bounceDash * 10f ) * 10f );
        }
    }
}
