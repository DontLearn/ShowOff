using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClassFSM : StateMachineBehaviour
{
    protected GameObject NPC;
    protected EnemyAI ai;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        ai = NPC.GetComponent<EnemyAI>();
        Debug.Assert(ai, "EnemyBaseClassFSM: Animator holder does not hold EnemyAi script!");
    }
}
