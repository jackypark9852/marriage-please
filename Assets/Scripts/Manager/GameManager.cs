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
    public static bool haveDone = false;
    protected override void Awake(){
        base.Awake();
    
       
    }

    void Start(){
        if(haveDone){
            return;
        }
        EventManager.AddEvent("StartMenu", new UnityAction(()=>SceneManager.LoadScene(Instance.sceneNameList[0]))); //When you first enter into the game
        EventManager.AddEvent("StartTutorial", new UnityAction(()=>SceneManager.LoadScene(Instance.sceneNameList[1]))); //When you press the start button
        EventManager.AddEvent("RoundStart", new UnityAction(()=>SceneManager.LoadScene(Instance.sceneNameList[2]))); //When you press the start button
        EventManager.AddEvent("GameWon", new UnityAction(()=>SceneManager.LoadScene(Instance.sceneNameList[3]))); //When you press the start button
        EventManager.AddEvent("GameLost", new UnityAction(()=>SceneManager.LoadScene(Instance.sceneNameList[4]))); //When you press the start button
        EventManager.AddEvent("ChangeState", new UnityAction(()=>Debug.Log("ChangeState")));//when you change the states'
        haveDone = true;
    }

     public static void ChangeState(string str){
        switch(str){
            case "Menu":
    
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

    private static void OnStateChanged()
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
