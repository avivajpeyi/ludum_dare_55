using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioSource soundObject;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            playBackgroundMusic(backgroundMusic, transform, 1f);
        }
    }

    public void playSound(AudioClip soundClip, Transform spawnTransform, float volume)
    {
        //spawn in game object
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);

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

    public void playBackgroundMusic(AudioClip soundClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = soundClip;

        audioSource.volume = volume;

        audioSource.loop = true;

        audioSource.Play();
    }
}
