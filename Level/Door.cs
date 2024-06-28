using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip doorOpening;
    [SerializeField] private AudioClip doorClosing;

    [SerializeField] private Animator anim;

    [SerializeField] private bool isInRange;
    
    public bool canBeOpened;
    [SerializeField] bool lockOnExit;

    float open;
    float close;

    Color red = Color.red;
    Color green = Color.green;
    [SerializeField] Light light;
    float duration = 1f;

    [SerializeField] private bool useKeycard;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (canBeOpened)
        {
            if(light.color != green)
            {
                close = 0f;
                open += Time.deltaTime;
                light.color = Color.Lerp(red, green, open);
            }
            
        }
        else
        {
            if (light.color != red)
            {
                open = 0f;
                close += Time.deltaTime;
                light.color = Color.Lerp(green, red, close);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC") || other.CompareTag("Enemy"))
        {
            if (canBeOpened == true)
            {
                audioSource.PlayOneShot(doorOpening);
                anim.Play("OpenDoor");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC") || other.CompareTag("Enemy"))
        {
            if (canBeOpened == true)
            {
                audioSource.PlayOneShot(doorClosing);
                anim.Play("CloseDoor");
            }
        }
        if (lockOnExit)
        {
            canBeOpened = false;
        }
    }

    public void OpenDoor()
    {
        anim.Play("OpenDoor");
    }

    public void CloseDoor()
    {
        anim.Play("CloseDoor");
    }
}
