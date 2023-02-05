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
        dialoguePosition.position = new Vector2(x/1920f * Camera.current.pixelRect.width, dialoguePosition.position.y);
    }
    public static void ChangePositionY(float y){
        dialoguePosition.position = new Vector2(dialoguePosition.position.x, y/1080f * Camera.current.pixelRect.height);
    }
    public static void ChangePosition(Vector2 position){
        dialoguePosition.position = position;
    }
}
