using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLineTrap : MonoBehaviour
{
    private PlayerManager player;

    private bool isInTrap = false;
    public int trapIndex;

    private void Start()
    {
        player = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrap = true;
            StartCoroutine(DamageOverTime());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrap = false;
        }
    }

    public IEnumerator DamageOverTime()
    {
        while (isInTrap)
        {
            player.DamageToPlayer(10);
            yield return new WaitForSeconds(1f);
        }

    }
}
