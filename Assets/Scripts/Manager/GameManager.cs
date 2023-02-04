using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public GameState state = GameState.NotStarted;
    public GameState State
    {
        get { return state; }
    }
    void OnEnable() {
        EventManager.AddEvent("ChangeState", null);
        EventManager.AddEvent("GameStart", null);
        EventManager.AddEvent("CorrectChoice", null);
        EventManager.AddEvent("WrongChoice", null);
        EventManager.AddEvent("GameOver", null);
    }
    void Start()
    {
        ChangeState(GameState.Starting);
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
            case GameState.Starting:
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
        //StateChanged.Invoke();
        return;
    }

}

public enum GameState
{
    NotStarted,
    Starting,
    PlayerTurn,  // Player input happens here
    Correct, //Turn when you make correct choices
    Wrong, //Turn when you make wrong choices
    GameOver,
}
