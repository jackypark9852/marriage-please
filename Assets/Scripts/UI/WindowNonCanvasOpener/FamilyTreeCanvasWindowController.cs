using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class FamilyTreeCanvasWindowController : CanvasWindowController, IPointerDownHandler
{
    [Header("Animation settings")]
    [SerializeField] float openAnimTime = 0.5f;
    [SerializeField] Vector3 openPos = Vector3.zero;  // Hard-coded (should be calculated)
    [SerializeField] Vector3 openScale = Vector3.one;  // Hard-coded (should be calculated)
    [SerializeField] float closeAnimTime = 0.5f;
    Vector3 closePos;
    Vector3 closeScale;
    bool isClosePosScaleSet = false;

    Color openColor = new Color(1f, 1f, 1f, 1f);
    Color closeColor = new Color(1f, 1f, 1f, 0f);

    [SerializeField] SpriteRenderer familyTreeOpenerSR;

    [SerializeField] GameObject onMouseDownBlockerGO;

    [SerializeField] SerializableDictionary<PersonData, ProfileFrame> personDataToFrame;

    bool isInAnim = false;

    void Awake()
    {
        // Quick fix
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        gameObject.SetActive(false);

        familyTreeOpenerSR.material.color = closeColor;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isInAnim)
            {
                return;
            }
            if (isOpened)
            {
                CloseWindow();
            }
            else
            {
                OpenWindow();
            }
        }
    }

    public override void OpenWindow()
    {
        if (isOpened)
        {
            return;
        }
        OpenWindowAnim();
        onMouseDownBlockerGO.SetActive(true);
    }

    public override void CloseWindow()
    {
        if (!isOpened)
        {
            return;
        }
        CloseWindowAnim();
        onMouseDownBlockerGO.SetActive(false);
    }

    private void OpenWindowAnim()
    {
        isInAnim = true;
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
                isInAnim = false;
            });
        familyTreeOpenerSR.transform.DOScale(openScale, openAnimTime)
            .SetEase(Ease.OutBack);
        familyTreeOpenerSR.material.DOColor(openColor, openAnimTime).SetEase(Ease.Linear);

        if (openSound != null)
        {
            AudioSource.PlayClipAtPoint(openSound, Camera.main.transform.position);
        }
    }

    private void CloseWindowAnim()
    {
        familyTreeOpenerSR.gameObject.SetActive(true);
        isInAnim = true;
        windowGO.SetActive(false);
        familyTreeOpenerSR.gameObject.SetActive(true);
        familyTreeOpenerSR.transform.DOMove(closePos, closeAnimTime)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                isOpened = false;
                isInAnim = false;
            });
        familyTreeOpenerSR.transform.DOScale(closeScale, closeAnimTime)
            .SetEase(Ease.OutBack);
        familyTreeOpenerSR.material.DOColor(closeColor, closeAnimTime).SetEase(Ease.Linear);

        if (closeSound != null)
        {
            AudioSource.PlayClipAtPoint(closeSound, Camera.main.transform.position);
        }
    }

    public void SetProfileFrameActive(PersonData personData, bool isActive)
    {
        if (personDataToFrame.ContainsKey(personData))
        {
            personDataToFrame[personData].gameObject.SetActive(isActive);
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (data.button == PointerEventData.InputButton.Right)
        {
            CloseWindow();
        }
    }
}
