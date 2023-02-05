
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine;
using UnityEngine.Events;

public class AnimationManager : MonoBehaviour
{

    [Header("InfoCards and PersonEffects: ")]
    public GameObject cardLeft;
    public GameObject cardRight;
    // public GameObject cardCandidate;
    public GameObject personEffectsHolder;


    [Header("The Random People Walk in:")]
    public GameObject personPlaceholder;
    public Sprite[] peopleSprites;



    [Header("Animation Hooks:")]
    public Vector3 outPos;
    public Vector3 dropPosition;
    public Vector3 leftScenePos;

    [Header("Gameobjects: ")]
    [SerializeField] private GameObject blockPanel;


    [Header("Animation Settings:")]
    public Vector3 rightScenePos;
    // public Vector3 candidateOutPos;
    // public Vector3 candidateScenePos;

    public float cardEnterDuration;

    public Vector3 personOutPos;
    public Vector3 personScenePos;
    public float personEnterDuartion;
    public float personLeaveDuartion;

    [Header("After Selection Hooks:")]
    public Vector3 resultPlace;
    public float randomRoateValue;
    public float cardLeaveDuration;

    [Header("After Show Result:")]
    public float waitTimeToDiscard;

    [Header("InfoCard Style Sprites:")]
    public GameObject leftCardStyle;
    public GameObject rightCardStyle;
    public Sprite[] cardStyles;


    [Header("VFXs:")]
    public string[] footstepAudios;
    // public string[] paperflipAudios;


    // Internal Vars
    private bool playerSelected;
    private bool playerCanSelect;


    // Start is called before the first frame update
    void Start()
    {
        ChangeInfoOnCard();
        StartRoundSequence();
        // SFXManager.PlayMusic("BadMale1");
    }

    // Update is called once per frame
    void Update()
    {

        // if(Input.GetKey(KeyCode.Q) && !playerSelected) {
        //     StartRoundSequence();
        // }

        // if(Input.GetKey(KeyCode.E) && !playerSelected) {
        //     playerSelected = true;
        //     takeChoice(cardLeft, false);
        //     // cardCandidate.GetComponent<InfoCardManager>().PlayAngryEffect();
        // }
    }

    // SEQUENCES

    public void StartRoundSequence()
    {
        // the random person walk from left to right to the scene

        personPlaceholder.transform.position = personOutPos;
        int index = Random.Range(0, peopleSprites.Length);
        personPlaceholder.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = peopleSprites[index];

        var seq = DOTween.Sequence();

        int audioIndex = Random.Range(0, footstepAudios.Length);
        SFXManager.PlayMusicLoop(footstepAudios[audioIndex]);

        seq.Append(personPlaceholder.transform.DOMove(personScenePos, personEnterDuartion));
        seq.Join(personPlaceholder.transform.GetChild(0).DOPunchPosition(new Vector3(0, 0.5f, 0), personEnterDuartion));
        seq.AppendCallback(() => {
            SFXManager.StopMusic(footstepAudios[audioIndex]);
        });


        // make the cards fly into the scence

        cardLeft.transform.position = outPos;
        cardLeft.transform.rotation = Quaternion.identity;
        cardRight.transform.position = outPos;
        cardRight.transform.rotation = Quaternion.identity;


        seq.Append(cardLeft.transform.DOMove(leftScenePos, cardEnterDuration));
        seq.Join(cardRight.transform.DOMove(rightScenePos, cardEnterDuration));

        Vector3 rot = new Vector3(0, 0, Random.Range(0, randomRoateValue));
        seq.Join(cardLeft.transform.DORotate(rot, cardLeaveDuration));
        Vector3 rot2 = new Vector3(0, 0, Random.Range(-randomRoateValue, 0));
        seq.Join(cardRight.transform.DORotate(rot2, cardLeaveDuration));
        // seq.Join(cardLeft.transform.DOShakePosition(cardEnterDuration));

        // make the candidate card fly from bottom to inside the scene
        // cardLeft.transform.DOMove(leftScenePos, cardEnterDuration);

        // cardCandidate.transform.position = candidateOutPos;
        // cardCandidate.transform.rotation = Quaternion.identity;

        // seq.Append(cardCandidate.transform.DOMove(candidateScenePos, cardEnterDuration));

        // Make player can choose
        seq.AppendCallback(() =>
        {
            playerCanSelect = true;
            blockPanel.SetActive(false);
        });

    }

