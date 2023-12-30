using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
public class CustomerMotor : Motor
{    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Transform Target)
    {
        target = Target;
    }

}
