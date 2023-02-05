using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSceneController : MonoBehaviour
{
    public Animator animator;
    private static string sceneName;
    void Awake(){
        animator = GetComponent<Animator>();
    }
    public void FadeToScene(string name){
        sceneName = name;
        animator.SetTrigger("FadeOut");
    }

    public void OnFaceComplete(){
        Debug.Log("Fade Complete"+sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
