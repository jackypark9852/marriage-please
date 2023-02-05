using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClientInfoLogic : MonoBehaviour
{
    private PersonData personData;

    public SpriteRenderer portraitSprite;
    public TextMeshPro nameField;

    public PersonData PersonData
    {
        get { return personData; }
        set
        {
            personData = value;
            // Debug.Log(personData);
            if (personData != null)
            {
                Debug.Log($"{personData.name} | {personData.Sprite}");
            }
            ChangePicture(personData.Sprite);
            ChangeName(personData.name);
        }
    }

    public void ChangePicture(Sprite sp)
    {
        portraitSprite.sprite = sp;
    }

    public void ChangeName(string name)
    {
        nameField.text = name;
    }
}
