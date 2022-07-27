using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefBar : MonoBehaviour
{
    public Slider slider;
    // public Gradient gradient;
    public Image fill;

    public void SetMaxDef(int def)
    {
        slider.maxValue = def;
        slider.value = def;

        // fill.color = gradient.Evaluate(1f);
    }
    
    public void SetDef(int def)
    {
        slider.value = def;
        // fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
