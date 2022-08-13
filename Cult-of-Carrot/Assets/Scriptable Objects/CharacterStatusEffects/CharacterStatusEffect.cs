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

[CreateAssetMenu(fileName = "CharacterStatusEffect", menuName = "Scriptable Objects/CharacterStatusEffect", order = 3)]
public class CharacterStatusEffect : ScriptableObject
{
    public string stateName = "";
    public bool castOnEnemy = true;
    [Multiline] public string description = "";
    public Texture image;
    public int statChangeValue;
    public bool doubleDamage;
    public int numTurns;
    public StatusEffectTypeChanged statusEffectType;
}
