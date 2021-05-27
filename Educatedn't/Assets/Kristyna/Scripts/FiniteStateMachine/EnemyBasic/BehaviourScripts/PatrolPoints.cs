using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolPoints : PatrolAbs
{
    public int startingPoint;
    public float pointAccuracy;
    public Vector3[] points;

    private int _currentPoint;
    private NavMeshAgent _agent;
    private GameObject _character;

    // Start is called before the first frame update
    void Start()
    {
        _character = this.gameObject;
        _agent = _character.GetComponent<NavMeshAgent>();
        Debug.Assert(_agent, "PatrolPoints: NavMeshAgent not found!");
        _currentPoint = startingPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Patrol()
    {        
        if (points.Length == 0)
        {
            Debug.LogWarning("PatrolPoints has no points!");
            return;
        }

        if (Vector3.Distance(points[_currentPoint], _character.transform.position) < pointAccuracy)
        {
            _currentPoint++;
            if (_currentPoint >= points.Length)
            {
                _currentPoint = 0;
            }
            startingPoint = _currentPoint;//once change of state back to patrol, npc starts where it left off
        }

        //Move Character
        _agent.SetDestination(points[_currentPoint]);
    }
}
