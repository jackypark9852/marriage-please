using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWindowController : MonoBehaviour
{
    [SerializeField] protected GameObject windowGO;
    [SerializeField] protected Button closeButton;
    [SerializeField] protected CanvasWindowOpener canvasWindowOpener;

    [SerializeField] protected AudioClip openSound;
    [SerializeField] protected AudioClip closeSound;

    protected bool isOpened = false;

    public virtual void OpenWindow()
    {
        windowGO.SetActive(true);
        isOpened = true;
        /*
        if (openSound != null)
        {
            AudioSource.PlayClipAtPoint(openSound, Camera.main.transform.position);
        }
        */
    }

    public virtual void CloseWindow()
    {
        windowGO.SetActive(false);
        isOpened = false;
        /*
        if (closeSound != null)
        {
            AudioSource.PlayClipAtPoint(closeSound, Camera.main.transform.position);
        }
        */
    }
}
