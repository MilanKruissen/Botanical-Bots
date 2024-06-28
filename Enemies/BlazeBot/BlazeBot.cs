using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlazeBot : MonoBehaviour
{
    [SerializeField] private GameObject floatingDamageText;
    [SerializeField] private GameObject thisEnemy;

    [SerializeField] private float maxHealth;

    private float currentHealth;
    public float movementSpeed;
    public bool isInTrap;

    private NavMeshAgent blazeBotAgent;

    private void Start()
    {
        blazeBotAgent = GetComponent<NavMeshAgent>();

        blazeBotAgent.speed = movementSpeed;
        currentHealth = maxHealth;
    }

    public void DamageTaken(float damage)
    {
        currentHealth = currentHealth - damage;
        try
        {
            DamageIndicator indicator = Instantiate(floatingDamageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
            indicator.SetDamageText(damage);
        }
        catch(System.Exception e)
        {
            Debug.Log(e);
        }
        

        if (currentHealth <= 0)
        {
            FindObjectOfType<PlayerManager>().AddCoins(25);

            Destroy(thisEnemy);
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
}
