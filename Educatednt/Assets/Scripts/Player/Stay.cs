using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class Stay : MonoBehaviour
    {
        void Update()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}