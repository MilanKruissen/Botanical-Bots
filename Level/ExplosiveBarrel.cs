using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float force;
    [SerializeField] private float damage;

    [SerializeField] private GameObject explosionEffect;

    bool exploded;

    public void Explode(bool fromOtherBarrel = false)
    {
        if (!exploded)
        {
            if (fromOtherBarrel)
            {
                StartCoroutine(fuse());
            }
            else
            {
                exploded = true;
                Instantiate(explosionEffect, transform.position, transform.rotation);

                SoundManager.PlaySound(SoundManager.Sounds.explosion, 0.05f);

                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

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

                    if (colliders[i].GetComponent<PlayerManager>())
                    {
                        float distance = Vector3.Distance(transform.position, colliders[i].transform.position);

                        colliders[i].GetComponent<PlayerManager>().DamageToPlayer(damage);
                    }

                    if (colliders[i].GetComponent<ExplosiveBarrel>() && colliders[i].gameObject != this.gameObject)
                    {
                        float distance = Vector3.Distance(transform.position, colliders[i].transform.position);

                        colliders[i].GetComponent<ExplosiveBarrel>().Explode(true);
                    }
                }

                Destroy(gameObject);
            }
        }
    }

    IEnumerator fuse()
    {
        yield return new WaitForSeconds(0.25f);
        Explode();
    }
}
