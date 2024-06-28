using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossStateDA : State
{
    [SerializeField] private State moveToPlayerState;
    [SerializeField] private Animator anim;

    //[SerializeField] private Animator anim;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform boss;
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent bossAgent;

    [SerializeField] private float attackingMovementSpeed;
    [SerializeField] private float range = 0.5f;
    [SerializeField] private int damage = 15;
    [SerializeField] private float attackRate;

    private float nextAttackTime;

    [SerializeField] private float distanceToPlayer;
    [SerializeField] private float extraTimeAttacking;
    public float timer;

    public override State RunCurrentState()
    {
        anim.SetBool("IsAttacking", true);

        CheckDistance();
        SetDestination();

        bossAgent.speed = attackingMovementSpeed;

        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }

        if (distanceToPlayer >= moveToPlayerState.GetComponent<BossStateMTP>().attackRange)
        {
            anim.SetBool("IsAttacking", false);
            timer += Time.deltaTime;
        }

        if (timer >= extraTimeAttacking)
        {

            bossAgent.speed = boss.GetComponent<EndBoss>().movementSpeed;

            return moveToPlayerState;
        }

        return this;
    }

    private void Attack()
    {
        //anim.SetTrigger("Attack");

        Collider[] player = Physics.OverlapSphere(attackPoint.position, range);

        foreach (Collider hit in player)
        {
            if (hit.GetComponent<PlayerManager>() != null)
            {
                hit.GetComponent<PlayerManager>().DamageToPlayer(damage);
            }
        }
    }

    private void SetDestination()
    {
        bossAgent.SetDestination(player.position);
    }

    private void CheckDistance()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, boss.transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, range);
        Gizmos.DrawWireSphere(boss.position, moveToPlayerState.GetComponent<BossStateMTP>().attackRange);
    }
}
