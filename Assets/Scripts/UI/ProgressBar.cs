using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{

    [Header("Bar Setting")]
    public Color BarColor;
    public Color BarBackGroundColor;
    public Sprite BarBackGroundSprite;

    private Image bar, barBackground;
    private float val;
    public float Val
    {
        get { return val; }

        set
        {
            value = Mathf.Clamp(value, 0, 1);
            val = value;
            UpdateValue(val);

        }
    }
    
    [SerializeField] TMP_Text text;

    private void Awake()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
        barBackground = GetComponent<Image>();
        barBackground = transform.Find("BarBackground").GetComponent<Image>();
    }

    private void Start()
    {
        bar.color = BarColor;
        barBackground.color = BarBackGroundColor;
        barBackground.sprite = BarBackGroundSprite;

        UpdateValue(val);
    }

    void UpdateValue(float val)
    {
        // text.text = $"{Mathf.Min(losingFarmValue, Mathf.RoundToInt(val * losingFarmValue))}/{losingFarmValue} PLANTED";  //| Hard-coded
        bar.fillAmount = val;
    }


    private void Update()
    {
        if (!Application.isPlaying)
        {
            UpdateValue(0.5f);  //|

            bar.color = BarColor;
            barBackground.color = BarBackGroundColor;

            barBackground.sprite = BarBackGroundSprite;
        }
    }
}
