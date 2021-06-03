using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AttackAbs : MonoBehaviour
{
    public abstract void Attack( Transform target, bool agentEnabled );

    public abstract void OnStateExit();
}
