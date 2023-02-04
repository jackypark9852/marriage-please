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
    public UnityEvent roundChanged;
    public UnityEvent stageChanged;
    public UnityEvent wrongCandidateChosen;
    public UnityEvent correctCandidateChosen;
    [Header("Debug")]
    [SerializeField] private TMPro.TextMeshProUGUI debugStageText;
    [SerializeField] private TMPro.TextMeshProUGUI debugClientText;
    [SerializeField] private TMPro.TextMeshProUGUI debugRoundText;

    PersonData client;
    PersonData candidate1;
    PersonData candidate2;
    private PersonData candidateChosen;
    FamilyLogic familyLogic;
    StageData currentStageData;

    void Awake()
    {
        familyLogic = GetComponent<FamilyLogic>();
        EventManager.AddEvent("StageEnded", new UnityAction(() => { }));
        EventManager.AddEvent("GameLost", new UnityAction(() => { }));
        EventManager.AddEvent("GameWon", new UnityAction(() => { }));

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
        if (timer <= 0f)
        {
            GameManager.Instance.ChangeState(GameState.GameOver);
        }
    }

    public void StartStage()
    {
        roundNum = 0;
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
        Debug.Log("EndStage: " + stageNum);
        stageChanged.Invoke(); // TODO: Remove redundant event
        EventManager.Invoke("StageEnded");

        stageNum++;
        if (stageNum < stageDatas.Count)
        {
        }
    }

    public void IncrementRound()
    {
        roundNum++;
        roundChanged.Invoke();
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
        GameManager.Instance.ChangeState(GameState.Correct);
    }

    public void OnInorrectMarry()
    {
        timer += GameSettings.INCORRECT_TIME_INCREMENT;
        timer = Mathf.Clamp(timer, 0f, currentStageData.maxTimeLength);
        GameManager.Instance.ChangeState(GameState.Wrong);
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
        candidateChosen = personData;
        MarriageInfo marriageInfo = familyLogic.Marry(personData, client);
        Debug.Log("Marrying " + personData.Name + " and " + client.Name + " is " + marriageInfo.isMarriageAllowed);
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
