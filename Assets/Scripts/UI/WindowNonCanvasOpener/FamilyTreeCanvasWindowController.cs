using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class FamilyTreeCanvasWindowController : CanvasWindowController
{
    [Header("Animation settings")]
    [SerializeField] float openAnimTime = 0.5f;
    [SerializeField] Vector3 openPos = Vector3.zero;  // Hard-coded (should be calculated)
    [SerializeField] Vector3 openScale = Vector3.one;  // Hard-coded (should be calculated)
    [SerializeField] float closeAnimTime = 0.5f;
    Vector3 closePos;
    Vector3 closeScale;
    bool isClosePosScaleSet = false;

    [SerializeField] SpriteRenderer familyTreeOpenerSR;

    public override void OpenWindow()
    {
        if (isOpened)
        {
            return;
        }
        OpenWindowAnim();
    }

    public override void CloseWindow()
    {
        if (!isOpened)
        {
            return;
        }
        CloseWindowAnim();
    }

    private void OpenWindowAnim()
    {
        if (!isClosePosScaleSet)
        {
            closePos = familyTreeOpenerSR.transform.position;
            closeScale = familyTreeOpenerSR.transform.localScale;
            isClosePosScaleSet = true;
        }

        familyTreeOpenerSR.transform.DOMove(openPos, openAnimTime)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                familyTreeOpenerSR.gameObject.SetActive(false);
                base.OpenWindow();
            });
        familyTreeOpenerSR.transform.DOScale(openScale, openAnimTime)
            .SetEase(Ease.OutBack);

        if (openSound != null)
        {
            AudioSource.PlayClipAtPoint(openSound, Camera.main.transform.position);
        }
    }

    private void CloseWindowAnim()
    {
        windowGO.SetActive(false);
        familyTreeOpenerSR.gameObject.SetActive(true);
        familyTreeOpenerSR.transform.DOMove(closePos, closeAnimTime)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                isOpened = false;
            });
        familyTreeOpenerSR.transform.DOScale(closeScale, closeAnimTime)
            .SetEase(Ease.OutBack);

        if (closeSound != null)
        {
            AudioSource.PlayClipAtPoint(closeSound, Camera.main.transform.position);
        }
    }
}
