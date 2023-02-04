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

    public static float CORRECT_TIME_INCREMENT = 5f;
    public static float INCORRECT_TIME_INCREMENT = -10f;
}
