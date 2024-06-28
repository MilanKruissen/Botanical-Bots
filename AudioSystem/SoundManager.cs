using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager 
{
    public enum Sounds
    {
        swordSlash,
        bulletImpact,
        bossPunch,
        bossFootstepOne,
        bossFootstepTwo,
        missileLaunch,
        explosion,
        playerFootstepOne,
        playerFootstepTwo,
        playerDash,
        shotgunShot,
        shotgunReload,
        grenadeThrow,
        walkingSpider,
        pistolBulet,
        assaultRifle,
        minigun,
        hitSound
    }

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    public static void PlaySound(Sounds sound, float volume)
    {
        if (oneShotGameObject == null)
        {
            oneShotGameObject = new GameObject("Sound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        }

        oneShotAudioSource.PlayOneShot(GetAudioClip(sound), volume);
    }

    private static AudioClip GetAudioClip(Sounds sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }

        Debug.LogError("Sound" + sound + "not found!");
        return null;
    }
}
