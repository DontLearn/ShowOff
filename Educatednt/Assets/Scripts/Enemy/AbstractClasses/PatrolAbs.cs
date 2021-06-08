using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PatrolAbs : MonoBehaviour
{
    public abstract void OnStateEnter();

    public abstract void Patrol( bool agentEnabled );
}
