using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FamilyData", menuName = "FamilyData", order = 0)]
public class FamilyData : ScriptableObject
{
    [Header("Person Data")]
    [SerializeField] private List<PersonData> _members;

    [Tooltip("The uppermost ancestors of the fmaily tree")]
    [SerializeField] private List<PersonData> _roots;
    public List<PersonData> Members => _members;

    public bool IsCloseRelative(PersonData person1, PersonData person2, int minSafeDistance = 2)
    {
        int distance = GetDistance(person1, person2);
        if (distance == -1) // No common ancestor found, so not related
        {
            return false;
        }

        return distance < minSafeDistance;
    }

    // Returns -1 if no common ancestor is found
    public int GetDistance(PersonData person1, PersonData person2)
    {
        List<PersonData> path1 = null;
        List<PersonData> path2 = null;
        foreach (var root in _roots)
        {
            path1 = FindPath(root, person1);
            path2 = FindPath(root, person2);
            if (path1 != null && path2 != null) // Stop searching if common ancestor is found
            {
                break;
            }
        }

        if (path1 == null || path2 == null) // No common ancestor found
        {
            return -1;
        }

        // Find the last common ancestor
        PersonData commonAncestor = null;
        foreach (var person in path1)
        {
            if (path2.Contains(person))
            {
                commonAncestor = person;
            }
        }

        if (commonAncestor == null) // just in case
        {
            return -1;
        }

        // Example:
        // path1 = [john, dave, bob]
        // path2 = [john, dave, bob, alice]
        // Lowest common ancestor is bob 
        // distance1 = 0 (bob to bob)
        // distance2 = 1 (bob to alice)
        // total distance = 1

        // Find the distance from the common ancestor to each person
        var distance1 = path1.Count - path1.IndexOf(commonAncestor) - 1;
        var distance2 = path2.Count - path2.IndexOf(commonAncestor) - 1;

        return distance1 + distance2;
    }


    // Find the path from root to person
    // If not found, return List of 0 length
    // If found, return list of PersonData from root to person
    private List<PersonData> FindPath(PersonData root, PersonData person)
    {
        var path = new List<PersonData>();
        path.Add(root);
        if (root == person)
        {
            return path;
        }

        // Compute subpaths because multiple children can lead to the same person
        List<List<PersonData>> subpaths = new List<List<PersonData>>();
        foreach (var child in root.Children)
        {
            List<PersonData> childPath = FindPath(child, person); // child is the new root
            if (childPath != null)
            {
                subpaths.Add(childPath); // add the path from child to person as a subpath
            }
        }

        List<PersonData> shortestSubpath = null;
        foreach (var subpath in subpaths)
        {
            if (shortestSubpath == null || subpath.Count < shortestSubpath.Count)
            {
                shortestSubpath = subpath;
            }
        }

        if (shortestSubpath != null) // If a subath from a child to a person was found
        {
            path.AddRange(shortestSubpath); // root + subpath
            return path;
        }

        // Reached if root != person && no subpath was found from any child to person 
        return null; // return empty list
    }
}