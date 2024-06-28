using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLineCollCheck : MonoBehaviour
{
    private int blockIndex;

    private TrapLineCollCheck[] checks;

    private void Start()
    {
        checks = FindObjectsOfType<TrapLineCollCheck>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            for (int i = 0; i < checks.Length; i++)
            {
                if (checks[i].blockIndex >= blockIndex)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }
}
