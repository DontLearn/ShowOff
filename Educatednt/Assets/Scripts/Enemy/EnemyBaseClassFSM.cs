using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyBaseClassFSM : StateMachineBehaviour {
    protected bool AgentEnabled => _agentEnabled;


    protected GameObject gameObject = null;
    protected Detection detection = null;
    private NavMeshAgent _agent = null;
    private Rigidbody _rb = null;
    private bool _agentEnabled = true;



    override public void OnStateEnter( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        gameObject = animator.gameObject;

        _agent = gameObject.GetComponent<NavMeshAgent>();
        Debug.Assert( _agent, $"{this}: Animator holder {gameObject.name} is missing a NavMeshAgent script!" );

        _rb = gameObject.GetComponent<Rigidbody>();
        if ( null == _rb ) {
            Debug.Log( $"{this}: Animator holder {gameObject.name} does not have a RigidBody script." );
        }

        detection = gameObject.GetComponent<Detection>();
        if ( null == detection ) {
            Debug.Log( $"{this}: Animator holder {gameObject.name} does not have a Detection script." );
        }
    }


    protected void EnableAgent() {
        _agentEnabled = true;
        _agent.enabled = true;
        _rb.isKinematic = false;
    }



    protected void DisableAgent() {
        _agentEnabled = false;
        _agent.SetDestination( gameObject.transform.position );
        _agent.enabled = false;
        _rb.isKinematic = false;
    }
}