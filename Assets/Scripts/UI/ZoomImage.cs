using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomImage : MonoBehaviour
{
    public float zoomSpeed = 0.7f;
    public float minZoom = 0.2f;
    public float maxZoom = 10f;

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            float localScale = image.rectTransform.localScale.x;
            float zoom = localScale- scroll * zoomSpeed * localScale;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            image.rectTransform.localScale = new Vector3(zoom, zoom, 1f);
        }
    }
}

