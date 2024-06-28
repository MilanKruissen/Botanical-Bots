using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateRA : State
{
    [SerializeField] private EndBoss boss;
    [SerializeField] private Animator anim;

    [SerializeField] private State MovingToPlayerState;

    [SerializeField] private float abilityTime;
    public float abilityTimer;

    [SerializeField] private float rocketRate;
    public float rocketTimer;

    [SerializeField] private GameObject missile;
    [SerializeField] private Transform missileSpawnPoint;

    private void Start()
    {
        boss = FindObjectOfType<EndBoss>();
    }

    public override State RunCurrentState()
    {
        anim.SetBool("IsHoming", true);

        boss.explosionRadiusIsReady = false;

        abilityTimer += Time.deltaTime;
        rocketTimer += Time.deltaTime;

        if (rocketTimer >= rocketRate)
        {

            ShootMissile();
            rocketTimer = 0;
        }

        if (abilityTimer >= abilityTime)
        {
            anim.SetBool("IsHoming", false);
            return MovingToPlayerState;
        }

        return this;
    }

    private void ShootMissile()
    {
        SoundManager.PlaySound(SoundManager.Sounds.missileLaunch, 0.03f);
        Instantiate(missile, missileSpawnPoint.position, missile.transform.rotation);
    }
}
