using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuriningPlatform : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private bool payloadOnPlatform = false;

    [SerializeField] private Payload payload;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Payload>())
        {
            payloadOnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Payload>())
        {
            payloadOnPlatform = false;
        }
    }

    private void Update()
    {
        if (payloadOnPlatform && payload.isRotating && payload.hasRotated)
        {
            Debug.Log("Payload on platform");
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.Rotate(0, 0, 0);
        }
    }
}
