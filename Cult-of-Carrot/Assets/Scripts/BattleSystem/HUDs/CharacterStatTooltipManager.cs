using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStatTooltipManager : TooltipManager
{
    public void SetTooltipText(float power, float def)
    {
        string text = $@"
            PWR: {power}
            DEF: {def}
        ";
        base.SetTooltipText(text);
    }
}