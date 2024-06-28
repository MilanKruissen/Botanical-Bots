using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingState : State
{
    private MiniBoss miniBoss;

    [SerializeField] private FieldOfView bossView;
    [SerializeField] private NavMeshAgent bossAgent;
    [SerializeField] private WalkingState walkingState;
    [SerializeField] private AbilityState abilityState;
    [SerializeField] private UltState ultState;

    [SerializeField] private float burstTime;
    [SerializeField] private float burstTimer;

    [SerializeField] private float fireRate;
    [SerializeField] private float shootTimer;

    [SerializeField] private float timeNotSeeingPlayer;
    [SerializeField] private float timeUntilSearching;

    [SerializeField] private float breakTime;
    [SerializeField] private float breakTimer;

    public Transform bulletSpawnpoint;
    [SerializeField] private GameObject bossBullet;

    [SerializeField] private Transform player;
    [SerializeField] private Animator anim;

    private AudioSource audioSource;
    [SerializeField] private AudioClip minigun;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        miniBoss = FindObjectOfType<MiniBoss>();
    }

    public override State RunCurrentState()
    {
        anim.SetBool("IsShooting", true);
        // Amount of time the boss is going to shoot
        burstTimer += Time.deltaTime;

        if (burstTimer <= burstTime)
        {
            Shooting();
        }
        else
        {
            shootTimer = 0;
            breakTimer += Time.deltaTime;

            if (breakTimer >= breakTime)
            {
                burstTimer = 0;
                breakTimer = 0;
            }
        }

        if (miniBoss.abilityReady == true)
        {
            return abilityState;
        }

        if (miniBoss.ultReady == true)
        {
            return ultState;
        }

        // What happens if the boss cant see the player
        if (bossView.canSeePlayer == false)
        {
            timeNotSeeingPlayer += Time.deltaTime;

            if (timeNotSeeingPlayer >= timeUntilSearching)
            {
                return walkingState;
            }
        }

        return this;
    }

    private void Shooting()
    {
        bossAgent.SetDestination(player.position);
        bossAgent.speed = 0.8f;

        shootTimer += Time.deltaTime;

        if (shootTimer >= fireRate)
        {
            Instantiate(bossBullet, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
            audioSource.PlayOneShot(minigun);
            shootTimer = 0;
        }
    }
}
