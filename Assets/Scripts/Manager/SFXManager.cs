using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SFXDictionary : SerializableDictionary<string, AudioClip>
{}

[RequireComponent(typeof(AudioSource))]
public class SFXManager : Singleton<SFXManager>
{
    public SFXDictionary sfxClips;
    private AudioSource audioSource;
    private float volume = 1;
    private static bool haveDone = false;
    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }


    public void SetVolume(float num)
    {
        volume = num;
        audioSource.volume = volume;
    }
    public void Mute()
    {
        audioSource.mute = true;
    }
    public void UnMute()
    {
        audioSource.mute = false;
    }
    public float GetVolume()
    {
        return volume;
    }
    public void PlayMusic(string SFXName)
    {
        if (sfxClips.ContainsKey(SFXName))
        {
            audioSource.clip = sfxClips[SFXName];
            audioSource.Play();
        }
    }
    
}
