using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum BattleState 
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WIN,
    LOST
}

public class BattleSystemManager : MonoBehaviour
{
    public HUDBattlePanel battlePanel;
    public SkillsPanel skillsPanel;

    public GameObject[] enemyPrefabs;
    public GameObject playerPrefab;

    public Transform[] enemyBattlePositions;
    public Transform playerBattlePosition;

    private PlayerUnit playerUnit;
    private EnemyUnit[] enemyUnits;

    private BattleState battleState;

    private bool playerHasClicked = true;
    private int enemiesRemaining;
    private int currentTurn;

    void Start()
    {
        enemyUnits = new EnemyUnit[enemyBattlePositions.Length];
        enemiesRemaining = enemyBattlePositions.Length;
        battleState = BattleState.START;
        currentTurn = 0;
        StartCoroutine(BeginBattle());
    }

    IEnumerator BeginBattle()
    {
        // spawn player on battle stations
        playerPrefab = Instantiate(playerPrefab, playerBattlePosition);
        playerPrefab.SetActive(true);
        playerUnit = playerPrefab.GetComponent<PlayerUnit>();
        
        // spawn enemies on battle stations
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            enemyPrefabs[i] = Instantiate(enemyPrefabs[i], enemyBattlePositions[i]);
            enemyPrefabs[i].SetActive(true);
            enemyUnits[i] = enemyPrefabs[i].GetComponent<EnemyUnit>();
        }

        yield return new WaitForSeconds(3);

        battleState = BattleState.PLAYERTURN;
        
        yield return StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        Debug.Log(currentTurn);
        battlePanel.UpdateBattleText("Player's Turn. Carrot be with you.");
        skillsPanel.SetCurrentTurn(currentTurn);
        skillsPanel.EnableSkillButtons();
        playerHasClicked = false;
        yield return null;
    }

    public void PlayerAttackChosen(Attack attack)
    {
        if (battleState != BattleState.PLAYERTURN) return;

        if (!attack.canCast)
        {
            battlePanel.UpdateBattleText("Cannot cast " + attack.skill.skillName + " during cooldown");
            return;
        }

        if (!playerHasClicked)
        {
            StartCoroutine(PlayerSkillAttack(attack.skill));
            playerHasClicked = true;
            skillsPanel.DisableSkillButtons();
        }
    }

    IEnumerator PlayerSkillAttack(Skill skill)
    {
        battlePanel.UpdateBattleText("Player used " + skill.skillName);

        float perEnemyFaithDamage = skill.changeFaith; // enemiesRemaining;
        float perEnemyPwrDamage = skill.changePower; // enemiesRemaining;
        float perEnemyDefDamage = skill.changeDef; // enemiesRemaining;

        foreach (EnemyUnit e in enemyUnits)
        {
            e.TakeDamage(perEnemyFaithDamage, perEnemyPwrDamage, perEnemyDefDamage);
            if (e.IsDefeated()) enemiesRemaining--;
            yield return new WaitForSeconds(1);
        }

        if (enemiesRemaining > 0)
        {
            battleState = BattleState.ENEMYTURN;
            yield return StartCoroutine(EnemiesAttack());
        }
        
        else
        {
            battleState = BattleState.WIN;
            yield return StartCoroutine(EndBattle());
        }
    }

    IEnumerator EnemiesAttack()
    {
        battlePanel.UpdateBattleText("Enemy's turn. You better start praying.");

        yield return new WaitForSeconds(2);

        foreach (EnemyUnit e in enemyUnits)
        {
            Skill enemySkill = e.SelectAttack();
            battlePanel.UpdateBattleText("Enemy used " + enemySkill.skillName);
            playerUnit.TakeDamage(enemySkill.changeFaith, enemySkill.changePower, enemySkill.changeDef);

            yield return new WaitForSeconds(1);

            if (playerUnit.IsDefeated())
            {
                battleState = BattleState.LOST;

                yield return StartCoroutine(EndBattle());
                yield break;
            }
        }

        yield return new WaitForSeconds(2);

        currentTurn += 1;
        battleState = BattleState.PLAYERTURN;

        yield return StartCoroutine(PlayerTurn());
    }

    IEnumerator EndBattle()
    {
        if (battleState == BattleState.WIN)
        {
            battlePanel.UpdateBattleText("You won. Bitch.");
        }

        else if (battleState == BattleState.LOST)
        {
            battlePanel.UpdateBattleText("You lost, you little bitch.");
        }

        yield break;
    }

}
