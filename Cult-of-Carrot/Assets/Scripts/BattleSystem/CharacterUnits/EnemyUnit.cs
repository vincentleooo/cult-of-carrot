using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : CharacterUnitBase
{
    System.Random random = new System.Random();

    public Skills SelectAttack()
    {
        Skills[] enemySkills = characterStats.Skills;
        int skillIndex = random.Next(0, enemySkills.Length);

        return enemySkills[skillIndex];
    }
}
