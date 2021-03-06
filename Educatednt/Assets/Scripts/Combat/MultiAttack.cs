using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Combat
{
    public class MultiAttack : Attack
    {
        public enum Hitbox
        {
            FRONT,
            BELOW
        }

        [SerializeField, Range( 0, 100 )]
        private int _diveDamage = 20;

        [SerializeField, Range( -100f, 100f )]
        private float _diveForce = -15f;

        [SerializeField]
        private BoxCollider _hitBoxFront = null;

        [SerializeField]
        private BoxCollider _hitBoxBelow = null;


        private BoxCollider _currentActive = null;



        public void SetActiveHitbox( Hitbox hitbox ) {
            switch ( hitbox ) {
                case Hitbox.FRONT:
                    _currentActive = _hitBoxFront;
                    break;
                case Hitbox.BELOW:
                    _currentActive = _hitBoxBelow;
                    break;
                default:
                    _currentActive = _hitBoxFront;
                    break;
            }
            if ( _currentActive == null ) {
                Debug.LogWarning( $"No hitbox selected for {name}'s MultiAttack script when selecting a hitbox." );
            }
        }


        public override bool Strike() {
            if ( null != _currentActive ) {
                // Get all colliders overlapping our hitbox
                Vector3 hitboxLocation = Vector3.RotateTowards( _currentActive.center.normalized, transform.forward, Mathf.PI * 2f, 0f ) * _currentActive.center.z + transform.up * _currentActive.center.y;
                Collider[] colliders = Physics.OverlapBox( transform.position + hitboxLocation, _currentActive.bounds.extents, transform.rotation, layerMask, QueryTriggerInteraction.Ignore );


                // looping through the colliders found
                if ( colliders.Length > 0 ) {
                    if ( _currentActive == _hitBoxBelow ) {
                        HitColliders( colliders, _diveDamage, _diveForce );
                    }
                    else {
                        HitColliders( colliders );
                    }
                    return true;
                }
            }
            else Debug.LogWarning( $"No hitbox detected for {name}'s MultiAttack script when trying to attack." );
            return false;
        }
    }
}