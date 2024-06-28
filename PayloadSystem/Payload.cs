using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payload : MonoBehaviour
{
    private PlayerManager player;

    [Header("Movement")]
    [SerializeField] private Transform[] waypoints;
    public int waypointIndex = 0;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private Quaternion targetRotation;
    public bool hasRotated = true; 
    public bool isRotating = false;

    [Header("stats")]
    [SerializeField] private GameObject floatingDamageText;
    [SerializeField] private GameObject payloadHealthbar;

    public float payloadHealth;
    public float currentHealth;

    private void Start()
    {
        player = FindObjectOfType<PlayerManager>();

        if (player.bossTime)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].CloseDoor();
                cutscene.SetActive(true);

                player.bossTime = true;

                waypointIndex = 7;
                player.currentHealth = player.maxHealth;
                player.currentArmor = player.maxArmor;

                player.SavePlayer();
            }
        }

        currentHealth = payloadHealth;

        MoveToNextPoint();
    }

    private void Update()
    {
        if (hasRotated)
        {
            payloadHealthbar.SetActive(true);
        }

        if (waypointIndex >= waypoints.Length)
        {

            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].CloseDoor();
            }

            cutscene.SetActive(true);

            player.bossTime = true;

            player.currentHealth = player.maxHealth;
            player.currentArmor = player.maxArmor;

            player.SavePlayer();

            return;
        }

        Transform targetWaypoint = waypoints[waypointIndex];

        if (isRotating && hasRotated == true)
        {
            targetRotation = Quaternion.LookRotation(targetWaypoint.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                isRotating = false;
            }
        }
        else if(hasRotated == true)
        {
            Vector3 direction = (targetWaypoint.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                hasRotated = false;
                waypointIndex++;
                MoveToNextPoint();
            }
        }
    }

    [SerializeField] private Door[] doors;
    [SerializeField] private GameObject cutscene;

    private void MoveToNextPoint()
    {
        if (waypointIndex >= waypoints.Length)
        {

            cutscene.SetActive(true);

            player.bossTime = true;

            return;
        }

        Transform targetWaypoint = waypoints[waypointIndex];
        targetRotation = Quaternion.LookRotation(targetWaypoint.position - transform.position);
        isRotating = true;
    }

    public void DamageTaken(int damage)
    {
        currentHealth = currentHealth - damage;

        DamageIndicator indicator = Instantiate(floatingDamageText, transform.position + new Vector3(0, 2, 0), Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(damage);

        if (currentHealth <= 0)
        {
            payloadHealthbar.SetActive(false);
            player.Dead();
            Destroy(gameObject);
        }
    }
}
     