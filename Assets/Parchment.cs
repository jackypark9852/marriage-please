using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Parchment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        transform.DOShakeScale(2, 0.05f);
    }
}
