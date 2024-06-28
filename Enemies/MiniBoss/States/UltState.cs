using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UltState : State
{
    [SerializeField] private WalkingState walkingState;
    [SerializeField] private NavMeshAgent bossAgent;

    public float ultTimer;
    [SerializeField] private float ultTime;

    private float shootTimer;
    [SerializeField] private float fireRate;

    [SerializeField] private GameObject bossBullet;
    [SerializeField] private Transform bulletSpawnpoint;

    private MiniBoss miniBoss;

    private AudioSource audioSource;
    [SerializeField] private AudioClip minigun;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        miniBoss = FindObjectOfType<MiniBoss>();
    }

    public override State RunCurrentState()
    {
        bossAgent.transform.Rotate(0, 200 * Time.deltaTime, 0);
        Shooting();

        ultTimer += Time.deltaTime;

        if (ultTimer >= ultTime)
        {
            miniBoss.UltUsed();
            return walkingState;
        }
        else
        {
            return this;
        }
    }

    private void Shooting()
    {
        bossAgent.speed = 0.0f;

        shootTimer += Time.deltaTime;

        if (shootTimer >= fireRate)
        {
            Instantiate(bossBullet, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
            audioSource.PlayOneShot(minigun);
            shootTimer = 0;
        }
    }
}
