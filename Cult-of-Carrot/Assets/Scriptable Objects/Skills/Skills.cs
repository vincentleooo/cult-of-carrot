using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skills", menuName = "Scriptable Objects/Skills", order = 1)]
public class Skills : ScriptableObject
{
    public string skillName;
    [Multiline] public string skillDescription;
	// public Image skillImage;
    public int changeFaith;
	public int changePower;
	public int changeDef;
    public int cooldown; // TODO: Handle skill cooldowns

}
