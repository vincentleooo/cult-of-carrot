using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public TMP_Text faithValueText;
    public TMP_Text maxFaithValueText;

    public void SetValue(float value)
    {
        if (value < 0) value = 0f;
        slider.value = value;
        faithValueText.text = value.ToString();
    }

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
        maxFaithValueText.text = "/" + value.ToString();
    }
}
