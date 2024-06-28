using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorBar : MonoBehaviour
{
    public Image armorBar;
    private PlayerManager playerManager;

    private float currentArmor;

    private void Start()
    {
        armorBar = GetComponent<Image>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        currentArmor = playerManager.currentArmor;
        armorBar.fillAmount = currentArmor / playerManager.maxArmor;
    }
}
