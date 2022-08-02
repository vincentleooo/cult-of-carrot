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
    public Transform[] skillButtonPositions;
    public Button[] skillButtons;

    public GameObject[] enemyPrefabs;
    public GameObject playerPrefab;

    public Transform[] enemyBattlePositions;
    public Transform playerBattlePosition;

    private PlayerUnit playerUnit;
    private EnemyUnit[] enemyUnits;

    private BattleState battleState;

    private bool playerHasClicked = true;
    private int enemiesRemaining;

    // Start is called before the first frame update
    void Start()
    {
        enemyUnits = new EnemyUnit[enemyBattlePositions.Length];
        enemiesRemaining = enemyBattlePositions.Length;
        battleState = BattleState.START;
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
        battlePanel.UpdateBattleText("Player's Turn. Carrot be with you.");
        playerHasClicked = false;
        yield return null;
    }

    public void CharacterAttack(Skills skill)
    {
        Debug.Log(skill.skillName);
        if (battleState != BattleState.PLAYERTURN) return;

        if (!playerHasClicked)
        {
            StartCoroutine(PlayerSkillAttack(skill));
            playerHasClicked = true;
        }
    }

    IEnumerator PlayerSkillAttack(Skills skill)
    {
        battlePanel.UpdateBattleText("Player used " + skill.skillName);

        int perEnemyFaithDamage = skill.changeFaith / enemiesRemaining;
        int perEnemyPwrDamage = skill.changePower / enemiesRemaining;
        int perEnemyDefDamage = skill.changeDef / enemiesRemaining;

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

        // TODO: Have enemies w diff attacks or smth
        foreach (EnemyUnit e in enemyUnits)
        {
            int playerDamage = e.Attack();
            playerUnit.TakeDamage(playerDamage, playerDamage, playerDamage);

            yield return new WaitForSeconds(1);

            if (playerUnit.IsDefeated())
            {
                battleState = BattleState.LOST;

                // TODO: Transition to end battle
                yield return StartCoroutine(EndBattle());
                yield break;
            }
        }

        yield return new WaitForSeconds(2);

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
