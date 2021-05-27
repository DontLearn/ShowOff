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

        [SerializeField, Range( 0f, 100f )]
        private float _diveForce = 15f;

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
                Collider[] colliders = Physics.OverlapBox( transform.position + _currentActive.center, _currentActive.bounds.extents, Quaternion.identity, layerMask, QueryTriggerInteraction.Ignore );


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