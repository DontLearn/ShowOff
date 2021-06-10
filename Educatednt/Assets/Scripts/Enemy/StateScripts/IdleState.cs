using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyBaseClassFSM {
    private IdleAbs _idle = null;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        base.OnStateEnter( animator, stateInfo, layerIndex );
        if ( !_idle ) {
            _idle = transform.GetComponent<IdleAbs>();
            if ( !_idle ) {
                _idle = transform.GetComponentInParent<IdleAbs>();
            }
            Debug.Assert( _idle, $"{this}: Idling component missing on {transform.name}." );
        }

        if ( _idle ) {
            _idle.OnStateEnter();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        if ( _idle ) {
            _idle.Idle( AgentEnabled );
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        Debug.Log( "Enabling agent." );
        EnableAgent();
    }
}