using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : StaticInstance<SoundManager>
{
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip bombSFX;
    [SerializeField] private AudioClip attractorSFX;
    [SerializeField] private AudioClip playerDeathSFX;
    [SerializeField] private AudioClip asteroidHitSFX;


    [SerializeField] private AudioSource soundObject;


    public void Awake()
    {
        playBackgroundMusic(backgroundMusic, transform, 1f);
    }


    public void playAttractorSFX(Transform spawnTransform, float volume)
    {
        playSound(attractorSFX, spawnTransform, volume);
    }

    public void playBombSFX(Transform spawnTransform, float volume)
    {
        playSound(bombSFX, spawnTransform, volume);
    }

    public void playPlayerDeathSFX(Transform spawnTransform, float volume)
    {
        playSound(playerDeathSFX, spawnTransform, volume);
    }

    public void playAsteroidHitSFX(Transform spawnTransform, float volume)
    {
        playSound(asteroidHitSFX, spawnTransform, volume);
    }


    public void playSound(AudioClip soundClip, Transform spawnTransform, float volume)
    {
        //spawn in game object
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position,
            Quaternion.identity);

        //assign sound clip
        audioSource.clip = soundClip;

        //assign volume
        audioSource.volume = volume;

        //play the sound
        audioSource.Play();

        //get length of sound clip
        float clipLength = audioSource.clip.length;

        //destroy the object after done playing music
        Destroy(audioSource.gameObject, clipLength);
    }

    public void playBackgroundMusic(AudioClip soundClip, Transform spawnTransform,
        float volume)
    {
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position,
            Quaternion.identity);

        audioSource.clip = soundClip;

        audioSource.volume = volume;

        audioSource.loop = true;

        audioSource.Play();
    }
}