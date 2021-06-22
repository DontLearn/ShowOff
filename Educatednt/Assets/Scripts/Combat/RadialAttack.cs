using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Combat {
    public class RadialAttack : Attack {
        public float Radius => _hitBox.radius;


        [SerializeField]
        private SphereCollider _hitBox = null;



        public override void SetDamage( int amount ) {
            damage = amount;
        }


        public override void SetKnockback( int amount ) {
            hitForce = amount;
        }


        public override bool Strike() {
            if ( null != _hitBox ) {
                // Get all colliders overlapping our hitbox
                Vector3 hitboxLocation = Vector3.RotateTowards( _hitBox.center.normalized, transform.forward, Mathf.PI * 2f, 0f ) * _hitBox.center.z + transform.up * _hitBox.center.y;
                Collider[] colliders = Physics.OverlapSphere( transform.position + _hitBox.center, _hitBox.radius, layerMask, QueryTriggerInteraction.Ignore );

                // looping through the colliders found
                if ( colliders.Length > 0 ) {
                    HitColliders( colliders );
                    return true;
                }
            }
            else Debug.LogWarning( $"No hitbox detected for {name}'s RadialAttack script when trying to attack." );
            return false;
        }


        public void SetRadius( float radius ) {
            _hitBox.radius = radius;
        }
    }
}