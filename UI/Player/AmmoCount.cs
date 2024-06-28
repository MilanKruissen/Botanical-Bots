using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoCount : MonoBehaviour
{
    //private Pistol pistol;

    public float currentAmmoCount;
    public float totalMagSize;

    [SerializeField] private TextMeshProUGUI currentAmmoCountUI;
    [SerializeField] private TextMeshProUGUI totalMagSizeUI;
    public Image currentGunImage;

    [SerializeField] private Image currentImage;

    private void Update()
    {
        currentAmmoCountUI.text = currentAmmoCount.ToString();
        totalMagSizeUI.text = totalMagSize.ToString();
        //currentImage = currentGunImage;
    }
}
