using System;
using UnityEngine;


namespace Combat
{
    class Health : MonoBehaviour, IHittable
    {
        [SerializeField, Range( 0, 1000 )]
        private int hitPoints = 100;

        [SerializeField, Range( .01f, 10f )]
        private float stability = 2f;

        [SerializeField]
        private HealthBar _healthBar;

        [SerializeField]
        private PlayAudioSource _damageSound;

        [SerializeField]
        private PlayAudioSource _killSound;



        private void Start() {
            if ( _healthBar ) {
                _healthBar.SetMaxHealth( hitPoints );
                _healthBar.SetHealth( hitPoints );
            }
            else {
                Debug.LogError( $"No Health Bar found on {name}." );
            }
        }


        public void Hit( int damage ) {
            hitPoints = Math.Max( hitPoints - damage, 0 );
            Debug.Log( $"{name} was hit for {damage} damage!" );

            if ( _healthBar )
                _healthBar.SetHealth( hitPoints );
            
            if ( IsDead() )
                Die();
            else if ( _damageSound )
                _damageSound.PlayAudio();
        }


        public void Hit( int damage, Vector3 force ) {
            Hit( damage );
            Rigidbody rb = GetComponent<Rigidbody>();
            if ( rb && stability > Mathf.Epsilon ) {
                rb.AddForce( force * ( 10f / stability ) );
            }
        }


        public void Die() {
            Debug.Log( $"{name} has died!" );
            Destroy( gameObject, 2f );

            Rigidbody rb = GetComponent<Rigidbody>();
            if ( rb ) {
                rb.constraints = RigidbodyConstraints.None;
            }
            if ( _killSound ) {
                _killSound.PlayAudio();
            }
        }


        public bool IsDead() {
            return hitPoints <= 0;
        }
    }
}
