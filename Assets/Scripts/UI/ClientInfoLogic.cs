using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClientInfoLogic : MonoBehaviour
{
    [SerializeField] private float _infoUpdateDelaySeconds = 1f;
    private PersonData personData;

    public SpriteRenderer portraitSprite;
    public TextMeshPro nameField;

    public PersonData PersonData
    {
        get { return personData; }
        set
        {
            personData = value;
            Debug.Log("PersonData Set");
            StartCoroutine("UpdateInfo");
        }
    }

    private void UpdateCard()
    {
        Debug.Log("Updating Card");
        // Debug.Log(personData);
        if (personData != null)
        {
            // Debug.Log($"{personData.name} | {personData.Sprite}");
            ChangePicture(personData.Sprite);
            ChangeName(personData.Name);
        }
    }

    private void ChangePicture(Sprite sp)
    {
        portraitSprite.sprite = sp;
    }

    private void ChangeName(string name)
    {
        nameField.text = name;
    }

    private IEnumerator UpdateInfo()
    {
        Debug.Log("Updating Info");
        yield return new WaitForSeconds(_infoUpdateDelaySeconds);
        UpdateCard();
    }
}
