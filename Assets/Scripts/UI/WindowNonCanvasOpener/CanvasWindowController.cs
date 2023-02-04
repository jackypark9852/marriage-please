using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWindowController : MonoBehaviour
{
    [SerializeField] GameObject windowGO;
    [SerializeField] Button closeButton;
    [SerializeField] CanvasWindowOpener canvasWindowOpener;

    public virtual void OpenWindow()
    {
        windowGO.SetActive(true);
    }

    public virtual void CloseWindow()
    {
        windowGO.SetActive(false);
    }
}
