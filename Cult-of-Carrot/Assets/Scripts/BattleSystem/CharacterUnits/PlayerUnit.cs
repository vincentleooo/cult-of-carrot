using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : CharacterUnitBase
{
    private int currentTurn;
    private Dictionary<string, int> skillCooldownDict = new Dictionary<string, int>();

    public void SetCurrentTurn(int currentTurn)
    {
        this.currentTurn = currentTurn;

        Dictionary<string, int> tempDict = new Dictionary<string, int>();

        foreach (KeyValuePair<string, int> kvp in skillCooldownDict)
        {
            // Remove from dictionary if cooldown period is over
            if (kvp.Value > currentTurn)
            {    
                tempDict.Add(kvp.Key, kvp.Value);
            }
        }

        skillCooldownDict.Clear();
        foreach (KeyValuePair<string, int> kvp in tempDict)
        {
            skillCooldownDict.Add(kvp.Key, kvp.Value);
        }

    }

    public bool CanCastSkill(Skill skill)
    {
        // If no cooldown period, cast skill
        if (skill.cooldown == 1)
        {
            print("No cooldown needed for " + skill.skillName);
            return true;
        }

        // If in dictionary, skill is still in cooldown
        if (skillCooldownDict.ContainsKey(skill.skillName)) return false;

        // Add skill to dictionary and start cooldown counter
        skillCooldownDict.Add(skill.skillName, skill.cooldown + currentTurn);
        return true;
    }

    public int GetSkillTurnsLeft(string skillName)
    {
        if (skillCooldownDict.ContainsKey(skillName))
        {
            return skillCooldownDict[skillName] - currentTurn;
        }
        else
        {
            return 0;
        }
    }

}
