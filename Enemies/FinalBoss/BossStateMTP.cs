using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossStateMTP : State
{
    private EndBoss boss;

    [SerializeField] private State attackState;
    [SerializeField] private State superDashState;
    [SerializeField] private State shockwaveState;
    [SerializeField] private State explosionRadiusState;

    [SerializeField] private NavMeshAgent bossAgent;
    [SerializeField] private Transform player;

    public float attackRange;
    private float distanceToPlayer;

    private void Start()
    {
        boss = FindObjectOfType<EndBoss>();
    }

    public override State RunCurrentState()
    {
        SetDestination();
        CheckDistance();

        // Attack the player when close by
        if (distanceToPlayer <= attackRange)
        {
            attackState.GetComponent<BossStateDA>().timer = 0;
            return attackState;
        }

        // Superdash
        if (boss.superDashIsReady == true)
        {
            superDashState.GetComponent<BossStateUD>().hasDashed = false;
            return superDashState;
        }

        // Shockwave
        if (boss.shockwaveIsReady == true)
        {
            shockwaveState.GetComponent<BossStateSW>().abilityTimer = 0;
            shockwaveState.GetComponent<BossStateSW>().shockWaveTimer = 0;
            return shockwaveState;
        }

        // ExplosionRadius
        if (boss.explosionRadiusIsReady == true)
        {
            explosionRadiusState.GetComponent<BossStateRA>().abilityTimer = 0;
            explosionRadiusState.GetComponent<BossStateRA>().rocketTimer = 0;
            return explosionRadiusState;
        }

        return this;
    }

    private void SetDestination()
    {
        bossAgent.SetDestination(player.position);
    }

    private void CheckDistance()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, boss.transform.position);
    }
}
