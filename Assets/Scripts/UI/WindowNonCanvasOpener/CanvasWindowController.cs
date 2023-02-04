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

    [SerializeField] AudioClip openSound;
    [SerializeField] AudioClip closeSound;

    public virtual void OpenWindow()
    {
        windowGO.SetActive(true);
        if (openSound != null)
        {
            AudioSource.PlayClipAtPoint(openSound, Camera.main.transform.position);
        }
    }

    public virtual void CloseWindow()
    {
        windowGO.SetActive(false);
        if (closeSound != null)
        {
            AudioSource.PlayClipAtPoint(closeSound, Camera.main.transform.position);
        }
    }
}
