using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyLogic : MonoBehaviour
{
    [Header("Family Data - testing purposes only")]
    [SerializeField] private FamilyData _familyData;
    private List<PersonData> availablePeople;
    private List<MarriageInfo> marriages;
    private List<PersonData> dishonorables; // TODO: Descendants of people who married close relatives
    public FamilyData FamilyData
    {
        get { return _familyData; }
        set
        {
            _familyData = value;
            InitializeMembers();
        }
    }
    public List<MarriageInfo> Marriages => marriages;
    public List<PersonData> AvailablePeople => availablePeople;
    public List<PersonData> Dishonorables => dishonorables;
    private void Start()
    {
        InitializeMembers();
    }

    public MarriageInfo Marry(PersonData person1, PersonData person2)
    {
        availablePeople.Remove(person1);
        availablePeople.Remove(person2);

        MarriageInfo marriageInfo;
        marriageInfo.person1 = person1;
        marriageInfo.person2 = person2;
        marriageInfo.isMarriageAllowed = !_familyData.IsCloseRelative(person1, person2);
        marriageInfo.distance = _familyData.GetDistance(person1, person2);
        marriages.Add(marriageInfo);
        return marriageInfo;
    }

    public PersonData GetSafeCandidate(PersonData person)
    {
        foreach (var availablePerson in availablePeople)
        {
            if (!_familyData.IsCloseRelative(person, availablePerson))
            {
                return availablePerson;
            }
        }
        return null;
    }

    public PersonData GetUnsafeCandidate(PersonData person)
    {
        foreach (var availablePerson in availablePeople)
        {
            if (_familyData.IsCloseRelative(person, availablePerson))
            {
                return availablePerson;
            }
        }
        return null;
    }

    public PersonData GetClient(PersonData person)
    {
        // If there are less than 3 people left, there won't be candidates for the client
        if (availablePeople.Count < 3)
        {
            return null;
        }
        return availablePeople[0];
    }

    private void InitializeMembers()
    {
        // Set availiblePeople as randomized version of _familyData.Members
        availablePeople = _familyData.Members;
        Shuffle(availablePeople);
        marriages = new List<MarriageInfo>();
        dishonorables = new List<PersonData>();
    }

    private void Shuffle(List<PersonData> a)
    {
        // Loop array
        for (int i = a.Count - 1; i > 0; i--)
        {
            // Randomize a number between 0 and i (so that the range decreases each time)
            int rnd = UnityEngine.Random.Range(0, i);

            // Save the value of the current i, otherwise it'll overwrite when we swap the values
            PersonData temp = a[i];

            // Swap the new and old values
            a[i] = a[rnd];
            a[rnd] = temp;
        }

        // Print
        for (int i = 0; i < a.Count; i++)
        {
            Debug.Log(a[i]);
        }
    }
}
public struct MarriageInfo
{
    public PersonData person1;
    public PersonData person2;
    public bool isMarriageAllowed;
    public int distance;
}
