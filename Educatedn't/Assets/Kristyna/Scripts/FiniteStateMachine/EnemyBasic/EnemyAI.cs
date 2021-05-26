using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum ChangeToChase
    {
        FOV,
        Distance
    }
    [Header("General:")]
    public bool showFwdRay = true;
    public float fwdRayDistance = 5;
    
    [Header("Patrol to Chase:")]
    [Tooltip("Based on what to change to the chase state?")]
    public ChangeToChase PatrolToChase = ChangeToChase.FOV;
    //Remake these to show only those given for the current ChangeToChase case
    public float chaseDistance = 5;
    public float viewAngle = 30;
    public float visibleDistance = 5;
    
    [Header("Chase to Attack:")]
    public float attackDistance = 1.0f;

    private PatrolAbs _patrol;
    private ChaseAbs _chase;
    //private AttackAbs _attack;
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _patrol = this.GetComponent<PatrolAbs>();
        Debug.Assert(_patrol, "EnemyAI: PatrolAbs type script not found!");
        
        _chase = this.GetComponent<ChaseAbs>();
        Debug.Assert(_chase, "EnemyAI: ChaseAbs type script not found!");

        //_attack = this.GetComponent<ChaseAbs>();
        //Debug.Assert(_attack, "EnemyAI: Attack type script not found!");

        _player = GameObject.FindGameObjectWithTag("Player");
        Debug.Assert(_player, "EnemyAI: GameObject with tag 'Player' not found!");
    }
    public void Update()
    {
        if (showFwdRay)
        {
            Debug.DrawRay(transform.position, this.transform.forward * fwdRayDistance, Color.yellow);
            Vector3 distance = _player.transform.position - this.transform.position;
            Debug.Log(distance.magnitude);
        }
    }

    public void Patrol()
    {
        _patrol.Patrol();
    }
    public void Chase()
    {
        _chase.Chase(_player);
    }
    public void Attack()
    {
        //_attack.Attack();
    }
    
    public bool ChangePatrolToChaseState()
    {
        switch (PatrolToChase)
        {
            default:
                Debug.Log("Change to chase state not defined!");
                return false;
            case ChangeToChase.FOV:
                Vector3 diffVec = _player.transform.position - this.transform.position;
                float angle = Vector3.Angle(diffVec, this.transform.forward);
                if (diffVec.magnitude <= visibleDistance && angle <= viewAngle)
                {
                    diffVec.y = 0;
                    return true;
                }
                else return false;
            case ChangeToChase.Distance:
                Vector3 distance = _player.transform.position - this.transform.position;
                if (distance.magnitude <= chaseDistance)
                {
                    return true;
                }
                else return false;
        }
    }
    public bool ChangeChaseToAttackState()
    {
        Vector3 distance = _player.transform.position - this.transform.position;
        if (distance.magnitude < attackDistance)
        {
            return true;
        }
        else return false;
    }
    public bool ChangeChaseToPatrolState()
    {
        Vector3 distance = _player.transform.position - this.transform.position;
        if (distance.magnitude > chaseDistance)
        {
            return true;
        }
        else return false;
    }
    public bool ChangeAttackToChaseState()
    {
        Vector3 distance = _player.transform.position - this.transform.position;
        if (distance.magnitude > attackDistance)
        {
            return true;
        }
        else return false;
    }
}
