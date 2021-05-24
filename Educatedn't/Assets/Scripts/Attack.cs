using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public abstract class Attack : MonoBehaviour
    {
        [SerializeField, Range( 0, 100 )]
        protected int damage = 10;

        [SerializeField, Range( 0f, 100f )]
        protected float hitForce = 20f;

        [SerializeField]
        protected int layerMask = 1 << 11;


        public abstract void Strike();
    }
}