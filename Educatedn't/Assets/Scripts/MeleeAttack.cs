using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Combat
{
    public class MeleeAttack : Attack
    {
        [SerializeField]
        private BoxCollider _hitBox;


        public override void Strike() {
            if ( null != _hitBox ) {
                // Get all colliders overlapping our hitbox
                Collider[] colliders = Physics.OverlapBox( transform.position + _hitBox.center, _hitBox.bounds.extents, Quaternion.identity, layerMask, QueryTriggerInteraction.Ignore );
                
                // looping through the colliders found
                if ( colliders.Length > 0 ) {
                    foreach ( Collider col in colliders ) {
                        // not ourself?
                        if ( col.gameObject != gameObject ) {
                            Debug.Log( $"Found {name}!" );
                            // detecting whether or not they can be hit
                            IHittable hp = col.GetComponent<IHittable>();
                            if ( null != hp && !hp.IsDead() ) {
                                // HIT!
                                Vector3 force = transform.forward * hitForce;
                                hp.Hit( damage, force );
                            }
                        }
                    }
                }
            }
            else Debug.LogWarning( $"No hitbox detected for {name}'s MeleeAttack script." );
        }
    }
}