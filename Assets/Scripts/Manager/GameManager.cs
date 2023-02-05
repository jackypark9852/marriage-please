using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameState state = GameState.NotStarted;
    public GameState State
    {
        get { return state; }
    }
    public List<string> sceneNameList = new List<string>();

    void Awake(){
        EventManager.Instance.Init();
        EventManager.AddEvent("StartMenu", new UnityAction(()=>Debug.Log("UnityStart"))); //When you first enter into the game
        EventManager.AddEvent("ChangeState", new UnityAction(()=>Debug.Log("ChangeState")));//when you change the states'
        EventManager.AddEvent("StartTutorial", new UnityAction(()=>SceneManager.LoadScene(sceneNameList[1]))); //When you press the start button
        EventManager.AddEvent("RoundStart", new UnityAction(()=>SceneManager.LoadScene(sceneNameList[2]))); //When you press the start button
        EventManager.AddEvent("GameWon", new UnityAction(()=>SceneManager.LoadScene(sceneNameList[3]))); //When you press the start button
        EventManager.AddEvent("GameLost", new UnityAction(()=>SceneManager.LoadScene(sceneNameList[4]))); //When you press the start button

    }

    void OnDisable(){
        EventManager.RemoveAllEvent("StartMenu");
        EventManager.RemoveAllEvent("ChangeState");
        EventManager.RemoveAllEvent("StartTutorial");
        EventManager.RemoveAllEvent("RoundStart");
        EventManager.RemoveAllEvent("GameWon");
        EventManager.RemoveAllEvent("GameLost");


    }
    void Start()
    {
        ChangeState(GameState.Menu);
        EventManager.Invoke("UnityStart");
    }

    public static void ChangeStateIndex(int i){
        SceneManager.LoadScene(Instance.sceneNameList[i]);
    }

     public static void ChangeScene(string str){
        switch(str){
            case "Menu":
                SceneManager.LoadScene(Instance.sceneNameList[0]);
                break;
            case "Tutorial":
                SceneManager.LoadScene(Instance.sceneNameList[1]);
                break;

            case "Round":
                SceneManager.LoadScene(Instance.sceneNameList[2]);
                break;
            
            case "Win": 
                SceneManager.LoadScene(Instance.sceneNameList[3]);
                break;
            
            case "Lose":   
                SceneManager.LoadScene(Instance.sceneNameList[4]);
                break;

        }
    }
    public static void ChangeStateString(string str){
        switch(str){
            case "Menu":
                Instance.ChangeState(GameState.Menu);
                break;
            case "Tutorial":
                Instance.ChangeState(GameState.Tutorial);
                break;
            case "Round":
                Instance.ChangeState(GameState.Round);
                break;
            case "Win":
                Instance.ChangeState(GameState.Win);
                break;
            case "Lose":
                Instance.ChangeState(GameState.Lose);
                break;
        }
    }

    public void DetectStage(){

    }

    public void ChangeState(GameState newState)
    {
        if (newState == state)
        {
            return;
        }

        state = newState;
        OnStateChanged();
        switch (newState)
        {
            case GameState.Menu:
                EventManager.Invoke("StartMenu");
                break;
            case GameState.Tutorial:
                EventManager.Invoke("StartTutorial");
                break;
            case GameState.Round:
                EventManager.Invoke("RoundStart");
                break;
            
            case GameState.Win:
                EventManager.Invoke("GameWon");
                break;
            case GameState.Lose:
                EventManager.Invoke("GameLost");
                break;


        }
    }

    private void OnStateChanged()
    {
        EventManager.Invoke("ChangeState");
        return;
    }

}

public enum GameState
{
    NotStarted, //When you start the unity and but not press the start button
    Menu, //When you are in the menu
    Tutorial,
    Round, //When you are in the game
    Win,
    Lose,

}
