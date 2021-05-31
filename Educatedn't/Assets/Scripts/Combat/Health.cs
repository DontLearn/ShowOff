using System;
using UnityEngine;


namespace Combat
{
    class Health : MonoBehaviour, IHittable
    {
        [SerializeField, Range( 0, 1000 )]
        private int hitPoints = 100;

        [SerializeField, Range( 1f, 10f )]
        private float stability = 2f;
		
		// added by Attila
		
		[SerializeField]
		private HealthBar _healthBar;

        [SerializeField] private PlayAudioSource _damageSound;
        [SerializeField] private PlayAudioSource _killSound;

        private void Awake()
        {
            _healthBar.SetMaxHealth(hitPoints);
            _healthBar.SetHealth(hitPoints);
        }

        //---------------------------------
        public void Hit( int damage ) {
            hitPoints = Math.Max( hitPoints - damage, 0 );
            Debug.Log( $"{name} was hit for {damage} damage!" );
            // edited by Attila
            _healthBar.SetHealth(hitPoints);
            _damageSound.PlayAudio();
            // --------------------------

            if ( IsDead() ) Die();
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
        }

        public bool IsDead() {
            return hitPoints <= 0;
            // edited by Attila
            _killSound.PlayAudio();
            // --------------------------
        }
    }
}
