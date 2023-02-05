using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameState state = GameState.Menu;
    public GameState State
    {
        get { return state; }
    }
    public List<string> sceneNameList = new List<string>();

    void Awake(){
        EventManager.Instance.Init();
        EventManager.AddEvent("StartMenu", new UnityAction(()=>Debug.Log("StartMenu"))); //When you first enter into the game
        EventManager.AddEvent("ChangeState", new UnityAction(()=>Debug.Log("ChangeState")));//when you change the states'
        EventManager.AddEvent("StartTutorial", new UnityAction(()=>Debug.Log("StartTutorial"))); //When you press the start button
        EventManager.AddEvent("RoundStart", new UnityAction(()=>Debug.Log("RoundStart"))); //When you press the start button
        EventManager.AddEvent("GameWon", new UnityAction(()=>Debug.Log("GameWon"))); //When you press the start button
        EventManager.AddEvent("GameLost", new UnityAction(()=>Debug.Log("GameLost"))); //When you press the start button
        ChangeState(GameState.Menu);
    }

    void OnDisable(){
        EventManager.RemoveAllEvent("StartMenu");
        EventManager.RemoveAllEvent("ChangeState");
        EventManager.RemoveAllEvent("StartTutorial");
        EventManager.RemoveAllEvent("RoundStart");
        EventManager.RemoveAllEvent("GameWon");
        EventManager.RemoveAllEvent("GameLost");


    }

     public static void ChangeScene(string str){
        switch(str){
            case "Menu":
                SceneManager.LoadScene(Instance.sceneNameList[0]);
                ChangeState(GameState.Menu);
                break;
            case "Tutorial":
                SceneManager.LoadScene(Instance.sceneNameList[1]);
                ChangeState(GameState.Tutorial);
                break;

            case "Round":
                SceneManager.LoadScene(Instance.sceneNameList[2]);
                ChangeState(GameState.Round);
                break;
            
            case "Win": 
                SceneManager.LoadScene(Instance.sceneNameList[3]);
                ChangeState(GameState.Win);
                break;
            
            case "Lose":   
                SceneManager.LoadScene(Instance.sceneNameList[4]);
                ChangeState(GameState.Lose);
                break;

        }
    }


    public static void ChangeState(GameState newState)
    {
        if (newState == Instance.state)
        {
            return;
        }   
        Debug.Log(newState);
        Instance.state = newState;
        Instance.OnStateChanged();
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
    Menu, //When you are in the menu
    Tutorial,
    Round, //When you are in the game
    Win,
    Lose,

}
