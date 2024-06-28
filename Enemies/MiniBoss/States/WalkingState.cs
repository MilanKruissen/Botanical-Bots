using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingState : State
{
    private MiniBoss miniBoss;

    [Header("States")]
    [SerializeField] private ShootingState shootingState;
    [SerializeField] private AbilityState abilityState;

    [SerializeField] private NavMeshAgent bossAgent;

    [SerializeField] private Transform player;
    [SerializeField] private Animator anim;

    private void Start()
    {
        miniBoss = FindObjectOfType<MiniBoss>();
    }

    public override State RunCurrentState()
    {
        anim.SetBool("IsShooting", false);

        SetDestination();

        if (miniBoss.abilityReady == true)
        {
            return abilityState;
        }

        if (miniBoss.GetComponent<FieldOfView>().canSeePlayer == true)
        {
            return shootingState;
        }
        return this;
    }

    private void SetDestination()
    {
        bossAgent.speed = 4f;
        bossAgent.SetDestination(player.position);
    }
}
