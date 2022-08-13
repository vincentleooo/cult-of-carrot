using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Scriptable Objects/CharacterStats", order = 0)]
public class CharacterStats : ScriptableObject 
{
    public string charName = "";
    public int node = 1;
    public float Faith = 30f;
    public float Power = 10f;
    public float Defence = 10f;
	public List<Skill> Skills;

    public void SetNode(int node)
    {
        this.node = node;
    }

    public void SetStats(float faith, float power, float defence) 
    {
        this.Faith = faith;
        this.Power = power;
        this.Defence = defence;
    }

    public void AddSkill(Skill skill)
    {
        Skills.Add(skill);
    }
}
