using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip hackingSound;

    private BuildingManager buildManager;
    private bool isInArea;
    private bool activateCanvas = true;

    [SerializeField] private GameObject laptopCanvas;
    [SerializeField] private int abilityIndex;

    private bool laptopUsed = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (activateCanvas == true)
            {
                laptopCanvas.SetActive(true);
            }

            isInArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (activateCanvas == true)
            {
                laptopCanvas.SetActive(false);
            }

            isInArea = false;
        }
    }

    private void Update()
    {
        if (isInArea == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && laptopUsed == false)
            {
                buildManager = FindObjectOfType<BuildingManager>();
                ActivateHack();

                audioSource.PlayOneShot(hackingSound);

                laptopUsed = true;
                activateCanvas = false;
                GetComponent<ActivateDoor>().openDoors = true;
            }
        }

        if (activateCanvas == false)
        {
            Destroy(laptopCanvas);
        }
    }

    private void ActivateHack()
    {
        switch (abilityIndex)
        {
            default:
                Debug.Log("");
                break;
            case 1:
                buildManager.AbilityOneAchieved();
                break;
            case 2:
                buildManager.AbilityTwoAchieved();
                break;
            case 3:
                buildManager.AbilityThreeAchieved();
                break;
        }
    }
}
