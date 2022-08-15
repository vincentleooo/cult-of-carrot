using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDBattlePanel : MonoBehaviour
{
    [SerializeField] private Text battleText;

    public void UpdateBattleText(string text)
    {
        battleText.text = text;
    }
}
