using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DialogueData", menuName = "marriage-please/DialogueData", order = 0)]
public class DialogueData : ScriptableObject
{
    [Multiline]
   public string text;
   public Vector2 position;
   public UnityEvent onDialogueEnd;
}