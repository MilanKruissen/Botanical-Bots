using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoBoxPickup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoInBox;
    [SerializeField] private TextMeshProUGUI maxAmmoInBox;

    Pistol pistol;
    Katana katana;
    Shotgun shotgun;
    AR ar;
    Minigun minigun;

    int oldweapon;

    int maxBulletsInBox = 24;
    int bullets = 24;
    int extra;

    private AudioSource audioSource;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        bullets = maxBulletsInBox;

        audioSource = GetComponent<AudioSource>();
        pistol = FindObjectOfType<Pistol>();
        katana = FindObjectOfType<Katana>();
        shotgun = FindObjectOfType<Shotgun>();
        ar = FindObjectOfType<AR>();
        minigun = FindObjectOfType<Minigun>();

        updateBullets();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        ammoInBox.text = bullets.ToString();
        maxAmmoInBox.text = maxBulletsInBox.ToString();

        if (bullets == 0)
        {
            model.SetActive(false);
            Destroy(UI);
            Destroy(gameObject, 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(pistol.pistolEquiped) //Pistol
            {
                pistol.AddAmmo(bullets);

                if (pistol.maxPistolAmmo < pistol.reservePistolAmmo)
                {
                    extra = pistol.reservePistolAmmo - pistol.maxPistolAmmo;
                    pistol.reservePistolAmmo = pistol.maxPistolAmmo;
                }
                else extra = 0;
                bullets = extra;

                audioSource.Play();
            }

            if (shotgun.shotgunEquiped)//Shotgun
            {
                shotgun.AddAmmo(bullets);
                if (shotgun.maxShotgunAmmo < shotgun.reserveShotgunAmmo)
                {
                    extra = shotgun.reserveShotgunAmmo - shotgun.maxShotgunAmmo;
                    shotgun.reserveShotgunAmmo = shotgun.maxShotgunAmmo;
                }
                else extra = 0;
                bullets = extra;
                audioSource.Play();
            }

            if (ar.AREquiped)//AR
            {
                ar.AddAmmo(bullets);
                if (ar.maxARAmmo < ar.reserveARAmmo)
                {
                    extra = ar.reserveARAmmo - ar.maxARAmmo;
                    ar.reserveARAmmo = ar.maxARAmmo;
                }
                else extra = 0;
                bullets = extra;
                audioSource.Play();
            }

            if (minigun.minigunEquiped)//Minigun
            {
                minigun.AddAmmo(bullets);
                if (minigun.maxMinigunAmmo < minigun.reserveMinigunAmmo)
                {
                    extra = minigun.reserveMinigunAmmo - minigun.maxMinigunAmmo;
                    minigun.reserveMinigunAmmo = minigun.maxMinigunAmmo;
                }
                else extra = 0;
                bullets = extra;
                audioSource.Play();
            }

            Debug.Log("Adding ammo");
        }
    }




    public void resetBullets()
    {
        switch (oldweapon)
        {
            case 0: // Nothing is selected
                break;
            case 1: // Pistol
                //should already be base
                break;
            case 2: //Katana
                //should already be base
                break;
            case 3: //Shotgun
                maxBulletsInBox *= 4;
                bullets *= 4;
                break;
            case 4: // AR
                maxBulletsInBox /= 2;
                bullets /= 2;
                break;
            case 5: //Minigun
                maxBulletsInBox /= 10;
                bullets /= 10;
                break;
            case 6:

                break;
            case 7:

                break;
            case 8:

                break;
        }        
        updateBullets();
    }

    void updateBullets()
    {
        int weaponId = WeaponWheelController.weaponID;
        switch (weaponId)
        {
            case 0: // Nothing is selected
                break;
            case 1: // Pistol
                //should already be base
                oldweapon = weaponId;
                break;
            case 2: //Katana
                //should already be base
                oldweapon = weaponId;
                break;
            case 3: //Shotgun
                maxBulletsInBox /= 4;
                bullets /= 4;
                oldweapon = weaponId;
                break;
            case 4: // AR
                maxBulletsInBox *= 2;
                bullets *= 2;
                oldweapon = weaponId;
                break;
            case 5: //Minigun
                maxBulletsInBox *= 10;
                bullets *= 10;
                oldweapon = weaponId;
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
