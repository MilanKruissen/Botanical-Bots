using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossHomingMissile : MonoBehaviour
{
    private Transform target;
    [SerializeField] private GameObject explosion;

    [SerializeField] private float turnSpeed = 5f; 
    [SerializeField] private float rocketSpeed = 10;

    [SerializeField] private float explosionRadius;
    [SerializeField] private float damage;

    private float rocketHeight = 4;
    private float distance;

    private Rigidbody rb;

    private void Awake()
    {
        target = FindObjectOfType<PlayerManager>().transform;
        rb = GetComponent<Rigidbody>();
    }

    private void CheckDistance()
    {
        distance = Vector3.Distance(transform.position, target.position);
    }

    private void Update()
    {
        CheckDistance();

        timer += Time.deltaTime;
        upwardsForce -= 4 * Time.deltaTime;

        if (distance <= 6)
        {
            rocketHeight = -1;
        }
    }

    private float timer;
    public float upwardsForce = 40;

    private void FixedUpdate()
    {
        if (timer <= 0.6f)
        {
            rb.AddForce(0, upwardsForce, 0, ForceMode.Impulse);
        }
            
        if (target == null)
            return;

        Vector3 targetDirection = (target.position + new Vector3(0, rocketHeight, 0) - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed));
 
        rb.velocity = transform.forward * rocketSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndBoss"))
        {
            Debug.Log("");

        }
        else
        {
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        SoundManager.PlaySound(SoundManager.Sounds.explosion, 0.3f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<PlayerManager>())
            {
                float distance = Vector3.Distance(transform.position, colliders[i].transform.position);

                colliders[i].GetComponent<PlayerManager>().DamageToPlayer(damage);
            }
        }

        Destroy(gameObject);
    }
}

