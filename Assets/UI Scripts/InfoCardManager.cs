using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InfoCardManager : MonoBehaviour
{
    public bool IsSafeChoice; 

    [Header("GameObjects: ")]
    public Transform portraitSprite;
    public TextMeshPro nameField;
    public GameObject heartObj;
    public GameObject AngeryObj;

    [Header("Events: ")]
    public UnityEvent<PersonData> infoCardClicked;
    public UnityEvent<GameObject, bool> animatingInfoCard; 
    [Header("Data")]
    [SerializeField] private PersonData personData;
    public PersonData PersonData
    {
        get { return personData; }
        set
        {
            personData = value;
            Debug.Log(personData);
            if (personData != null)
            {
                Debug.Log($"{personData.name} | {personData.Sprite}");
            }
            ChangePicture(personData.Sprite);
            ChangeName(personData.name);
        }
    }

    [Header("For Test Only: ")]
    public Sprite testSprite;

    [SerializeField] bool isCandidate = true;

    // public 

    // Start is called before the first frame update
    void Start()
    {
        // transform.DOMoveX(10, 2);

        // var sequence = DOTween.Sequence();

        // sequence.Append(transform.DOMoveX(2, 2));


    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKey(KeyCode.Mouse0))
        // {
        //     MoveTo(Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10), 2);
        // }

        // if (Input.GetKey(KeyCode.P))
        // {
        //     ChangePicture(testSprite);
        //     ChangeName("Andy Song");
        // }
    }

    void OnMouseEnter()
    {
        // if (!isCandidate)
        // {
        //     return;
        // }
        transform.DOShakeScale(1, 0.05f);

    }

    void OnMouseDown()
    {
        // if (!isCandidate)
        // {
        //     return;
        // }
        Debug.Log("clicked");
        infoCardClicked.Invoke(personData);
        animatingInfoCard.Invoke(gameObject, IsSafeChoice); 


    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        // Debug.Log("Mouse is over GameObject.");

    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        // Debug.Log("Mouse is no longer on GameObject.");
    }

    // custome functions
    public void MoveTo(Vector3 target, float duration)
    {
        transform.DOMove(target, duration);
    }

    public void ChangePicture(Sprite sp)
    {
        portraitSprite.GetComponent<SpriteRenderer>().sprite = sp;
    }

    public void ChangeName(String name)
    {
        nameField.text = name;
    }

    public void PlayHeartAnim()
    {
        Animator anim = heartObj.GetComponent<Animator>();
        anim.Play("HeartEffect", 0, 0.25f);
    }

    public void PlayAngryEffect()
    {
        Animator anim = AngeryObj.GetComponent<Animator>();
        anim.Play("AngryEffect", 0, 0.25f);
    }
}
