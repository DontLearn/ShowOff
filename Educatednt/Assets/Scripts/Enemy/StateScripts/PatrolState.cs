using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyBaseClassFSM {
    private PatrolAbs _patrol = null;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        base.OnStateEnter( animator, stateInfo, layerIndex );
        if ( !_patrol ) {
            _patrol = gameObject.GetComponent<PatrolAbs>();
            Debug.Assert( _patrol, $"{this}: Patrolling component missing on {gameObject.name}." );
        }

        if ( _patrol )
            _patrol.OnStateEnter();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        if ( _patrol ) {
            _patrol.Patrol( AgentEnabled );
        }

        if ( detection )
            detection.DetectTarget();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {

    }
}