using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponWheelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponText;

    public Animator anim;
    public static int weaponID;

    private bool weaponWheelSelected = false;

    private Pistol pistol;
    private Katana katana;
    private Shotgun shotgun;
    private AR assaultRifle;
    private Minigun minigun;
    private AmmoBoxPickup[] ammoBox;

    public int oldweapon;
    bool wasOpen;

    private void Start()
    {
        unequipAll();

        pistol = FindObjectOfType<Pistol>();
        katana = FindObjectOfType<Katana>();
        shotgun = FindObjectOfType<Shotgun>();
        assaultRifle = FindObjectOfType<AR>();
        minigun = FindObjectOfType<Minigun>();
        ammoBox = FindObjectsOfType<AmmoBoxPickup>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            wasOpen = true;
            weaponWheelSelected = true;
            PlayerManager.canGunsFire = false;
            if(Time.timeScale > 0.5f)
            {
                Time.timeScale -= Time.deltaTime * 2;
            } else Time.timeScale = 0.5f;
        }
        else
        {
            if (wasOpen)
            {
                weaponWheelSelected = false;
                PlayerManager.canGunsFire = true;
                wasOpen = false;
            }
            if (Time.timeScale < 1f)
            {
                Time.timeScale += Time.deltaTime;
            }
            else Time.timeScale = 1f;
        }

        if (weaponWheelSelected)
        {
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            anim.SetBool("OpenWeaponWheel", false);
        }

        if(oldweapon != weaponID)
        {
            ammoBox = null;
            ammoBox = FindObjectsOfType<AmmoBoxPickup>();
            oldweapon = weaponID;
            switch (weaponID)
            {
                case 0: // Nothing is selected
                    break;
                case 1: // Pistol
                    Debug.Log("Pistol is selected");
                    unequipAll();
                    pistol.EquipPistol();
                    weaponText.text = "Pistol";
                    break;
                case 2:
                    Debug.Log("Katana is selected");
                    unequipAll();
                    katana.EquipKatana();
                    weaponText.text = "Katana";
                    break;
                case 3:
                    Debug.Log("Shotgun is selected");
                    unequipAll();
                    shotgun.EquipShotgun();
                    weaponText.text = "Shotgun";
                    break;
                case 4:
                    Debug.Log("Assault rifle is selected");
                    unequipAll();
                    assaultRifle.EquipAssaultRifle();
                    weaponText.text = "Assault Rifle";
                    break;
                case 5:
                    Debug.Log("Minigun is selected");
                    unequipAll();
                    minigun.EquipMinigun();
                    weaponText.text = "Minigun";
                    break;
                case 6:

                    break;
                case 7:

                    break;
                case 8:

                    break;
            }
        }
    }

    public void unequipAll()
    {
        try
        {
            pistol.DeEquipPistol();
            katana.DeEquipKatana();
            shotgun.DeEquipShotgun();
            assaultRifle.DeEquipRifle();
            minigun.DeEquipMinigun();
            foreach (AmmoBoxPickup ammobox in ammoBox)
            {
                ammobox.resetBullets();
            }
        }
        catch (System.Exception)
        {
            //Debug.Log(e);
        } 
    }
}
