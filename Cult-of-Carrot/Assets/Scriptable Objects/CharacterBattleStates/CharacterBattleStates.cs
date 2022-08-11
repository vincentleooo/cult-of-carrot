using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Rename this to character effects

public enum StatTypeChanged
{
    FAITH,
    PWR,
    DEF,
    BLOCKTURN
}

[CreateAssetMenu(fileName = "CharacterBattleStates", menuName = "Scriptable Objects/CharacterBattleStates", order = 3)]
public class CharacterBattleStates : ScriptableObject
{
    public string stateName = "";
    public bool castOnEnemy = true;
    [Multiline] public string description = "";
    public int statChangeValue;
    public int numTurns;
    public StatTypeChanged statType;
}
