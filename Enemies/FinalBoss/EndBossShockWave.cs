using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossShockWave : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private PlayerBehaviour player;

    [SerializeField] private int pointsCount;
    [SerializeField] private float maxRadius;
    [SerializeField] private float speed;
    [SerializeField] private float startWidth;
    [SerializeField] private float damage;

    private bool playerHit = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = pointsCount + 1;

        StartCoroutine(ShockWave());
    }

    private void Update()
    {
        if (lineRenderer.widthMultiplier <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ShockWave()
    {
        float currentRadius = 0f;

        while (currentRadius <= maxRadius)
        {
            currentRadius += Time.deltaTime * speed;
            Draw(currentRadius);
            Damage(currentRadius);
            yield return null;
        }
    }

    private void Damage(float currentRadius)
    {
        if (player.isDashing == true)
        {
            playerHit = true;
        }

        if (playerHit is false)
        {
            Collider[] player = Physics.OverlapSphere(transform.position, currentRadius);

            foreach (Collider hit in player)
            {
                if (hit.GetComponent<PlayerManager>() != null)
                {
                    hit.GetComponent<PlayerManager>().DamageToPlayer(damage);
                    playerHit = true;
                }
            }
        }
    }

    private void Draw(float currentRadius)
    {
        float angleBetweenPoints = 360f / pointsCount;

        for (int i = 0; i <= pointsCount; i++)
        {
            float angle = i * angleBetweenPoints * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
            Vector3 position = direction * currentRadius;

            lineRenderer.SetPosition(i, position);
        }

        lineRenderer.widthMultiplier = Mathf.Lerp(0f, startWidth, 1f - currentRadius / maxRadius);
    }
}
