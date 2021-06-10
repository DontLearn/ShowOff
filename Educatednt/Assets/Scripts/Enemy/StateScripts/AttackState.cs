using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackState : EnemyBaseClassFSM {
    private AttackAbs _attack = null;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        base.OnStateEnter( animator, stateInfo, layerIndex );
        if ( !_attack ) {
            _attack = transform.GetComponent<AttackAbs>();
            if ( !_attack )
            {
                _attack = transform.GetComponentInParent<AttackAbs>();
            }
            Debug.Assert( _attack, $"{this}: Attacking component missing on {transform.name}." );
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        if ( _attack ) {
            _attack.Attack( detection.Target, AgentEnabled );
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        if ( _attack ) {
            _attack.OnStateExit();
        }
    }
}