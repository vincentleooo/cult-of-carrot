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
    public Text battleText;
    public Transform[] skillButtonPositions;
    public Button[] skillButtons;

    private GameObject[] enemies;
    private GameObject player;

    public Transform[] enemyBattlePositions;
    public Transform playerBattlePosition;

    public CharacterStats[] enemiesStats;
    public CharacterStats playerStats;

    private PlayerUnit playerUnit;
    private EnemyUnit[] enemyUnits;

    private BattleState battleState;

    private bool playerHasClicked = true;
    private int enemiesRemaining;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(enemiesStats.Length == enemyBattlePositions.Length); 
        enemies = new GameObject[enemiesStats.Length];
        enemyUnits = new EnemyUnit[enemiesStats.Length];
        enemiesRemaining = enemiesStats.Length;
        battleState = BattleState.START;
        StartCoroutine(BeginBattle());
    }

    IEnumerator BeginBattle()
    {
        // spawn characters on battle stations
        // players
        player = Instantiate(playerStats.characterGameObject, playerBattlePosition);
        player.SetActive(true);
        playerUnit = player.GetComponent<PlayerUnit>();
        playerUnit.SetStats(playerStats.Faith, playerStats.Power, playerStats.Defence);
        
        // enemies
        for (int i = 0; i < enemiesStats.Length; i++)
        {
            enemies[i] = Instantiate(enemiesStats[i].characterGameObject, enemyBattlePositions[i]);
            enemies[i].SetActive(true);
            enemyUnits[i] = enemies[i].GetComponent<EnemyUnit>();
            enemyUnits[i].SetStats(enemiesStats[i].Faith, enemiesStats[i].Power, enemiesStats[i].Defence);
        }

        yield return new WaitForSeconds(3);

        battleState = BattleState.PLAYERTURN;
        
        yield return StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        battleText.text = "Player's Turn. Carrot be with you.";
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
        battleText.text = "Player used " + skill.skillName;

        int perEnemyFaithDamage = skill.changeFaith / enemiesRemaining;
        int perEnemyPwrDamage = skill.changePower / enemiesRemaining;
        int perEnemyDefDamage = skill.changeDef / enemiesRemaining;

        foreach (EnemyUnit e in enemyUnits)
        {
            e.ChangeStats(perEnemyFaithDamage, perEnemyPwrDamage, perEnemyDefDamage);
            if (e.currentFaith <= 0) enemiesRemaining--;
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
        battleText.text = "Enemy's turn. You better start praying.";

        // TODO: Have enemies w diff attacks or smth
        foreach (EnemyUnit e in enemyUnits)
        {
            int playerDamage = e.Attack();
            playerUnit.TakeDamage(playerDamage);

            yield return new WaitForSeconds(1);

            // Check if player is still alive
            if (playerUnit.currentFaith <= 0)
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
            battleText.text = "You won. Bitch.";
        }

        else if (battleState == BattleState.LOST)
        {
            battleText.text = "You lost, you little bitch.";
        }

        yield break;
    }

}
