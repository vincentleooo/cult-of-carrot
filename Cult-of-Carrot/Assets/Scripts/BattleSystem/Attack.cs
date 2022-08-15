using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : ScriptableObject
{
    public Skill skill;
    public bool canCast;

    public Attack(Skill skill)
    {
        this.skill = skill;
    }
}
