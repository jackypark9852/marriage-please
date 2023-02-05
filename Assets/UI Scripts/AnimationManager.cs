
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AnimationManager : MonoBehaviour
{

    [Header("InfoCards: ")]
    public GameObject cardLeft;    
    public GameObject cardRight;
    public GameObject cardCandidate;


    [Header("The Random People Walk in:")]
    public GameObject personPlaceholder;
    public Sprite[] peopleSprites;



    [Header("Animation Hooks:")]
    public Vector3 leftOutPos;
    public Vector3 leftScenePos;
    
    public Vector3 rightOutPos;
    public Vector3 rightScenePos;
    public Vector3 candidateOutPos;
    public Vector3 candidateScenePos;

    public float cardEnterDuration;

    public Vector3 personOutPos;
    public Vector3 personScenePos;
    public Vector3 personEndPos;
    public float personEnterDuartion; 
    public float personLeaveDuartion; 

    [Header("After Selection Hooks:")]
    public Vector3 resultPlaceFront;
    public Vector3 resultPlaceBack;
    public float randomRoateValue;
    public float cardLeaveDuration;

    [Header("After Show Result:")]
    public float waitTimeToDiscard;


    // Internal Vars
    private bool playerSelected;


    // Start is called before the first frame update
    void Start()
    {
        StartRoundSequence();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Q) && !playerSelected) {
            playerSelected = true;
            takeChoice(cardLeft, false);
            // cardCandidate.GetComponent<InfoCardManager>().PlayAngryEffect();
        }
    }

    // SEQUENCES

    public void StartRoundSequence() {
        // the random person walk from left to right to the scene

        personPlaceholder.transform.position = personOutPos;
        int index = Random.Range (0, peopleSprites.Length);
        personPlaceholder.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = peopleSprites[index];

        var seq = DOTween.Sequence();
        seq.Append(personPlaceholder.transform.DOMove(personScenePos, personEnterDuartion));
        seq.Join(personPlaceholder.transform.GetChild(0).DOPunchPosition(new Vector3(0, 1, 0), personEnterDuartion));


        // make the cards fly into the scence

        cardLeft.transform.position = leftOutPos;
        cardLeft.transform.rotation = Quaternion.identity;
        cardRight.transform.position = rightOutPos;
        cardRight.transform.rotation = Quaternion.identity;


        seq.Append(cardLeft.transform.DOMove(leftScenePos, cardEnterDuration));
        seq.Join(cardRight.transform.DOMove(rightScenePos, cardEnterDuration));

        Vector3 rot = new Vector3(0, 0, Random.Range(0, randomRoateValue));
        seq.Join(cardLeft.transform.DORotate(rot, cardLeaveDuration));
        seq.Join(cardRight.transform.DORotate(rot, cardLeaveDuration));
        // seq.Join(cardLeft.transform.DOShakePosition(cardEnterDuration));

        // make the candidate card fly from bottom to inside the scene
        // cardLeft.transform.DOMove(leftScenePos, cardEnterDuration);

        cardCandidate.transform.position = candidateOutPos;

        seq.Append(cardCandidate.transform.DOMove(candidateScenePos, cardEnterDuration));

    }

    public void takeChoice(GameObject sel, bool isCorrect) {

        Debug.Log("taked choice");

        bool isLeft = sel == cardLeft;

        GameObject selected;
        GameObject other;
        // Vector3 wrongOutPos;
        // Vector3 correctOutPos;
        if(isLeft) {
            other = cardRight;
            selected = cardLeft;
            // wrongOutPos = rightOutPos;
            // correctOutPos = leftOutPos;
        } else {
            other = cardLeft;
            selected = cardRight;
            // wrongOutPos = leftOutPos;
            // correctOutPos = rightOutPos;
        }

        // make the wrong card fly away from the scene
        var seq = DOTween.Sequence();
        seq.Append(other.transform.DOMove(rightOutPos, cardLeaveDuration));

        // make the selcted cards to the designed postion
        seq.Join(selected.transform.DOMove(resultPlaceBack, cardLeaveDuration));
        seq.Join(cardCandidate.transform.DOMove(resultPlaceFront, cardLeaveDuration));

        // make some random rotation
        Vector3 rot = new Vector3(0, 0, Random.Range(0, randomRoateValue));
        seq.Join(selected.transform.DORotate(rot, cardLeaveDuration));
        seq.Join(cardCandidate.transform.DORotate(-rot, cardLeaveDuration));

        // add callback at the end;
        seq.AppendCallback(() => {
            if (isCorrect) {
                cardCandidate.GetComponent<InfoCardManager>().PlayHeartAnim();
                selected.GetComponent<InfoCardManager>().PlayHeartAnim();
            }
            else {
                cardCandidate.GetComponent<InfoCardManager>().PlayAngryEffect();
                selected.GetComponent<InfoCardManager>().PlayAngryEffect();
            }

        });

        seq.AppendInterval(waitTimeToDiscard);
        seq.Append(other.transform.DOMove(rightScenePos, cardLeaveDuration));

    }




    // PUBLIC INTERFACES
    

}