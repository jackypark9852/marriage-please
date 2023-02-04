using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundManager : Singleton<RoundManager>
{
    [HideInInspector] public int stageNum { get; private set; } = 0;
    [HideInInspector] public int roundNum { get; private set; } = 0;
    [HideInInspector] public float timer { get; private set; }

    PersonData client;
    PersonData candidate1;
    PersonData candidate2;
    private PersonData candidateChosen;
    
    float unsafeProbability = 0f;


    [SerializeField] UnityEvent OnRoundChange;

    void Awake()
    {
        timer = GameSettings.START_TIME_LENGTHS[stageNum];
    }

    void Update()
    {
        timer -= Time.deltaTime;
    }

    public void IncrementRound()
    {
        roundNum++;
        OnRoundChange.Invoke();
    }
    

    public void StartRound()
    {
        SetCandidates();
    }

    public void EndRound()
    {
        // Marry(candidateChosen, client);
    }

    public void OnCorrectMarry()
    {
        timer += GameSettings.CORRECT_TIME_INCREMENT;
        timer = Mathf.Clamp(timer, 0f, GameSettings.MAX_TIME_LENGTHS[stageNum]);
    }

    public void OnInorrectMarry()
    {
        timer += GameSettings.INCORRECT_TIME_INCREMENT;
        timer = Mathf.Clamp(timer, 0f, GameSettings.MAX_TIME_LENGTHS[stageNum]);
    }

    public void SetCandidates()
    {
        // candidate1 = GetRandomSafePerson();

        float rand = Random.Range(0f, 1f);
        if (rand < unsafeProbability)
        {
            // candidate2 = GetRandomUnsafePerson();
        }
        else
        {
            // candidate2 = GetRandomSafePerson();
        }

        bool swap = Random.Range(0, 2) == 1;
        if (swap)
        {
            PersonData temp = candidate1;
            candidate1 = candidate2;
            candidate2 = temp;
        }
    }

    /*
    public PersonData GetRandomSafePerson()
    {
        // List<PersonData> safePersons = GetSafePerson();
        return safePersons[Random.Range(0, safePersons.Count)];
    }

    public PersonData GetRandomUnsafePerson()
    {
        // List<PersonData> unsafePersons = GetUnsafePerson();
        return unsafePersons[Random.Range(0, unsafePersons.Count)];
    }
    */
}
