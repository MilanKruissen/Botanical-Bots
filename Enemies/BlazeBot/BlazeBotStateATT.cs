using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlazeBotStateATT : State
{
    [SerializeField] private Animator anim;
    [SerializeField] private BlazeBotStateMTD moveToDestState;
    [SerializeField] private NavMeshAgent blazeBotAgent;
    [SerializeField] private FieldOfView fov;
    [SerializeField] private BlazeBot blazeBot;
    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform bulletSpawnPoint;
    private Transform player;
    private Transform payload;

    private float distanceToDestination;

    [SerializeField] private float firerate;
    private float shootTimer;

    public bool attackPlayer;

    private void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>().transform;
        payload = FindObjectOfType<Payload>().transform;
    }

    public override State RunCurrentState()
    {
        anim.SetBool("IsWalking", false);

        CheckDistance();

        blazeBotAgent.speed = 0.3f;

        shootTimer += Time.deltaTime;

        if (attackPlayer == true)
        {
            blazeBotAgent.SetDestination(player.position);
        }
        else
        {
            blazeBotAgent.SetDestination(payload.position);
        }

        if (shootTimer >= firerate && fov.canSeePlayer == true)
        {
            shootTimer = 0;

            Shooting();
        }

        if (distanceToDestination >= moveToDestState.attackRange)
        {
            return moveToDestState;
        }

        return this;
    }

    private void CheckDistance()
    {
        if (attackPlayer)
        {
            distanceToDestination = Vector3.Distance(blazeBot.transform.position, player.position);
        }
        else
        {
            distanceToDestination = Vector3.Distance(blazeBot.transform.position, payload.position);
        }
    }

    private void Shooting()
    {
        SoundManager.PlaySound(SoundManager.Sounds.pistolBulet, 0.06f);

        Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
