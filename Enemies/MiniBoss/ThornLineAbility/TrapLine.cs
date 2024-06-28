using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLine : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private float trapTimer;
    [SerializeField] private float trapTime;

    private void Start()
    {
        anim = GetComponent<Animator>();

        anim.Play("TrapLine");
    }

    private void Update()
    {
        trapTimer += Time.deltaTime;

        if (trapTimer >= trapTime)
        {
            anim.Play("DestroyTrapLine");
            Destroy(gameObject);
        }
    }
}
