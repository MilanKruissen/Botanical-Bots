using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateAttacking : State
{
    [SerializeField] private Animator anim;

    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private NavMeshAgent rapidBlastAgent;

    [SerializeField] private float jumpForce;
    [SerializeField] private float leapForce;

    private float timer;
    public bool isAttacking;

    private void Start()
    {
        target = FindObjectOfType<Payload>().transform;

    }

    public override State RunCurrentState()
    {
        anim.SetBool("IsAttacking", true);

        timer += Time.deltaTime;
        rapidBlastAgent.enabled = false;

        if (isAttacking == false && timer >= 0.7f)
        {
            Attack();
            isAttacking = true;
        }

        return this;
    }

    private void Attack()
    {
        rb.constraints = RigidbodyConstraints.None;
        Vector3 direction = (target.position - rapidBlastAgent.transform.position).normalized;
        
        rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        rb.AddForce(direction * leapForce, ForceMode.Impulse);
    }
}
