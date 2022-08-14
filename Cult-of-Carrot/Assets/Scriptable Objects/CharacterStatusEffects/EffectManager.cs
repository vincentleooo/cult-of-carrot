using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EffectIndex
{
    DAZED,
    ENRAGED,
    TAUNTED,
    INTEREST,
    CASSLIGHT,
    DRUNK,
    SPIRITUAL_AWAKENING
}

public class EffectManager : MonoBehaviour
{
    public StatusEffectsPanel statusEffectsPanel;
    public List<GameObject> statusEffectsIcons;

    void Start() 
    {
        statusEffectsPanel.battleStarted = true;
        statusEffectsPanel.Setup(statusEffectsIcons.Count);
        ResetStatusEffects();
    }

    public void ResetStatusEffects()
    {
        for (int i = 0; i < statusEffectsIcons.Count; i++)
        {
            statusEffectsIcons[i].SetActive(false);
        }
    }

    void AddStatusEffectIconUI(int index, Texture texture)
    {
        statusEffectsIcons[index].GetComponent<RawImage>().texture = texture;
        statusEffectsIcons[index].SetActive(true);
    }

    public void AddStatusEffect(CharacterStatusEffect statusEffect)
    {
        int effectIndex = (int) statusEffect.effectIndex;
        statusEffectsPanel.Add(statusEffect, effectIndex);
        AddStatusEffectIconUI(effectIndex, statusEffect.texture);
    }

    public void RemoveStatusEffect(int index)
    {
        statusEffectsPanel.Remove(index);
        statusEffectsIcons[index].SetActive(false);
    }

    public void ResetValues()
    {
        statusEffectsPanel.Clear();
        ResetStatusEffects();
    }

    private void OnApplicationQuit() 
    {
        ResetValues();    
    }
}
