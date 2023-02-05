using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleAnimation : MonoBehaviour
{
    public void Animate(GameObject obj, bool isSafe) {
        Debug.Log(obj); 
        if(isSafe) {
            Debug.Log("safe animation"); 
        } else {
            Debug.Log("unsafe animation"); 
        }

    }
}
