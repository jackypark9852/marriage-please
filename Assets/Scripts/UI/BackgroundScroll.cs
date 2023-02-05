using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f;
    [SerializeField] float startY = 245f;
    [SerializeField] float endY = -250f;
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
    }
    void Update()
    {
        if (transform.position.y > endY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - scrollSpeed * Time.deltaTime, transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, startY, transform.localPosition.z);
        }
    }
}
