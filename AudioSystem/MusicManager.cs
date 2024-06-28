using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource mainTheme;
    [SerializeField] private AudioSource bossTheme;

    private bool stopMusic;

    private void Start()
    {

    }

    public void StartBossTheme()
    {
        mainTheme.Stop();
        bossTheme.Play();
    }

    public void StopBossTheme()
    {
        stopMusic = true;
    }

    private void Update()
    {
        if (stopMusic == true)
        {
            bossTheme.volume -= 0.02f * Time.deltaTime;
        }
    }
}
