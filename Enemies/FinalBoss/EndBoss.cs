using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EndBoss : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
 
    [SerializeField] private GameObject floatingDamageText;
    [SerializeField] private GameObject bossHealthbar;

    private NavMeshAgent bossAgent;
    private PlayerManager player;

    public float movementSpeed;

    [SerializeField] private float abilityCooldown;
    private float abilityTimer;
    private int abilityIndex;

    public bool superDashIsReady = false;
    public bool shockwaveIsReady = false;
    public bool explosionRadiusIsReady = false;

    public bool isInTrap;
    public bool isDashing = false;

    private void Start()
    {
        bossHealthbar.SetActive(true);
        currentHealth = maxHealth;

        bossAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerManager>();

        bossAgent.speed = movementSpeed;
    }

    private void Update()
    {
        abilityTimer += Time.deltaTime;

        if (abilityTimer >= abilityCooldown)
        {
            AbilityReady();
            abilityTimer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isDashing == true)
            {
                player.DamageToPlayer(60);
            }
            else
            {
                player.DamageToPlayer(5);
            }
        }
    }

    private void AbilityReady()
    {
        abilityIndex = Random.Range(0, 3);

        if (abilityIndex == 0)
        {
            ResetAbilities();
            superDashIsReady = true;
        }
        else if (abilityIndex == 1)
        {
            ResetAbilities();
            shockwaveIsReady = true;
        }
        else if (abilityIndex == 2)
        {
            ResetAbilities();
            explosionRadiusIsReady = true;
        }
    }

    private void ResetAbilities()
    {
        superDashIsReady = false;
        shockwaveIsReady = false;
        explosionRadiusIsReady = false;
    }

    public void DamageTaken(float damage)
    {
        currentHealth = currentHealth - damage;

        DamageIndicator indicator = Instantiate(floatingDamageText, transform.position + new Vector3(0, 5, 0), Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(damage);

        if (currentHealth <= 0)
        {
            FindObjectOfType<PlayerManager>().AddCoins(640);

            Dead();
        }
    }

    public IEnumerator DamageOverTime(float damage)
    {
        while (isInTrap)
        {
            DamageTaken(damage);
            yield return new WaitForSeconds(1f);
        }
    }

    [SerializeField] private GameObject barier;

    [SerializeField] private UpdateObjective ob1;
    [SerializeField] private UpdateObjective ob2;

    private void OnEnable()
    {
        ob1.UpdateObjectives();
        barier.SetActive(true);
    }

    private void Dead()
    {
        ob2.UpdateObjectives();

        barier.SetActive(false);
        bossHealthbar.SetActive(false);

        Debug.Log("Boss Is Dead");
        Destroy(gameObject);
    }
}
