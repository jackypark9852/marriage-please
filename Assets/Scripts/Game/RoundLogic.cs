using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundLogic : MonoBehaviour
{
    private PersonData client;
    private PersonData candidate1;
    private PersonData candidate2;

    float unsafeProbability = 0f;

    private PersonData candidateChosen;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRound()
    {
        SetCandidates();
    }

    public void EndRound()
    {
        // Marry(candidateChosen, client);
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
