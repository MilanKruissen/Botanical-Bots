using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityState : State
{
    [SerializeField] private WalkingState walkingState;

    [SerializeField] private GameObject trapLine;
    [SerializeField] private Transform boss;
    [SerializeField] private Transform player;
    [SerializeField] private Animator anim;

    private MiniBoss miniBoss;
    public float chargeTime;

    private void Start()
    {
        miniBoss = FindObjectOfType<MiniBoss>();
        chargeTime = 0;
    }

    public override State RunCurrentState()
    {
        anim.SetBool("IsStomping", true);

        LookAtPlayer();
   
        chargeTime += Time.deltaTime;

        if (chargeTime >= 3)
        {
            TrapLine();

            anim.SetBool("IsStomping", false);

            return walkingState;
        }

        return this;
    }

    private void LookAtPlayer()
    {
        boss.LookAt(player);
    }

    private void TrapLine()
    {
        Vector3 trapPos = new Vector3(boss.position.x, trapLine.transform.position.y, boss.position.z);
        Instantiate(trapLine, trapPos, boss.rotation);
        miniBoss.abilityUsed();
    }
}
