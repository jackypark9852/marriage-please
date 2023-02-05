using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(FamilyLogic))]
public class RoundManager : Singleton<RoundManager>
{
    [HideInInspector] public int roundNum { get; private set; } = 0;
    [HideInInspector] public float timer { get; private set; }
    [HideInInspector] public float unsafeProbability { get; private set; }
    [HideInInspector] public Sprite familyTreeSprite { get; private set; }


    [Header("Script References")]
    [SerializeField] private InfoCardManager infoCardManager1;
    [SerializeField] private InfoCardManager infoCardManager2;
    [SerializeField] private ClientInfoLogic clientInfoLogic;

    [SerializeField] Image familyTreeImage;
    [SerializeField] SpriteRenderer familyTreeOpenerSR;
    [SerializeField] FamilyTreeCanvasWindowController familyTreeCanvasWindowController;

    [SerializeField] ProgressBar timerBar;

    [SerializeField] TraumaInducer traumaInducer;
    [Header("Data")]
    [SerializeField] List<StageData> stageDatas;
    [Header("Events")]
    public UnityEvent roundEnded;
    public UnityEvent stageEnded;
    public UnityEvent wrongCandidateChosen;
    public UnityEvent correctCandidateChosen;
    [Header("Debug")]
    [SerializeField] private TMPro.TextMeshProUGUI debugStageText;
    [SerializeField] private TMPro.TextMeshProUGUI debugClientText;
    [SerializeField] private TMPro.TextMeshProUGUI debugRoundText;
    [SerializeField] private TMPro.TextMeshProUGUI debugTimerText;
    [SerializeField] private TMPro.TextMeshProUGUI debugChoiceResultText;

    PersonData client;
    PersonData candidate1;
    PersonData candidate2;
    private PersonData candidateChosen;
    FamilyLogic familyLogic;
    StageData currentStageData;

    StageState stageState = StageState.BeforeStage;


    void Awake()
    {
        //ase.Awake();
        familyLogic = GetComponent<FamilyLogic>();
        EventManager.AddEvent("GameLost", new UnityAction(() => { }));
        Debug.Log("RoundManager Awake");
        //SetCandidates(); 
    }

    void Start()
    {
        Debug.Log("RoundManager Start");
        StartStage();
        wrongCandidateChosen.AddListener(OnInorrectMarry);
        correctCandidateChosen.AddListener(OnCorrectMarry);
        infoCardManager1.infoCardClicked.AddListener(OnInfoCardClicked);
        infoCardManager2.infoCardClicked.AddListener(OnInfoCardClicked);
        SetCandidates();
        SetInfoCardDatas();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timerBar.Val = timer / currentStageData.maxTimeLength;
        debugTimerText.text = timer.ToString(); // TODO: Remove this
        if (timer <= 0f)
        {
            EventManager.Invoke("LoseInterim");
        }
    }

    public void StartStage()
    {
        stageState = StageState.StageInProgress;
        roundNum = 0;
        Debug.Log("Starting stage " + GameManager.stageNum);
        debugStageText.text = "Stage: " + GameManager.stageNum; // TODO: Remove this
        currentStageData = stageDatas[GameManager.stageNum];
        familyLogic.FamilyData = stageDatas[GameManager.stageNum].familyData;
        familyLogic.FamilyData = currentStageData.familyData;
        timer = currentStageData.startTimeLength;
        unsafeProbability = currentStageData.unsafeStartingProbability;
        ChangeFamilyTreeSprite(currentStageData.familyTreeSprite);

        StartRound();
    }

    public void EndStage()
    {
        stageState = StageState.AfterStage;
        stageEnded.Invoke(); // TODO: Remove redundant event
        Debug.Log("Ending stage " + GameManager.stageNum);
        switch (GameManager.stageNum){
                case 0:
                    GameManager.ChangeState(GameState.Interim1);
                    break;
                case 1:
                    GameManager.ChangeState(GameState.Interim2);
                    break;
                case 2:
                    GameManager.ChangeState(GameState.WinInterim);
                    break;
                default:
                    GameManager.ChangeState(GameState.WinInterim);
                    break;
                
        }
    }

    public void IncrementRound()
    {
        roundNum++;
        roundEnded.Invoke();
    }

    public void StartRound()
    {
        client = familyLogic.GetClient();
        SetCandidates();
        SetInfoCardDatas();
        Debug.Log(familyTreeCanvasWindowController);
        familyTreeCanvasWindowController.SetProfileFrameActive(client, true);
    }

