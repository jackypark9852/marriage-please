using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowController : MonoBehaviour
{
    [SerializeField] GameObject window;
    [SerializeField] Button openButton;
    [SerializeField] Button closeButton;
    
    public void OpenWindow()
    {
        window.SetActive(true);
    }

    public void CloseWindow()
    {
        window.SetActive(false);
    }
}
