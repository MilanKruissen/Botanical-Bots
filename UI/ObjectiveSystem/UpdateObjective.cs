using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateObjective : MonoBehaviour
{
    private LevelOneObjectives objectives;

    [SerializeField] private int objectiveUpdateIndex;

    private void Start()
    {
        objectives = FindObjectOfType<LevelOneObjectives>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UpdateObjectives();
        }
    }

    public void UpdateObjectives()
    {
        if (FindObjectOfType<PlayerManager>().objectiveIndex == objectiveUpdateIndex - 1)
        {
            FindObjectOfType<PlayerManager>().objectiveIndex++;
        }
    }
}
