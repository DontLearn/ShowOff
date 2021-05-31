using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player {
    [RequireComponent( typeof( Animator ))]
    public class PlayerAnimator : MonoBehaviour {
        [SerializeField]
        private AnimatorOverrideController[] _attackOverride = null;


        private Animator _animator = null;



        private void Start() {
            LoadComponents();
        }


        private void LoadComponents() {
            _animator = GetComponent<Animator>();
            Debug.Assert( null != _animator, $"Animator component missing on {name}." );
        }


        public void SetVelocityXZ( float velocityXZ ) {
            if ( _animator )
                _animator.SetFloat( "VelocityXZ", velocityXZ );
        }

        public void SetVelocityY( float velocityY ) {
            if ( _animator )
                _animator.SetFloat( "VelocityY", velocityY );
        }


        public void SetGrounded( bool isGrounded ) {
            if ( _animator )
                _animator.SetBool( "Grounded", isGrounded );
        }


        public void TriggerAttack() {
            if ( _animator ) {
                if ( !_animator.GetCurrentAnimatorStateInfo( 0 ).IsTag( "Attack" ) ) {
                    int randomAttack = Random.Range( 0, _attackOverride.Length );
                    SetAnimations( randomAttack );

                    _animator.SetTrigger( "Attack" );
                }
            }
        }


        private void SetAnimations( int value ) {
            if ( value >= 0 && value <= _attackOverride.Length - 1 ) {
                AnimatorOverrideController controller = _attackOverride[ value ];
                if ( null != controller )
                    _animator.runtimeAnimatorController = controller;
            }
        }
    }
}