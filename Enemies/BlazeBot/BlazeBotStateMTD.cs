using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlazeBotStateMTD : State
{
    [SerializeField] private Animator anim;
    [SerializeField] private State attackState;
    [SerializeField] private BlazeBotStateATT attack;

    [SerializeField] private BlazeBot blazeBot;
    [SerializeField] private NavMeshAgent blazeBotAgent;
    [SerializeField] private FieldOfView fov;

    private Transform player;
    private Transform payload;

    public float attackRange;
    private float distanceToPlayer;
    private float distanceToPayload;

    private void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>().transform;
        payload = FindObjectOfType<Payload>().transform;
    }

    public override State RunCurrentState()
    {
        anim.SetBool("IsWalking", true);
        blazeBotAgent.speed = blazeBot.movementSpeed;

        SetDestination();
        CheckDistance();

        if (distanceToPlayer <= attackRange && fov.canSeePlayer)
        {
            attack.attackPlayer = true;
            return attackState;
        }
        else if (distanceToPayload <= attackRange && fov.canSeePlayer)
        {
            attack.attackPlayer = false;
            return attackState;
        }
        else
        {
            return this;
        }
    }

    private void SetDestination()
    {
        blazeBotAgent.SetDestination(player.position);
    }

    private void CheckDistance()
    {
        distanceToPlayer = Vector3.Distance(blazeBot.transform.position, player.position);
        distanceToPayload = Vector3.Distance(blazeBot.transform.position, payload.position);
    }
}
