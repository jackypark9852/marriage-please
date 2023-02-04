using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PersonData", menuName = "marriage-please/PersonData", order = 0)]
public class PersonData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private List<PersonData> _children;
    public string Name => _name;
    public Sprite Sprite => _sprite;
    public List<PersonData> Children => _children;
}