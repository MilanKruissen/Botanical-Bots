using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    private ThornTrap thornTrap;
    private HealingTower healingTower;
    private Turret turret;
    private PlayerManager playerManager;

    public GameObject[] objects;
    private GameObject currentObject;

    private Vector3 mousePos;
    private RaycastHit hit;

    private bool objectEquiped;

    public bool canPlace;

    // Ability 
    [SerializeField] private GameObject thornTrapIcon;
    public bool abilityOne;
    public bool abilityOneReady;

    [SerializeField] private GameObject healingTreeIcon;
    public bool abilityTwo;
    public bool abilityTwoReady;

    [SerializeField] private GameObject turretIcon;
    public bool abilityThree;
    public bool abilityThreeReady;

    int currentAbillity;

    [SerializeField] private float timer;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Door door;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Start()
    {
        if (playerManager.thornTrapFound)
        {
            AbilityOneAchieved();
        }
        if (playerManager.healingTowerFound)
        {
            AbilityTwoAchieved();
        }
        if (playerManager.turretFound)
        {
            AbilityThreeAchieved();
        }
    }

    public void AbilityOneAchieved()
    {
        playerManager.thornTrapFound = true;
        thornTrapIcon.SetActive(true);

        if (door != null)
        {
            door.canBeOpened = true;
        }

        abilityOne = true;
        abilityOneReady = true;
    }

    public void AbilityTwoAchieved()
    {
        playerManager.healingTowerFound = true;
        healingTreeIcon.SetActive(true);

        abilityTwo = true;
        abilityTwoReady = true;
    }

    public void AbilityThreeAchieved()
    {
        playerManager.turretFound = true;
        turretIcon.SetActive(true);

        abilityThree = true;
        abilityThreeReady = true;
    }

    private void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            playerManager.thornTrapTimer -= Time.deltaTime;
            playerManager.healingTowerTimer -= Time.deltaTime;
            playerManager.turretTimer -= Time.deltaTime;

            if (playerManager.thornTrapTimer < 0 && abilityOne == true)
            {
                abilityOneReady = true;
            }
            if (playerManager.healingTowerTimer < 0 && abilityTwo == true)
            {
                abilityTwoReady = true;
            }
            if (playerManager.turretTimer < 0 && abilityThree == true)
            {
                abilityThreeReady = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && objectEquiped == false && abilityOneReady == true)
            {
                SelectObject(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && objectEquiped == false && abilityTwoReady == true)
            {
                SelectObject(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && objectEquiped == false && abilityThreeReady == true)
            {
                SelectObject(2);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && objectEquiped == true)
            {
                cancelAbillity();
            }

            if (currentObject != null)
            {
                currentObject.transform.position = mousePos;

                if (Input.GetKeyDown(KeyCode.Mouse0) && objectEquiped == true && canPlace == true && currentAbillity == 0)
                {
                    thornTrap = currentObject.GetComponent<ThornTrap>();

                    thornTrap.isPlaced = true;
                    abilityOneReady = false;
                    playerManager.thornTrapTimer = playerManager.thornTrapCooldown;
                    PlaceObject();
                }
                else

                if (Input.GetKeyDown(KeyCode.Mouse0) && objectEquiped == true && canPlace == true && currentAbillity == 1)
                {
                    healingTower = currentObject.GetComponent<HealingTower>();

                    healingTower.isPlaced = true;
                    abilityTwoReady = false;
                    playerManager.healingTowerTimer = playerManager.healingTowerCooldown;
                    PlaceObject();
                }
                else
                if (Input.GetKeyDown(KeyCode.Mouse0) && objectEquiped == true && canPlace == true && currentAbillity == 2)
                {
                    turret = currentObject.GetComponent<Turret>();

                    turret.isPlaced = true;
                    abilityThreeReady = false;
                    playerManager.turretTimer = playerManager.turretCooldown;
                    PlaceObject();
                }
            }
        }
    }

    private void PlaceObject()
    {
        objectEquiped = false;
        currentObject = null;
        PlayerManager.canGunsFire = true;
    }
    
    void cancelAbillity()
    {
        objectEquiped = false;
        Destroy(currentObject);
        currentObject = null;
        PlayerManager.canGunsFire = true;
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 

        if (Physics.Raycast(ray, out hit, 1000, layerMask)) 
        {
            mousePos = hit.point;   
        }
    }

    public void SelectObject(int index)
    {
        PlayerManager.canGunsFire = false;
        objectEquiped = true;
        currentAbillity = index;
        currentObject = Instantiate(objects[index], mousePos, transform.rotation);
    }
}
