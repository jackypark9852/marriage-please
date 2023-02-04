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
        EventManager.AddEvent("ChangeState", new UnityAction(()=>Debug.Log("ChangeState")));//when you change the states
        EventManager.AddEvent("GameStart", new UnityAction(()=>SceneManager.LoadScene(sceneNameList[1]))); //When you press the start button
        EventManager.AddEvent("CorrectChoice", new UnityAction(()=>Debug.Log("ChangeState"))); //When you make correct choices
        EventManager.AddEvent("WrongChoice", new UnityAction(()=>Debug.Log("ChangeState"))); //When you make wrong choices
        EventManager.AddEvent("GameOver", new UnityAction(()=>Debug.Log("ChangeState"))); //When you lose
    }

    void OnDisable(){
        EventManager.RemoveAllEvent("UnityStart");
        EventManager.RemoveAllEvent("ChangeState");
        EventManager.RemoveAllEvent("GameStart");
        EventManager.RemoveAllEvent("CorrectChoice");
        EventManager.RemoveAllEvent("WrongChoice");
        EventManager.RemoveAllEvent("GameOver");
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
            case GameState.InGame:
                EventManager.Invoke("GameStart");
                break;
            case GameState.PlayerTurn:

                break;
            case GameState.Correct:
                break;
            case GameState.Wrong:
                break;
            case GameState.GameOver:
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
    InGame, //When you are in the game
    PlayerTurn,  // Waiting for player to make decision
    Correct, //Turn when you make correct choices
    Wrong, //Turn when you make wrong choices
    GameOver, //When you lose

}
