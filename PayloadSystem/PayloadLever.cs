using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayloadLever : MonoBehaviour
{
    private Animator anim;
    private Payload payload;
 
    [SerializeField] private WaveSystem.Wave wave;
 
    [SerializeField] private int leverIndex;

    private bool playerInArea = false;
    public bool leverIsUsed = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        payload = FindObjectOfType<Payload>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = false;
        }
    }

    private void Update()
    {
        if (leverIndex == payload.waypointIndex)
        {
            if (playerInArea && Input.GetKey(KeyCode.E) && !leverIsUsed)
            {
                anim.Play("Lever");

                payload.hasRotated = true;
                leverIsUsed = true;

                wave.SpawnEnemies();
            }
        }
    }
}
