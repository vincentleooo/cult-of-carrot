using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumables", menuName = "Scriptable Objects/Consumables", order = 6)]
public class Consumables : ScriptableObject
{
    public string consumableName;
    [Multiline] public string consumableDescription;
    public float changeFaith;
	public float changePower;
	public float changeDef;
    public int numTurns; // Not sure if we need this yet
    public int numberOfConsummables; // Not sure if we need this yet
    public StatusEffectTypeChanged[] statusEffectType; // Not sure if we need this yet

}
