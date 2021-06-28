using UnityEngine;


namespace Combat {
    public abstract class Attack : MonoBehaviour {
        public int Damage => damage;
        public int Knockback => ( int )hitForce;
        public int AttackLevel => attackLevel;


        [SerializeField, Range( 0, 100 )]
        protected int damage = 10;

        [SerializeField, Range( 0f, 100f )]
        protected float hitForce = 70f;

        [SerializeField]
        protected int layerMask = 1 << 11;


        protected int attackLevel = 0;


        public abstract void SetAttackLevel( int amount );
        public abstract void SetDamage( int amount );
        public abstract void SetKnockback( int amount );
        public abstract void LevelUp( int level );
        public abstract bool Strike();



        /*public void HitHittables( IHittable[] hittables ) {
            foreach( IHittable hittable in hittables ) {
                // not ourself?
                if ( hittable != GetComponent<IHittable>() ) {
                    // Hit
                    Vector3 displacement = transform.forward * hitForce;
                    hittable.Hit( damage, displacement );
                }
            }
        }

        public void HitHittables( IHittable[] hittables, int dmg, float force ) {
            foreach ( IHittable hittable in hittables ) {
                // not ourself?
                if ( hittable != GetComponent<IHittable>() ) {
                    // Hit
                    Vector3 displacement = transform.forward * force;
                    hittable.Hit( dmg, displacement );
                }
            }
        }*/


        public void HitColliders( Collider[] colliders ) {
            foreach ( Collider col in colliders ) {
                // not ourself?
                if ( col.gameObject != gameObject ) {
                    Debug.Log( $"Found {col.name}!" );
                    // detecting whether or not they can be hit
                    IHittable hp = col.GetComponent<IHittable>();
                    if ( null != hp && !hp.IsDead() ) {
                        // HIT!
                        Vector3 displacement = transform.forward * hitForce;
                        hp.Hit( damage, displacement );
                    }
                }
            }
        }

        public void HitColliders( Collider[] colliders, int dmg, float force ) {
            foreach ( Collider col in colliders ) {
                // not ourself?
                if ( col.gameObject != gameObject ) {
                    Debug.Log( $"Found {col.name}!" );
                    // detecting whether or not they can be hit
                    IHittable hp = col.GetComponent<IHittable>();
                    if ( null != hp && !hp.IsDead() ) {
                        // HIT!
                        Vector3 displacement = transform.forward * force;
                        hp.Hit( dmg, displacement );
                    }
                }
            }
        }
    }
}