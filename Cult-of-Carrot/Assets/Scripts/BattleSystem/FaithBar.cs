using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaithBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxFaith(int faith)
    {
        slider.maxValue = faith;
        slider.value = faith;

        fill.color = gradient.Evaluate(1f);
    }
    
    public void SetFaith(int faith)
    {
        slider.value = faith;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
