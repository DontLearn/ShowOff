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




        public void Hit( int damage ) {
            hitPoints = Math.Max( hitPoints - damage, 0 );
            Debug.Log( $"{name} was hit for {damage} damage!" );

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
        }
    }
}
