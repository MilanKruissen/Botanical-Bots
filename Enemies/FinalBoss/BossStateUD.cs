using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossStateUD : State
{
    [SerializeField] private EndBoss boss;
    [SerializeField] private NavMeshAgent bossAgent;
    [SerializeField] private Transform player;
    [SerializeField] private Transform bossLoc;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BossStateMTP moveToPlayerState;
    [SerializeField] private Animator anim;

    [SerializeField] private float chargeTime;
    private float chargeTimer;

    [SerializeField] private float dashDamage;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;

    public bool hasDashed = false;

    public override State RunCurrentState()
    {
        anim.SetBool("IsDashing", true);
        chargeTimer += Time.deltaTime;

        if (chargeTimer >= chargeTime)
        {
            Dash();
        }
        else
        {
            SetDestination();
            bossAgent.speed = 0.2f;
        }

        if (hasDashed == true)
        {
            anim.SetBool("IsDashing", false);

            return moveToPlayerState;
        }

        return this;
    }

    private void Dash()
    {
        if (hasDashed == false)
        {
            boss.isDashing = true;

            Vector3 dashDirection = bossAgent.transform.forward;
            Vector3 dashVelocity = dashDirection * dashSpeed;

            rb.velocity = dashVelocity;

            bossAgent.enabled = false;

            StartCoroutine(EndDash());
        }
    }

    private IEnumerator EndDash()
    {
        yield return new WaitForSeconds(dashDuration); // Adjust the duration as needed

        rb.velocity = Vector3.zero;

        boss.isDashing = false;
        bossAgent.enabled = true;
        bossAgent.speed = boss.movementSpeed;

        chargeTimer = 0;

        boss.superDashIsReady = false;
        hasDashed = true;
    }

    private void SetDestination()
    {
        bossAgent.SetDestination(player.position);
    }
}
