using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSkills", menuName = "Scriptable Objects/WeaponSkills", order = 2)]
public class WeaponSkills : ScriptableObject
{
    public string weaponSkillName;
    [Multiline] public string weaponSkillDescription;
	// public Image skillImage;
    public float changeFaith;
	public float changePower;
	public float changeDef;
    public bool canBeUsed; // TODO: Handle when the weapon skill can be used
    public CharacterStatusEffects resultingStatusEffect;
}
