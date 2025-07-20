using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.AI;

public class TestAggro : MonoBehaviour
{
    //private UnitData unitData;
    private NavMeshAgent navMeshAgent;
    private Unit.UnitFinder unitFinder;
    public LayerMask findedLayer;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = 2;
        unitFinder = new Unit.UnitFinder();
    }

    void Update()
    {
        Unit.Unit target = unitFinder.FindNearUnit(this.transform.position, 5f, findedLayer, this.transform);
        if (target != null) 
            navMeshAgent.SetDestination(target.transform.position);
        else
        {
            Debug.Log("비어있음");
        }
    }
}
