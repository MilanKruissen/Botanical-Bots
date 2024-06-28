using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    private PlayerManager playerManager;
    private PlayerBehaviour player;
    private PlayerInventory playerInv;

    [SerializeField] private GameObject ShopUI;

    public TMP_Text coinsUI;

    // All Items
    public ShopItemSO[] shopItemsSO;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsGO;
    public Button[] PurchaseButtons;

    public bool shopIsActive = false;

    private void OnEnable()
    {
        Debug.Log("shopIsActive");

        shopIsActive = true;
    }

    private void OnDisable()
    {
        shopIsActive = false;
        CheckPurchaseable();
    }

    private void Start()
    {
        shopIsActive = false;  

        playerManager = FindObjectOfType<PlayerManager>();
        player = FindObjectOfType<PlayerBehaviour>();
        playerInv = FindObjectOfType<PlayerInventory>();

        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }

        LoadPanels();
        CheckPurchaseable();
    }

    private void Update()
    {
        FindObjectOfType<CurrencyUI>().UpdateText(playerManager.playerCoins);

        PlayerManager.canGunsFire = false;
        player.canMove = false;
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        if (shopIsActive)
        {
            CheckPurchaseable();
        }
    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (playerManager.playerCoins >= shopItemsSO[i].baseCost)
            {
                PurchaseButtons[i].interactable = true;
            }
            else
            {
                PurchaseButtons[i].interactable = false;
            }
        }
    }

    public void PurchaseItem(int buttonNumber)
    {
        if (playerManager.playerCoins >= shopItemsSO[buttonNumber].baseCost)
        {
            playerManager.playerCoins = playerManager.playerCoins - shopItemsSO[buttonNumber].baseCost;

            // Buy Shotgun
            if (shopItemsSO[buttonNumber].title == "Shotgun")
            {
                Debug.Log("Shotgun!!!");
                playerInv.Shotgun();
            }
            
            // Buy Submachine Gun
            if (shopItemsSO[buttonNumber].title == "Assault Rifle")
            {
                Debug.Log("AR!!!");

                playerInv.AssaultRifle();
            }

            // Buy Minigun
            if (shopItemsSO[buttonNumber].title == "Minigun")
            {
                Debug.Log("Mingun!!!");

                playerInv.Minigun();
            }
            
            CheckPurchaseable();  
        }
    }

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemsSO[i].title;
            shopPanels[i].icon = shopItemsSO[i].icon;
            shopPanels[i].descriptionTxt.text = shopItemsSO[i].description;
            shopPanels[i].costTxt.text = "Coins: " + shopItemsSO[i].baseCost.ToString();
        }
    }
}
