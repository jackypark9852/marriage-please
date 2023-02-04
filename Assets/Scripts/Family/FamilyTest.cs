using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FamilyTest : MonoBehaviour
{
    [SerializeField] FamilyData familyData;
    [SerializeField] PersonData person1;
    [SerializeField] PersonData person2;

    private void Update()
    {
        Debug.Log(familyData.GetDistance(person1, person2));
    }
}
