using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ChaseAbs : MonoBehaviour {
    public abstract void Chase( Transform target, bool agentEnabled );
}
