using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] private float creditsSpeed;

    private void Update()
    {
        transform.Translate(0, -creditsSpeed * Time.deltaTime, 0);
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
