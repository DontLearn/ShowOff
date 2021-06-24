using System;
using UnityEngine;
using Data;


namespace Combat {
    class Health : MonoBehaviour, IHittable {
        public int HitPoints => _hitPoints;


        [SerializeField, Range( 0, 1000 )]
        private int _hitPoints = 100;

        [SerializeField, Range( .01f, 10f )]
        private float _stability = 2f;

        [SerializeField]
        private HealthBar _healthBar;

        [SerializeField]
        private PlayAudioSource _damageSound;

        [SerializeField]
        private PlayAudioSource _killSound;



        private void Start() {
            if ( _healthBar ) {
                _healthBar.SetMaxHealth( _hitPoints );
                _healthBar.SetHealth( _hitPoints );
            }
            else {
                Debug.LogError( $"{this}: No Health Bar found on {name}." );
            }
        }


        public void AddHitPoints( int amount ) {
            _hitPoints += amount;
        }


        public void Hit( int damage ) {
            _hitPoints = Math.Max( _hitPoints - damage, 0 );
            Debug.Log( $"{name} was hit for {damage} damage!" );

            if ( _healthBar )
                _healthBar.SetHealth( _hitPoints );
            
            if ( IsDead() )
                Die();
            //else if ( _damageSound )
               // _damageSound.PlayAudio();
        }


        public void Hit( int damage, Vector3 force ) {
            Hit( damage );
            Rigidbody rb = GetComponent<Rigidbody>();
            if ( rb && _stability > Mathf.Epsilon ) {
                rb.AddForce( force * ( 10f / _stability ) );
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
                //_killSound.PlayAudio();
            }
        }


        public bool IsDead() {
            return _hitPoints <= 0;
        }
    }
}