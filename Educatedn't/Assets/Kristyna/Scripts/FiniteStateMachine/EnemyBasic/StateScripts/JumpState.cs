using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : EnemyBaseClassFSM {
    private JumpAbs _jump = null;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        base.OnStateEnter( animator, stateInfo, layerIndex );
        if ( !_jump ) {
            _jump = gameObject.GetComponent<JumpAbs>();
            Debug.Assert( _jump, $"{this}: Jumping component missing on {gameObject.name}." );
        }

        Debug.Log( "Disabling agent." );
        DisableAgent();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        if ( _jump ) {
            _jump.Jump();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {

    }
}