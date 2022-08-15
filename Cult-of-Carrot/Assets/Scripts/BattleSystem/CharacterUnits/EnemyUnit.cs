using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : CharacterUnitBase
{
    System.Random random = new System.Random();

    public Skill SelectAttack()
    {
        List<Skill> enemySkills = characterStats.Skills;
        int skillIndex = random.Next(0, enemySkills.Count);

        return enemySkills[skillIndex];
    }
}
