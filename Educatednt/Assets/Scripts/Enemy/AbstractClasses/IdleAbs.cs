using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class IdleAbs : MonoBehaviour
{
    public abstract void OnStateEnter();

    public abstract void Idle( bool agentEnabled );
}