    public void takeChoice(GameObject sel, bool isCorrect)
    {
        Debug.Log("Take choice name: " + sel.GetComponent<InfoCardManager>().PersonData.Name);
        Debug.Log("Take choice isCorrect: " + isCorrect);

        if (!playerCanSelect)
        {
            return;
        }

        Debug.Log("taked choice");
        blockPanel.SetActive(true); // Activate the block panel to prevent player from clicking again
        playerSelected = false;
        playerCanSelect = false;

        bool isLeft = sel == cardLeft;

        GameObject selected;
        GameObject other;
        // Vector3 wrongOutPos;
        // Vector3 correctOutPos;
        if (isLeft)
        {
            other = cardRight;
            selected = cardLeft;
            // wrongOutPos = rightOutPos;
            // correctOutPos = leftOutPos;
        }
        else
        {
            other = cardLeft;
            selected = cardRight;
            // wrongOutPos = leftOutPos;
            // correctOutPos = rightOutPos;
        }


        // play anims
        if (isCorrect)
        {
            personEffectsHolder.GetComponent<InfoCardManager>().PlayHeartAnim();
            selected.GetComponent<InfoCardManager>().PlayHeartAnim();
        }
        else
        {
            personEffectsHolder.GetComponent<InfoCardManager>().PlayAngryEffect();
            selected.GetComponent<InfoCardManager>().PlayAngryEffect();
        }

        // make the wrong card fly away from the scene
        var seq = DOTween.Sequence();
        seq.Append(other.transform.DOMove(outPos, cardLeaveDuration));

        // if the card is correct, then fly card there and passaway with the person.
        // if (isCorrect) {
        //     // make the selcted cards to the designed postion
        //     seq.Append(selected.transform.DOMove(resultPlace, cardLeaveDuration));
        //     // seq.Join(selected.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), cardLeaveDuration));

        //     // make some random rotation
        //     Vector3 rot = new Vector3(0, 0, Random.Range(0, randomRoateValue));
        //     seq.Join(selected.transform.DORotate(rot, cardLeaveDuration));

        //     // the person left the room
        //     seq.Append(personPlaceholder.transform.DOMove(personOutPos, cardLeaveDuration));
        //     seq.Join(personPlaceholder.transform.GetChild(0).DOPunchPosition(new Vector3(0, 0.3f, 0), cardLeaveDuration));

        //     // all cards fly outside the screen
        //     seq.Join(selected.transform.DOMove(personOutPos, cardLeaveDuration));
        // }

        // add callback at the end;
        // seq.AppendCallback(() => {
        //     if (isCorrect) {
        //         cardCandidate.GetComponent<InfoCardManager>().PlayHeartAnim();
        //         selected.GetComponent<InfoCardManager>().PlayHeartAnim();
        //     }
        //     else {
        //         cardCandidate.GetComponent<InfoCardManager>().PlayAngryEffect();
        //         selected.GetComponent<InfoCardManager>().PlayAngryEffect();
        //     }

        // });

        // seq.AppendInterval(waitTimeToDiscard);
        // seq.Append(other.transform.DOMove(rightScenePos, cardLeaveDuration));



        // the person left the room

        // int index = Random.Range (0, peopleSprites.Length);
        // personPlaceholder.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = peopleSprites[index];

        int index = Random.Range(0, footstepAudios.Length);
        seq.AppendCallback(() => {
            SFXManager.PlayMusicLoop(footstepAudios[index]);
        });

        if (isCorrect)
        {
            seq.PrependCallback(() => {
                SFXManager.PlayMusic("Heartbeat");
            });
            
            seq.Append(personPlaceholder.transform.DOMove(personOutPos, cardLeaveDuration));
            seq.Join(personPlaceholder.transform.GetChild(0).DOPunchPosition(new Vector3(0, 0.3f, 0), cardLeaveDuration));

            // all cards fly outside the screen
            seq.Join(selected.transform.DOMove(personOutPos, cardLeaveDuration));
            // seq.Join(cardCandidate.transform.DOMove(leftOutPos, cardLeaveDuration));
        }
        else
        {
            seq.PrependCallback(() => {
                SFXManager.PlayMusic("Humm");
            });
            
            seq.Append(personPlaceholder.transform.DOMove(personOutPos, cardLeaveDuration));
            seq.Join(personPlaceholder.transform.GetChild(0).DOPunchPosition(new Vector3(0, 0.3f, 0), cardLeaveDuration));
            seq.Append(selected.transform.DOMove(dropPosition, cardLeaveDuration));

        }

        seq.AppendCallback(() =>
        {
            SFXManager.StopMusic(footstepAudios[index]);

            ChangeInfoOnCard();
            // RandomChangeCardStyle(leftCardStyle);
            // RandomChangeCardStyle(rightCardStyle);
            StartRoundSequence();
        });

    }




    // PRIVATE HELPERS
    private void ChangeInfoOnCard()
    {
        cardLeft.GetComponent<InfoCardManager>().UpdateCard();
        cardRight.GetComponent<InfoCardManager>().UpdateCard();
    }

    private void RandomChangeCardStyle(GameObject card)
    {
        int index = Random.Range(0, cardStyles.Length);
        card.GetComponent<SpriteRenderer>().sprite = cardStyles[index];
    }

}