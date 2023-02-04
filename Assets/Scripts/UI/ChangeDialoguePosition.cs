using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDialoguePosition : MonoBehaviour
{
    public static Transform dialoguePosition;
    // Start is called before the first frame update
    void Start()
    {
        dialoguePosition = GetComponent<Transform>();
    }
    public static void ChangePositionX(float x){
        dialoguePosition.position = new Vector2(x, dialoguePosition.position.y);
    }
    public static void ChangePositionY(float y){
        dialoguePosition.position = new Vector2(y, dialoguePosition.position.y);
    }
    public static void ChangePosition(Vector2 position){
        dialoguePosition.position = position;
    }
}
