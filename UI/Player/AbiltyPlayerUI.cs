using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbiltyPlayerUI : MonoBehaviour
{
    private PlayerManager player;

    [SerializeField] private Image thornTrapIcon;
    [SerializeField] private Image grenadeIcon;
    [SerializeField] private Image healingTreeIcon;
    [SerializeField] private Image turretIcon;

    private void Start()
    {
        player = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        thornTrapIcon.fillAmount = player.thornTrapTimer / player.thornTrapCooldown;
        grenadeIcon.fillAmount = player.grenadeTimer / player.grenadeCooldown;
        healingTreeIcon.fillAmount = player.healingTowerTimer / player.healingTowerCooldown;
        turretIcon.fillAmount = player.turretTimer / player.turretCooldown;
    }
}
