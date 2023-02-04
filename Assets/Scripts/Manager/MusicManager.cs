using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : Singleton<MusicManager>
{
    public List<AudioClip> musicClips = new List<AudioClip>();
    private AudioSource audioSource;
    public float volume = 0;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Awake(){
        EventManager.AddEvent("UnityStart", new UnityAction(()=>PlayMusic(0))); //When you first enter into the game
        EventManager.AddEvent("GameStart", new UnityAction(()=>PlayMusic(2))); //When you press the start button
        EventManager.AddEvent("CorrectChoice", new UnityAction(()=>PlayMusic(3))); //When you make correct choices
        EventManager.AddEvent("WrongChoice", new UnityAction(()=>PlayMusic(4))); //When you make wrong choices
        EventManager.AddEvent("GameOver", new UnityAction(()=>PlayMusic(5))); //When you lose

    }
    public void SetVolume(float num)
    {
        volume = num;
        audioSource.volume = volume;
    }

    public void PlayMusic(int index)
    {
        StopMusic();
        if(index >= musicClips.Count)
            return;
        audioSource.clip = musicClips[index];
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
