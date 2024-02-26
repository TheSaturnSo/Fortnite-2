using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public PlayerController player;
    public float viewAngle;

    private NavMeshAgent _navMeshAgent;
    private bool _isPlayerNoticed;

    // Start is called before the first frame update
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_navMeshAgent.remainingDistance == 0)
        {
            _navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
        }

        var direction = player.transform.position - transform.position;

        if (Vector3.Angle(transform.forward, direction) < viewAngle)
        {
            _navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;


            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    _isPlayerNoticed = true;
                }
                else
                {
                    _isPlayerNoticed = false;
                }
            }
            else
            {
                _isPlayerNoticed = false;
            }
        }
        else
        {
            _isPlayerNoticed = false;
        }
        if (_isPlayerNoticed)
        {
            _navMeshAgent.destination = player.transform.position;
        }
        if(_isPlayerNoticed = false)
        {
            if(_navMeshAgent.remainingDistance == 0)
            {
                _navMeshAgent = GetComponent<NavMeshAgent>();
                _navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
            }
        }
    }
}
