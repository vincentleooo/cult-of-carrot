using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skills", menuName = "Scriptable Objects/Skills", order = 1)]
public class Skill : ScriptableObject
{
    public string skillName;
    [Multiline] public string skillDescription;
    public float changeFaith;
	public float changePower;
	public float changeDef;
    public int cooldown;
    public CharacterStatusEffect statusEffect;
    public bool isSingleTarget;
    // public bool isMultiTarget;
    public bool isEnemyCast;

    public GameObject skillAnim;
}
