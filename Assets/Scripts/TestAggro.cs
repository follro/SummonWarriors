using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.AI;

public class TestAggro : MonoBehaviour
{
    //private UnitData unitData;
    private NavMeshAgent navMeshAgent;
    public Transform target;
    void Start()
    {
        //unitData = GetComponent<Unit.Unit>().UnitStatus;
        navMeshAgent =GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = 2;
    }

    void Update()
    {
        navMeshAgent.SetDestination(target.position);
    }
}
