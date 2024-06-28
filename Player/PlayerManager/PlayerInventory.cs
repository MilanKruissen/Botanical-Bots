using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject pistolPickup;
    [SerializeField] private GameObject katanaPickup;

    private weaponWheelButtonController[] weaponWeelButton;
    private ShopManager shop;
    private PlayerManager player;
    private Animator anim;

    private WeaponWheelController weaponWheel;

    public bool weaponIsEquiped;

    private Pistol pistol;
    private Katana katana;
    private Shotgun shotgun;
    private AR assaultRifle;
    private Minigun minigun;

    public int coins;

    // Keycards
    public bool blueCard;
    public bool redCard;
    public bool purpleCard;

    // Weapons
    [SerializeField] private Sprite pistolIcon;
    [SerializeField] private Sprite katanaIcon;
    [SerializeField] private Sprite shotgunIcon;
    [SerializeField] private Sprite assaultRifleIcon;
    [SerializeField] private Sprite minigunIcon;

    // utilitie
    public bool medkit;
    public int amountOfMedkits;

    private void Awake()
    {
        weaponWheel = FindObjectOfType<WeaponWheelController>();
        player = FindObjectOfType<PlayerManager>();
    }

    private void Start()
    {
        pistol = FindObjectOfType<Pistol>();
        katana = FindObjectOfType<Katana>();
        shotgun = FindObjectOfType<Shotgun>();
        assaultRifle = FindObjectOfType<AR>();
        minigun = FindObjectOfType<Minigun>();

        anim = GetComponent<Animator>();

        CheckAnimator();

        weaponWeelButton = FindObjectsOfType<weaponWheelButtonController>();

        weaponIsEquiped = false;

        if (player.pistolFound == true)
        {
            Destroy(pistolPickup);
            Pistol();
        }

        if (player.katanaFound == true)
        {
            Destroy(katanaPickup);
            Katana();
        }

        if (player.shotgunFound == true)
        {
            Shotgun();
        }

        if (player.assaultRifleFound == true)
        {
            AssaultRifle();
        }

        if (player.minigunFound == true)
        {
            Minigun();
        }
    }

    // Weapons
    public void Pistol()
    {
        for (int i = 0; i < weaponWeelButton.Length; i++)
        {
            if (weaponWeelButton[i].ID == 1)
            {
                weaponIsEquiped = true;

                player.pistolFound = true;
                weaponWheel.unequipAll();
                pistol.EquipPistol();
                CheckAnimator();

                weaponWeelButton[i].weaponName = "Pistol";
                weaponWeelButton[i].icon = pistolIcon;
                weaponWeelButton[i].iconTab.SetActive(true);
                weaponWeelButton[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void Katana()
    {
        for (int i = 0; i < weaponWeelButton.Length; i++)
        {
            if (weaponWeelButton[i].ID == 2)
            {
                weaponIsEquiped = true;

                player.katanaFound = true;
                weaponWheel.unequipAll();
                CheckAnimator();

                weaponWeelButton[i].weaponName = "Katana";
                weaponWeelButton[i].icon = katanaIcon;
                weaponWeelButton[i].iconTab.SetActive(true);
                weaponWeelButton[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void Shotgun()
    {
        for (int i = 0; i < weaponWeelButton.Length; i++)
        {
            if (weaponWeelButton[i].ID == 3)
            {
                weaponIsEquiped = true;

                player.shotgunFound = true;
                weaponWheel.unequipAll();
                shotgun.EquipShotgun();
                CheckAnimator();

                weaponWeelButton[i].weaponName = "Shotgun";
                weaponWeelButton[i].icon = shotgunIcon;
                weaponWeelButton[i].iconTab.SetActive(true);
                weaponWeelButton[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void AssaultRifle()
    {
        for (int i = 0; i < weaponWeelButton.Length; i++)
        {
            if (weaponWeelButton[i].ID == 4)
            {
                weaponIsEquiped = true;

                player.assaultRifleFound = true;
                weaponWheel.unequipAll();
                assaultRifle.EquipAssaultRifle();
                CheckAnimator();

                weaponWeelButton[i].weaponName = "Assault Rifle";
                weaponWeelButton[i].icon = assaultRifleIcon;
                weaponWeelButton[i].iconTab.SetActive(true);
                weaponWeelButton[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void Minigun()
    {
        for (int i = 0; i < weaponWeelButton.Length; i++)
        {
            if (weaponWeelButton[i].ID == 5)
            {
                weaponIsEquiped = true;

                player.minigunFound = true;
                weaponWheel.unequipAll();
                minigun.EquipMinigun();
                CheckAnimator();

                weaponWeelButton[i].weaponName = "Minigun";
                weaponWeelButton[i].icon = minigunIcon;
                weaponWeelButton[i].iconTab.SetActive(true);
                weaponWeelButton[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    private void CheckAnimator()
    {
        if (weaponIsEquiped)
        {
            anim.SetBool("IsHoldingGun", true);
        }
        else
        {
            anim.SetBool("IsHoldingGun", false);
        }
    }
}
