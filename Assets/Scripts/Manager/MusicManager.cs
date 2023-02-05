using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : Singleton<MusicManager>
{
    public List<AudioClip> musicClips = new List<AudioClip>();
    private AudioSource audioSource;
    private float volume = 1;
    protected override void Awake(){
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        EventManager.AddEvent("StartMenu", new UnityAction(()=>Instance.PlayMusic(0))); //When you first enter into the game
        EventManager.AddEvent("StartTutorial", new UnityAction(()=>Instance.PlayMusic(1))); //When you press the start button
        EventManager.AddEvent("RoundStart", new UnityAction(()=>Instance.PlayMusic(1))); //When you press the start button
        //EventManager.AddEvent("GameWon", new UnityAction(()=>SceneManager.LoadScene(sceneNameList[3]))); //When you press the start button
        //EventManager.AddEvent("GameLost", new UnityAction(()=>SceneManager.LoadScene(sceneNameList[4]))); //When you press the start button


    }
    public void SetVolume(float num)
    {
        volume = num;
        audioSource.volume = volume;
    }
    public float GetVolume(){
        return volume;
    }
    public void PlayMusic(int index)
    {
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
