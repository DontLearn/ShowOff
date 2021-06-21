using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ShroomAttack : AttackAbs {
    private Combat.Attack _attack = null;
    //private NavMeshAgent _agent = null;
    private float _attackRange = 2f;
    private float _attackMargin = 0.5f;
    private bool _damageDone = false;



    private void Start() {
        LoadComponents();
    }


    private void LoadComponents() {
        _attack = GetComponent<Combat.Attack>();
        Debug.Assert( _attack, $"{this}: No attack found on {name}." );

        /*_agent = GetComponent<NavMeshAgent>();
        Debug.Assert( _agent, $"{this}: NavMeshAgent component missing on {name}." );*/

        if ( _attack && _attack is Combat.RadialAttack ) {
            _attackRange = ( ( Combat.RadialAttack ) _attack ).Radius;
        }
    }


    public override void Attack( Transform target, bool agentEnabled ) {
        if ( target ) {
            if ( Vector3.Distance( transform.position, target.position ) <= _attackRange - _attackMargin ) {
                if ( _attack && !_damageDone ) {
                    // HIT
                    if ( _attack.Strike() ) {
                        _damageDone = true;
                    }
                }
            }
        }
        /*if ( _agent && !agentEnabled ) {
            _agent.SetDestination( transform.position );
        }*/
    }


    public override void OnStateExit() {
        _damageDone = false;
    }
}