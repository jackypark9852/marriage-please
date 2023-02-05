using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "StageData", order = 0)]
public class StageData : ScriptableObject
{
    public FamilyData familyData;
    public Sprite familyTreeSprite;
    public float startTimeLength;
    public float maxTimeLength;
    public int gameRoundLength;
    public float unsafeStartingProbability;
    public float unsafeProbabilityIncrement;
    public float correctTimeIncrement;
    public float incorrectTimeIncrement;
}
