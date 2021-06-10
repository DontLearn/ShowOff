using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyBaseClassFSM : StateMachineBehaviour {
    protected bool AgentEnabled => _agentEnabled;


    protected Transform transform = null;
    protected Detection detection = null;
    private NavMeshAgent _agent = null;
    private Rigidbody _rb = null;
    private bool _agentEnabled = true;



    override public void OnStateEnter( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        transform = animator.transform;

        _agent = transform.GetComponent<NavMeshAgent>();
        if ( !_agent ) {
            _agent = transform.GetComponentInParent<NavMeshAgent>();
        }
        Debug.Assert( _agent, $"{this}: Animator holder {transform.name} is missing a NavMeshAgent script!" );

        _rb = transform.GetComponent<Rigidbody>();
        if ( !_rb ) {
            _rb = transform.GetComponentInParent<Rigidbody>();
        }
        if ( null == _rb ) {
            Debug.Log( $"{this}: Animator holder {transform.name} does not have a RigidBody script." );
        }

        detection = transform.GetComponent<Detection>();
        if ( !detection)
        {
            detection = transform.GetComponentInParent<Detection>();
        }
        if ( null == detection ) {
            Debug.Log( $"{this}: Animator holder {transform.name} does not have a Detection script." );
        }
    }


    protected void EnableAgent() {
        _agentEnabled = true;
        _agent.enabled = true;
        _rb.isKinematic = false;
    }



    protected void DisableAgent() {
        _agentEnabled = false;
        _agent.SetDestination( transform.transform.position );
        _agent.enabled = false;
        _rb.isKinematic = false;
    }
}