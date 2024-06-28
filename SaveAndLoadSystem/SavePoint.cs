using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private PlayerManager player;

    private void Start()
    {
        player = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.SavePlayer();
        }
    }
}
