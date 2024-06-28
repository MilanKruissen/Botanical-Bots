using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class weaponWheelButtonController : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private TextMeshProUGUI weaponText;

    private AudioSource audioSource;
    [SerializeField] private AudioClip hoverSound;
    
    public int ID;
    public Sprite icon;
    public string weaponName;
    public GameObject iconTab;

    private bool selected = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (selected)
        {
            
        }

        if (icon != null)
        {
            iconTab.GetComponent<Image>().sprite = icon;
        }
    }

    public void Selected()
    {
        selected = true;
    }

    public void DeSelected()
    {
        selected = false; 
    }

    public void HoverEnter()
    {
        if (icon)
        {
            selected = true;
            WeaponWheelController.weaponID = ID;
            audioSource.PlayOneShot(hoverSound);
            anim.SetBool("Hover", true);
        }
    }

    public void HoverExit()
    {
        anim.SetBool("Hover", false);
        weaponText.text = "";
    }
}