    public void EndRound()
    {
        MarriageInfo marriageInfo = familyLogic.Marry(candidateChosen, client);
        familyTreeCanvasWindowController.SetProfileFrameActive(client, false);
        debugChoiceResultText.text = marriageInfo.isMarriageAllowed ? "<color=green>Correct!</color>" : "<color=red>Wrong!</color>";
        if (!marriageInfo.isMarriageAllowed)
        {
            traumaInducer.InduceTrauma();
        }
        unsafeProbability += currentStageData.unsafeProbabilityIncrement;
        IncrementRound();
        if (roundNum >= currentStageData.gameRoundLength)
        {
            EndStage();
        }
        else
        {
            StartRound();
        }
        // TODO: Move infocards out of sight and update them
    }

    public void OnCorrectMarry()
    {
        timer += stageDatas[GameManager.stageNum].correctTimeIncrement;
        timer = Mathf.Clamp(timer, 0f, currentStageData.maxTimeLength);

    }

    public void OnInorrectMarry()
    {
        timer += stageDatas[GameManager.stageNum].incorrectTimeIncrement;
        timer = Mathf.Clamp(timer, 0f, currentStageData.maxTimeLength);
    }

    public void SetCandidates()
    {
        Debug.Log("SetCandidates");
        const int MAX_TRIES = 10;
        int tries = 0;
        bool candidatesFound = false;
        List<PersonData> triedClients = new List<PersonData>();
        while (tries < MAX_TRIES)
        {
            debugRoundText.text = "Round: " + roundNum; // TODO: Remove this
            client = familyLogic.GetClient(triedClients);
            if (client == null)
            {
                Debug.LogWarning("No clients left");
                EndStage();
                return;
            }
            debugClientText.text = client.Name; // TODO: Remove this

            candidate1 = familyLogic.GetSafeCandidate(client);
            float rand = Random.Range(0f, 1f);
            if (rand < unsafeProbability)
            {
                candidate2 = familyLogic.GetUnsafeCandidate(client, new List<PersonData> { candidate1 });
            }
            else
            {
                candidate2 = familyLogic.GetSafeCandidate(client, new List<PersonData> { candidate1 });
            }

            bool swap = Random.Range(0, 2) == 1;
            if (swap)
            {
                PersonData temp = candidate1;
                candidate1 = candidate2;
                candidate2 = temp;
            }
            tries++;
            triedClients.Add(client);
            if (candidate1 != null && candidate2 != null)
            {
                candidatesFound = true;
                break; ;
            }
        }

        if (!candidatesFound)
        {
            Debug.LogWarning("No suitable candidates found");
            EndStage();
        }
        return;
    }

    public void OnInfoCardClicked(PersonData personData)
    {
        if (stageState != StageState.StageInProgress)
        {
            return;
        }
        candidateChosen = personData;
        MarriageInfo marriageInfo = familyLogic.Marry(personData, client);
        // Debug.Log("Marrying " + personData.Name + " and " + client.Name + " is " + marriageInfo.isMarriageAllowed);
        if (marriageInfo.isMarriageAllowed)
        {
            correctCandidateChosen.Invoke();
        }
        else
        {
            wrongCandidateChosen.Invoke();
        }
        EndRound();
    }
    private void SetInfoCardDatas()
    {
        Debug.Log("SetInfoCardDatas"); 
        if (infoCardManager1 == null || infoCardManager2 == null || clientInfoLogic == null)
        {
            Debug.Log("InfoCardManager1 or InfoCardManager2 or ClientInfoLogic is null");
            return;
        }
        infoCardManager1.PersonData = candidate1;
        infoCardManager1.IsSafeChoice = !familyLogic.IsCloseRelative(client, candidate1);
        infoCardManager2.PersonData = candidate2;
        infoCardManager2.IsSafeChoice = !familyLogic.IsCloseRelative(client, candidate2);
        clientInfoLogic.PersonData = client;

        Debug.Log("SetInfoCardDatas done");
    }

    private void ChangeFamilyTreeSprite(Sprite sprite)
    {
        familyTreeSprite = sprite;
        familyTreeImage.sprite = sprite;
        familyTreeOpenerSR.sprite = sprite;
    }
}

public enum StageState
{
    BeforeStage,
    StageInProgress,
    AfterStage,
}
