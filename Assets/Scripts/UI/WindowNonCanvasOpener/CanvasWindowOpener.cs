using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasWindowOpener : MonoBehaviour
{
    [SerializeField] CanvasWindowController canvasWindowController;

    void OnMouseDown()
    {
        canvasWindowController.gameObject.SetActive(true);
        canvasWindowController.OpenWindow();
    }
}
