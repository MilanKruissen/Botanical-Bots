using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private float radius;
    [SerializeField] private float force;
    [SerializeField] private int damage;

    [SerializeField] private GameObject explosionEffect;

    private float countdown;
    private bool hasExploded = false;

    private void Start()
    {
        countdown = delay;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    private void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        SoundManager.PlaySound(SoundManager.Sounds.explosion, 0.03f);

        Collider[] colliders =  Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObjects in colliders)
        {
            Rigidbody rb = nearbyObjects.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<MiniBoss>())
            {
                float distance = Vector3.Distance(transform.position, colliders[i].transform.position);

                colliders[i].GetComponent<MiniBoss>().DamageTaken(damage);
            }

            if (colliders[i].GetComponent<EnemyOne>())
            {
                float distance = Vector3.Distance(transform.position, colliders[i].transform.position);

                colliders[i].GetComponent<EnemyOne>().DamageTaken(damage);
            }

            if (colliders[i].GetComponent<PlayerManager>())
            {
                float distance = Vector3.Distance(transform.position, colliders[i].transform.position);

                colliders[i].GetComponent<PlayerManager>().DamageToPlayer(damage);
            }

            if (colliders[i].GetComponent<ExplosiveBarrel>())
            {
                float distance = Vector3.Distance(transform.position, colliders[i].transform.position);

                colliders[i].GetComponent<ExplosiveBarrel>().Explode();
            }

            if (colliders[i].GetComponent<EndBoss>())
            {
                float distance = Vector3.Distance(transform.position, colliders[i].transform.position);

                colliders[i].GetComponent<EndBoss>().DamageTaken(damage);
            }

            if (colliders[i].GetComponent<RapidBlast>())
            {
                float distance = Vector3.Distance(transform.position, colliders[i].transform.position);

                colliders[i].GetComponent<RapidBlast>().DamageTaken(damage);
            }

            if (colliders[i].GetComponent<BlazeBot>())
            {
                float distance = Vector3.Distance(transform.position, colliders[i].transform.position);

                colliders[i].GetComponent<BlazeBot>().DamageTaken(damage);
            }
        }

        Destroy(gameObject);
    }
}
