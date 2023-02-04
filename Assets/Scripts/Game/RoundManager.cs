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
    [SerializeField] private List<InfoCardManager> infoCardManagers;
    [Header("Data")]
    [SerializeField] List<StageData> stageDatas;
    [Header("Events")]
    public UnityEvent roundChanged;
    public UnityEvent stageChanged;
    public UnityEvent wrongCandidateChosen;
    public UnityEvent correctCandidateChosen;

    PersonData client;
    PersonData candidate1;
    PersonData candidate2;
    private PersonData candidateChosen;
    FamilyLogic familyLogic;
    StageData currentStageData;

    void Awake()
    {
        familyLogic = GetComponent<FamilyLogic>();
    }

    void Start()
    {
        StartStage();
        wrongCandidateChosen.AddListener(OnInorrectMarry);
        correctCandidateChosen.AddListener(OnCorrectMarry);
        foreach (InfoCardManager infoCardManager in infoCardManagers)
        {
            infoCardManager.infoCardClicked.AddListener(OnInfoCardClicked);
        }
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
        currentStageData = stageDatas[stageNum];
        familyLogic.FamilyData = currentStageData.familyData;
        timer = currentStageData.startTimeLength;
        unsafeProbability = currentStageData.unsafeStartingProbability;

        StartRound();
    }

    public void EndStage()
    {

    }

    public void IncrementRound()
    {
        roundNum++;
        roundChanged.Invoke();
    }

    public void StartRound()
    {
        familyLogic.FamilyData = stageDatas[stageNum].familyData;
        client = familyLogic.GetClient();
        SetCandidates();
    }

    public void EndRound()
    {
        familyLogic.Marry(candidateChosen, client);
        unsafeProbability += currentStageData.unsafeProbabilityIncrement;
        IncrementRound();
        if (roundNum >= currentStageData.gameRoundLength)
        {
            stageNum++;
            EndStage();
        }
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
            candidate1 = familyLogic.GetUnsafeCandidate(client, new List<PersonData> { candidate1 });
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
        if (marriageInfo.isMarriageAllowed)
        {
            correctCandidateChosen.Invoke();
        }
        else
        {
            wrongCandidateChosen.Invoke();
        }
    }
}
