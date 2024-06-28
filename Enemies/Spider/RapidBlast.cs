using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RapidBlast : MonoBehaviour
{
    private NavMeshAgent rapidBlastAgent;
    private Rigidbody rb;
  
    [SerializeField] private GameObject floatingDamageText;

    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    public float movementSpeed;

    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject thisEnemy;

    public bool isInTrap;

    private void Start()
    {
        rapidBlastAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezePositionY;

        currentHealth = maxHealth;
        rapidBlastAgent.speed = movementSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Payload>())
        {
            collision.gameObject.GetComponent<Payload>().DamageTaken(250);
            Explode();
        }

        if (collision.gameObject.GetComponent<BlazeBot>())
        {
            collision.gameObject.GetComponent<BlazeBot>().DamageTaken(250);
            Explode();
        }
    }

    public void DamageTaken(float damage)
    {
        currentHealth = currentHealth - damage;

        DamageIndicator indicator = Instantiate(floatingDamageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(damage);

        if (currentHealth <= 0)
        {
            FindObjectOfType<PlayerManager>().AddCoins(40);

            gameObject.SetActive(false);
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

    private void Explode()
    {
        Debug.Log("Explode");
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(thisEnemy);
    }
}
