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


        public override bool Strike() {
            if ( null != _hitBox ) {
                // Get all colliders overlapping our hitbox
                Collider[] colliders = Physics.OverlapBox( transform.position + _hitBox.center, _hitBox.bounds.extents, Quaternion.identity, layerMask, QueryTriggerInteraction.Ignore );

                // looping through the colliders found
                if ( colliders.Length > 0 ) {
                    HitColliders( colliders );
                    return true;
                }
            }
            else Debug.LogWarning( $"No hitbox detected for {name}'s MeleeAttack script when trying to attack." );
            return false;
        }
    }
}