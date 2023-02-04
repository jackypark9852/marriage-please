using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundLogic : MonoBehaviour
{
    private PersonData client;
    private PersonData candidate1;
    private PersonData candidate2;

    List<PersonData> availablePersons = new List<PersonData>();
    float unsafeProbability = 0f;

    private PersonData candidateChosen;

    // List<PersonData> safePersons = new List<PersonData>();
    // List<PersonData> unsafePersons = new List<PersonData>();    

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
        // Art, SFX, UI effects
    }

    public void EndRound()
    {
        SetMarriage();
        // Art, SFX, UI effects
    }

    public void SetCandidates()
    {
        candidate1 = GetRandomSafePerson();

        float rand = Random.Range(0f, 1f);
        if (rand < unsafeProbability)
        {
            candidate2 = GetRandomUnsafePerson();
        }
        else
        {
            candidate2 = GetRandomSafePerson();
        }

        bool swap = Random.Range(0, 2) == 1;
        if (swap)
        {
            PersonData temp = candidate1;
            candidate1 = candidate2;
            candidate2 = temp;
        }
    }

    public PersonData GetRandomSafePerson()
    {
        throw new System.NotImplementedException();
    }

    public PersonData GetRandomUnsafePerson()
    {
        throw new System.NotImplementedException();
    }

    public void SetMarriage()
    {
        if (candidateChosen == null)
        {
            Debug.LogError("No candidate chosen");
            return;
        }
        throw new System.NotImplementedException();
    }
}
