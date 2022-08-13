using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDBar : MonoBehaviour
{
    // TODO: Add animation for increasing/decreasing value
    public Slider slider;
    public Image fill;

    public void SetValue(float value)
    {
        slider.value = value;
    }

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
    }
}
