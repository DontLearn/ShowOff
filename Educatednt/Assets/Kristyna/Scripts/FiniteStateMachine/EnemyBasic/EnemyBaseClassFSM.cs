using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyBaseClassFSM : StateMachineBehaviour {
    protected bool AgentEnabled => _agentEnabled;


    protected GameObject gameObject = null;
    protected Detection detection = null;
    private NavMeshAgent _agent = null;
    private bool _agentEnabled = true;



    override public void OnStateEnter( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        gameObject = animator.gameObject;

        detection = gameObject.GetComponent<Detection>();
        Debug.Assert( detection, $"{this}: Animator holder {gameObject.name} is missing a Detection script!" );

        _agent = gameObject.GetComponent<NavMeshAgent>();
        Debug.Assert( _agent, $"{this}: Animator holder {gameObject.name} is missing a NavMeshAgent script!" );
    }


    protected void EnableAgent() {
        _agentEnabled = true;
    }



    protected void DisableAgent() {
        _agentEnabled = false;
        _agent.SetDestination( gameObject.transform.position );
    }
}