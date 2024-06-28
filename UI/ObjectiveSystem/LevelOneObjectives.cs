using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelOneObjectives : MonoBehaviour
{
    [SerializeField] private GameObject objectiveUI;

    [SerializeField] private Objective[] objectives;

    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        UpdateObjective();
    }

    private void UpdateObjective()
    {
        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i].objectiveIndex == playerManager.objectiveIndex)
            {
                objectives[i].gameObject.SetActive(true);
            }

            if (objectives[i].objectiveIndex != playerManager.objectiveIndex)
            {
                objectives[i].gameObject.SetActive(false);
            }
        }
    }
}
