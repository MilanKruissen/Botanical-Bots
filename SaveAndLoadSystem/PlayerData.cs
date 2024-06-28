using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool saveStarted;

    public float health;
    public float[] position;

    public static int deaths;
    public static float timeSinceDeath;

    public bool pistol;
    public bool katana;
    public bool shotgun;
    public bool ar;
    public bool minigun;

    public bool thornTrap;
    public bool healingTower;
    public bool turret;

    // Level One
    public bool cutsceneOne;
    public bool cutsceneTwo;
    public bool receptionist;
    public int objectiveIndex;

    // Level Two
    public bool firstTimeSpawned;
    public int payloadCheckpointIndex;

    public int playerCoins;

    public bool bossTime;

    public PlayerData (PlayerManager player)
    {
        saveStarted = player.saveStarted;
        health = player.currentHealth;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        pistol = player.pistolFound;
        katana = player.katanaFound;
        shotgun = player.shotgunFound;
        ar = player.assaultRifleFound;
        minigun = player.minigunFound;

        playerCoins = player.playerCoins;

        thornTrap = player.thornTrapFound;
        healingTower = player.healingTowerFound;
        turret = player.turretFound;

        cutsceneOne = player.cutsceneOneDestroyed;
        cutsceneTwo = player.cutsceneTwoDestroyed;
        receptionist = player.receptionistDestroyed;

        objectiveIndex = player.objectiveIndex;
        firstTimeSpawned = player.firstTimeSpawned;
        payloadCheckpointIndex = player.payloadCheckpointIndex;
        bossTime = player.bossTime;
    }
}
