using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FamilyLogic))]
public class RoundManager : Singleton<RoundManager>
{
    [HideInInspector] public int stageNum { get; private set; } = 0;
    [HideInInspector] public int roundNum { get; private set; } = 0;
    [HideInInspector] public float timer { get; private set; }
    [HideInInspector] public float unsafeProbability { get; private set; }

    [Header("Script References")]
    [SerializeField] private InfoCardManager infoCardManager1;
    [SerializeField] private InfoCardManager infoCardManager2;
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

    PersonData client;
    PersonData candidate1;
    PersonData candidate2;
    private PersonData candidateChosen;
    FamilyLogic familyLogic;
    StageData currentStageData;

    StageState stageState = StageState.BeforeStage;

    void Awake()
    {
        familyLogic = GetComponent<FamilyLogic>();
        EventManager.AddEvent("StagePassed", new UnityAction(() => {StartStage(); }));
        EventManager.AddEvent("GameLost", new UnityAction(() => { }));

    }

    void Start()
    {
        StartStage();
        wrongCandidateChosen.AddListener(OnInorrectMarry);
        correctCandidateChosen.AddListener(OnCorrectMarry);
        infoCardManager1.infoCardClicked.AddListener(OnInfoCardClicked);
        infoCardManager2.infoCardClicked.AddListener(OnInfoCardClicked);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        debugTimerText.text = timer.ToString(); // TODO: Remove this
        if (timer <= 0f)
        {
            EventManager.Invoke("GameLost");
        }
    }

    public void StartStage()
    {
        stageState = StageState.StageInProgress;
        roundNum = 0;
        Debug.Log("Starting stage " + stageNum);
        debugStageText.text = "Stage: " + stageNum; // TODO: Remove this
        currentStageData = stageDatas[stageNum];
        familyLogic.FamilyData = stageDatas[stageNum].familyData;
        familyLogic.FamilyData = currentStageData.familyData;
        timer = currentStageData.startTimeLength;
        unsafeProbability = currentStageData.unsafeStartingProbability;

        StartRound();
    }

    public void EndStage()
    {
        stageState = StageState.AfterStage;
        stageEnded.Invoke(); // TODO: Remove redundant event
        

        stageNum++;
        if (stageNum >= stageDatas.Count)
        {
            EventManager.Invoke("GameWon");
        } else {
            Debug.Log("Stage passed");
            EventManager.Invoke("StagePassed");
        }
    }

    public void IncrementRound()
    {
        roundNum++;
        roundEnded.Invoke();
    }

    public void StartRound()
    {
        debugRoundText.text = "Round: " + roundNum; // TODO: Remove this
        client = familyLogic.GetClient();
        debugClientText.text = client.name; // TODO: Remove this
        SetCandidates();
        SetInfoCardDatas();
    }

    public void EndRound()
    {
        familyLogic.Marry(candidateChosen, client);
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
        timer += GameSettings.CORRECT_TIME_INCREMENT;
        timer = Mathf.Clamp(timer, 0f, currentStageData.maxTimeLength);

    }

    public void OnInorrectMarry()
    {
        timer += GameSettings.INCORRECT_TIME_INCREMENT;
        timer = Mathf.Clamp(timer, 0f, currentStageData.maxTimeLength);
    }

    public void SetCandidates()
    {
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
        infoCardManager1.PersonData = candidate1;
        infoCardManager2.PersonData = candidate2;
    }
}

public enum StageState
{
    BeforeStage,
    StageInProgress,
    AfterStage,
}
