using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    private PlayerManager playerManager;

    [SerializeField] private float throwForce;
    [SerializeField] private GameObject grenadePrefab;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        playerManager.grenadeTimer -= Time.deltaTime;

        if (playerManager.grenadeTimer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !PauseMenu.GameIsPaused)
            {
                ThrowGrenade();
                playerManager.grenadeTimer = playerManager.grenadeCooldown;
            }
        }
    }

    private void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        Vector3 mousePos = Input.mousePosition;
        PlayerBehaviour player = GetComponent<PlayerBehaviour>();

        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

        SoundManager.PlaySound(SoundManager.Sounds.grenadeThrow, 0.18f);
    }
}
