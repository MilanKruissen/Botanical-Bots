using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    private NewMusicManager music;
    private AbilityState abiltyState;
    private UltState ultState;

    public float maxHealth;
    public float currentHealth;
   
    [SerializeField] private GameObject floatingDamageText;

    [SerializeField] private GameObject bossHealthbar;
 
    private float cooldownTimer;
    [SerializeField] private float abilityCooldown;
    public bool abilityReady = false;

    private float ultCooldownTimer;
    [SerializeField] private float ultCooldown;
    public bool ultReady = false;

    [Header("Dropables")]
    private bool medkitDropped = false;
    [SerializeField] private GameObject medkit;

    private bool hasBeenDroppedOnce = false;
    private bool ammoBoxDropped = false;
    [SerializeField] private GameObject ammoBox;

    [SerializeField] private GameObject keycard;

    public bool isInTrap;
    public float walkingSpeed = 3;

    private void Start()
    {
        bossHealthbar.SetActive(true);

        currentHealth = maxHealth;

        abiltyState = FindObjectOfType<AbilityState>();
        ultState = FindObjectOfType<UltState>();

        music = FindObjectOfType<NewMusicManager>();
        music.StartBossTheme();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        ultCooldownTimer += Time.deltaTime;

        if (cooldownTimer >= abilityCooldown)
        {
            abilityReady = true;
        }

        if (ultCooldownTimer >= ultCooldown)
        {
            ultReady = true;
        }
    }

    public void abilityUsed()
    {
        cooldownTimer = 0;
        abilityReady = false;
        abiltyState.chargeTime = 0;
    }

    public void UltUsed()
    {
        ultCooldownTimer = 0;
        ultReady = false;
        ultState.ultTimer = 0;
    }

    public IEnumerator DamageOverTime(int damage)
    {
        while (isInTrap)
        {
            DamageTaken(damage);
            yield return new WaitForSeconds(1f);
        }
    }

    public void DamageTaken(float damage)
    {
        currentHealth = currentHealth - damage;

        DamageIndicator indicator = Instantiate(floatingDamageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(damage);

        // Drops medkit when at different health stages
        if (currentHealth <= (maxHealth / 100 * 50) && medkitDropped == false)
        {
            DropMedkit();
        }

        if (currentHealth <= (maxHealth / 100 * 75) && ammoBoxDropped == false)
        {
            DropAmmoBox();
        }
        else if (currentHealth <= (maxHealth / 100 * 25))
        {
            if (hasBeenDroppedOnce == false)
            {
                ammoBoxDropped = false;
                hasBeenDroppedOnce = true;
            }
        }

        if (currentHealth <= 0)
        {
            FindObjectOfType<PlayerManager>().AddCoins(300);

            DropUponDeath();
            Dead();
        }
    }

    private void DropMedkit()
    {
        Instantiate(medkit, transform.position, transform.rotation);
        medkitDropped = true;
    }

    private void DropAmmoBox()
    {
        Instantiate(ammoBox, transform.position, transform.rotation);
        ammoBoxDropped = true;
    }

    private void DropUponDeath()
    {
        Instantiate(keycard, transform.position, transform.rotation);
    }

    [SerializeField] private Door door;

    private void Dead()
    {
        door.canBeOpened = true;    
        music.StopBossTheme();
        music.StartMainTheme();
        Destroy(gameObject);
    }
}
