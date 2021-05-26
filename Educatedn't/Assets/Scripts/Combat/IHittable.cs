using System;
using UnityEngine;


namespace Combat
{
    interface IHittable
    {
        public void Hit( int damage );
        public void Hit( int damage, Vector3 force );
        public void Die();
        public bool IsDead();
    }
}
