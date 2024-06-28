using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMoveToDestination : State
{
    [SerializeField] private State attackState;

    [SerializeField] private RapidBlast rapidBlast;
    [SerializeField] private NavMeshAgent rapidBlastAgent;
    [SerializeField] private FieldOfView fov;

    [SerializeField] private Transform destination;

    [SerializeField] private float attackRange;
    private float distanceToDestination;

    private void Start()
    {
        destination = FindObjectOfType<Payload>().transform;
    }

    public override State RunCurrentState()
    {
        SetDestination();
        CheckDistance();

        if (distanceToDestination <= attackRange && fov.canSeePlayer)
        {
            return attackState;
        }
        else
        {
            return this;
        }
    }

    private void SetDestination()
    {
        rapidBlastAgent.SetDestination(destination.position);
    }

    private void CheckDistance()
    {
        distanceToDestination = Vector3.Distance(rapidBlast.transform.position, destination.position);
    }
}
