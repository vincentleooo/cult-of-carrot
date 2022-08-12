using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Rename this to character effects

public enum StatusEffectTypeChanged
{
    FAITH,
    PWR,
    DEF,
    BLOCKTURN
}

[CreateAssetMenu(fileName = "CharacterStatusEffects", menuName = "Scriptable Objects/CharacterStatusEffects", order = 3)]
public class CharacterStatusEffects : ScriptableObject
{
    public string stateName = "";
    public bool castOnEnemy = true;
    [Multiline] public string description = "";
    public int statChangeValue;
    public int numTurns;
    public StatusEffectTypeChanged[] statusEffectType;
}
