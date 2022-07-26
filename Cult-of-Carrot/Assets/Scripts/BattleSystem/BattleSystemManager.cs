using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private GameObject[] enemies;
    private GameObject player;

    public Transform[] enemyBattlePositions;
    public Transform playerBattlePosition;

    public CharacterStatus[] enemiesStatus;
    public CharacterStatus playerStatus;

    private PlayerUnit playerUnit;
    private EnemyUnit[] enemyUnits;

    private BattleState battleState;

    private bool playerHasClicked = true;
    private int enemiesRemaining;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(enemiesStatus.Length == enemyBattlePositions.Length); 
        enemies = new GameObject[enemiesStatus.Length];
        enemyUnits = new EnemyUnit[enemiesStatus.Length];
        enemiesRemaining = enemiesStatus.Length;
        battleState = BattleState.START;
        StartCoroutine(BeginBattle());
    }

    IEnumerator BeginBattle()
    {
        // spawn characters on battle stations
        // players
        player = Instantiate(playerStatus.characterGameObject, playerBattlePosition);
        player.SetActive(true);
        playerUnit = player.GetComponent<PlayerUnit>();
        
        // enemies
        for (int i = 0; i < enemiesStatus.Length; i++)
        {
            enemies[i] = Instantiate(enemiesStatus[i].characterGameObject, enemyBattlePositions[i]);
            enemies[i].SetActive(true);
            enemyUnits[i] = enemies[i].GetComponent<EnemyUnit>();
        }

        yield return new WaitForSeconds(3);

        battleState = BattleState.PLAYERTURN;
        
        yield return StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        // Debug.Log("Player Turn");
        battleText.text = "Player's Turn";
        yield return new WaitForSeconds(2);
        playerHasClicked = false;
    }

    public void OnAttackButtonPress()
    {
        if (battleState != BattleState.PLAYERTURN) return;

        if (!playerHasClicked)
        {
            StartCoroutine(PlayerAttack());
            playerHasClicked = true;
        }
    }

    IEnumerator PlayerAttack()
    {
        battleText.text = "CONVERT THOSE BITCHES. BITCH.";

        // Let all enemies take equal damage FOR NOW
        foreach (EnemyUnit e in enemyUnits)
        {
            e.TakeDamage(20);
            if (e.currentFaith <= 0) enemiesRemaining--;
        }

        yield return new WaitForSeconds(2);

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

        yield return null;
    }

}
