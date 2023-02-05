using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    public static int SAFE_DISTANCE = 3;

    // Time lengths by stage
    public static SerializableDictionary<int, float> START_TIME_LENGTHS = new SerializableDictionary<int, float>
    {
        { 0, 30f },
        { 1, 30f },
        { 2, 30f },
    };
    public static SerializableDictionary<int, float> MAX_TIME_LENGTHS = new SerializableDictionary<int, float>
    {
        { 0, 30f },
        { 1, 30f },
        { 2, 30f },
    };

    public static float CORRECT_TIME_INCREMENT = 20f;
    public static float INCORRECT_TIME_INCREMENT = -30f;

    public static SerializableDictionary<int, float> GAME_ROUND_LENGTHS = new SerializableDictionary<int, float>
    {
        { 0, 5 },
        { 1, 10 },
        { 2, 15 },
    };
    
    public static SerializableDictionary<int, float> UNSAFE_STARTING_PROBABILITY = new SerializableDictionary<int, float>
    {
        { 0, -0.5f },
        { 1, 0.0f },
        { 2, 0.2f },
    };
    public static SerializableDictionary<int, float> UNSAFE_PROBABILITY_INCREMENTS = new SerializableDictionary<int, float>
    {
        { 0, 0.5f },
        { 1, 0.15f },
        { 2, 0.1f },
    };
}
