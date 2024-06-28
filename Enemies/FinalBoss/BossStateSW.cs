using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateSW : State
{
    [SerializeField] private State movingToPlayerState;
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject shockWave;
    [SerializeField] private Transform boss;

    [SerializeField] private float abilityTime;
    public float abilityTimer;

    [SerializeField] private float shockWaveRate;
    public float shockWaveTimer;

    public override State RunCurrentState()
    {
        anim.SetBool("IsShockwaving", true);

        boss.GetComponent<EndBoss>().shockwaveIsReady = false;

        abilityTimer += Time.deltaTime;
        shockWaveTimer += Time.deltaTime;

        if (shockWaveTimer >= shockWaveRate)
        {
            ShockwaveAttack();
            shockWaveTimer = 0;
        }

        if (abilityTimer >= abilityTime)
        {
            anim.SetBool("IsShockwaving", false);

            return movingToPlayerState;
        }

        return this;
    }

    private void ShockwaveAttack()
    {
        Instantiate(shockWave, boss.position + new Vector3(0, 0.2f, 0), shockWave.transform.rotation);
    }
}
