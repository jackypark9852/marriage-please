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
        EventManager.AddEvent("UnityStart", new UnityAction(()=>Debug.Log("UnityStart"))); //When you first enter into the game
        EventManager.AddEvent("ChangeState", new UnityAction(()=>Debug.Log("ChangeState")));//when you change the states'
        EventManager.AddEvent("StartTutorial", new UnityAction(()=>SceneManager.LoadScene(sceneNameList[1]))); //When you press the start button
        EventManager.AddEvent("RoundStart", new UnityAction(()=>SceneManager.LoadScene(sceneNameList[2]))); //When you press the start button
        EventManager.AddEvent("GameWin", new UnityAction(()=>SceneManager.LoadScene(sceneNameList[3]))); //When you press the start button
        EventManager.AddEvent("GameLose", new UnityAction(()=>SceneManager.LoadScene(sceneNameList[4]))); //When you press the start button

    }

    void OnDisable(){
        EventManager.RemoveAllEvent("UnityStart");
        EventManager.RemoveAllEvent("ChangeState");
        EventManager.RemoveAllEvent("StartTutorial");
        EventManager.RemoveAllEvent("RoundStart");
        EventManager.RemoveAllEvent("GameWin");
        EventManager.RemoveAllEvent("GameLose");


    }
    void Start()
    {
        ChangeState(GameState.Menu);
        EventManager.Invoke("UnityStart");
    }

    public static void ChangeStateIndex(int i){
        Instance.ChangeState((GameState)i);
        Debug.Log((GameState)i);
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
                break;
            case GameState.Tutorial:
                EventManager.Invoke("StartTutorial");
                break;
            case GameState.Round:
                EventManager.Invoke("RoundStart");
                Debug.Log(sceneNameList[2]);
                break;
            
            case GameState.Win:
                break;
            case GameState.Lose:
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
