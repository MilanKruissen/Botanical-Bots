using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossBullet : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerManager playerManager;

    [SerializeField] private float bulletSpeed = 40;
    [SerializeField] private float damage;

    public float accuracy = 30;

    private void Awake()
    {
        transform.Rotate(0, Random.Range(-accuracy, accuracy), 0);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    void Update()
    {
        rb.AddForce(transform.forward * bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerManager.DamageToPlayer(damage);
        }
        else if (other.CompareTag("Boss"))
        {
            return;
        }
        else if (other.CompareTag("Bullet"))
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
