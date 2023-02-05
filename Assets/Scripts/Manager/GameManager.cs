using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameState state = GameState.Menu;
    public GameState State
    {
        get { return state; }
    }
    public static int stageNum;
    public List<string> sceneNameList = new List<string>();
    private static bool haveDone = false;
    protected override void Awake(){
        base.Awake();
        //fadeImage = GameObject.Find("FadeImage").GetComponent<FadeSceneController>();
        
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
        EventManager.AddEvent("Interm1", new UnityAction(()=>SceneManager.LoadScene(Instance.sceneNameList[5])));
        EventManager.AddEvent("Interm2", new UnityAction(()=>SceneManager.LoadScene(Instance.sceneNameList[6])));
        EventManager.AddEvent("WinInterim", new UnityAction(()=>SceneManager.LoadScene(Instance.sceneNameList[7])));
        EventManager.AddEvent("LoseInterim", new UnityAction(()=>SceneManager.LoadScene(Instance.sceneNameList[8])));
        haveDone = true;
        // ChangeState(GameState.Menu);  // TODO: uncomment in final build
    }

     public static void ChangeState(string str){
        switch(str){
            case "Menu":
    
                ChangeState(GameState.Menu);
                break;
            case "Tutorial":
                ChangeState(GameState.Tutorial);
                break;


            case "Round1":
                stageNum = 0;
                ChangeState(GameState.Round1);
                break;
            
            case "Interim1":
                ChangeState(GameState.Interim1);
                break;
            
            case "Round2":
                stageNum = 1;
                ChangeState(GameState.Round2);
                break;
            
            case "Interim2":
                ChangeState(GameState.Interim2);
                break;  
            
            case "Round3":
                stageNum = 2;
                ChangeState(GameState.Round3);
                break;

            case "Win": 
                ChangeState(GameState.Win);
                break;
            
            case "Lose":   
                ChangeState(GameState.Lose);
                break;
            case "WinInterim":
                ChangeState(GameState.WinInterim);
                break;
            case "LoseInterim":
                ChangeState(GameState.LoseInterim);
                break;

        }
    }


    public static void ChangeState(GameState newState)
    {
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
            case GameState.Round1:
                EventManager.Invoke("RoundStart");
                break;
            case GameState.Interim1:
                EventManager.Invoke("Interm1");
                break;
            case GameState.Round2:
                EventManager.Invoke("RoundStart");
                break;

            case GameState.Interim2:
                EventManager.Invoke("Interm2");
                break;

            case GameState.Round3:
                EventManager.Invoke("RoundStart");
                break;
            case GameState.WinInterim:
                EventManager.Invoke("WinInterim");
                break;
            case GameState.LoseInterim:
                EventManager.Invoke("LoseInterim");
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
    Round1, //When you are in the game
    Interim1,
    Round2,
    Interim2,
    Round3,
    Win,
    Lose,
    WinInterim,
    LoseInterim

}
