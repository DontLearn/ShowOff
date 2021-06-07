using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class JumpAbs : MonoBehaviour
{
    public abstract void OnStateEnter( params object[] args );

    //public abstract void Jump();

    public abstract void OnStateExit();
}