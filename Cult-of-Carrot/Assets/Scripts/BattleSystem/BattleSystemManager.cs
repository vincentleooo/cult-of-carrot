using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

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

    public TextMeshProUGUI currentRoundText;

    public PlayerUnit playerUnit;
    public EnemyUnit[] enemyUnits;

    private BattleState battleState;

    private bool playerHasClicked = true;
    private int enemiesRemaining;
    private int currentTurn;

    [SerializeField]
    private CharacterStats[] currentCharacters;
    private int currentCharacterIndex = 0;

    private GetMouseClick getMouseClick;

    void Start()
    {
        enemyUnits = new EnemyUnit[enemyBattlePositions.Length];
        enemiesRemaining = enemyBattlePositions.Length;
        battleState = BattleState.START;
        currentTurn = 1;

        string currentRound = "Round: " + currentTurn.ToString();
        currentRoundText.text = currentRound;

        getMouseClick = GetComponent<GetMouseClick>();

        StartCoroutine(BeginBattle());
    }

    IEnumerator BeginBattle()
    {
        // spawn player on battle stations
        playerPrefab = Instantiate(playerPrefab, playerBattlePosition);

        // set player name
        var characterName = playerPrefab.GetComponentInChildren<TextMeshPro>();
        characterName.text = currentCharacters[currentCharacterIndex].charName;

        playerPrefab.SetActive(true);
        playerUnit = playerPrefab.GetComponent<PlayerUnit>();
        
        // spawn enemies on battle stations
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            enemyPrefabs[i] = Instantiate(enemyPrefabs[i], enemyBattlePositions[i]);

            // set enemy names
            var characterNames = enemyPrefabs[i].GetComponentInChildren<TextMeshPro>();
            characterNames.text = currentCharacters[i + 1].charName;

            enemyPrefabs[i].SetActive(true);
            enemyUnits[i] = enemyPrefabs[i].GetComponent<EnemyUnit>();
        }

        yield return new WaitForSeconds(3);

        battleState = BattleState.PLAYERTURN;
        
        yield return StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        string currentRound = "Round: " + currentTurn.ToString();
        currentRoundText.text = currentRound;

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
            battlePanel.UpdateBattleText("Cannot cast " + attack.skill.skillName + " during cooldown.");
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
        battlePanel.UpdateBattleText("Player used " + skill.skillName + "!");

        float perEnemyFaithDamage = skill.changeFaith; // enemiesRemaining;
        float perEnemyPwrDamage = skill.changePower; // enemiesRemaining;
        float perEnemyDefDamage = skill.changeDef; // enemiesRemaining;

        currentCharacterIndex = 1; // Reset it each time

        // Single target skills, cast on Enemy
        if (skill.isSingleTarget && skill.isEnemyCast)
        {
            foreach (EnemyUnit e in enemyUnits)
            {
                if (e.isSelected)
                {
                    e.TakeDamage(perEnemyFaithDamage, perEnemyPwrDamage, perEnemyDefDamage, currentCharacters[0], currentCharacters[getMouseClick.enemyUnitIndex + 1], getMouseClick.enemyUnitIndex + 1);
                    if (e.IsDefeated()) enemiesRemaining--;
                    yield return new WaitForSeconds(1);
                    currentCharacterIndex++;
                }
            }
        }

        // Single target skills, cast on Self
        if (skill.isSingleTarget && skill.isSelfCast)
        {
            // smth smth
        }

        // foreach (EnemyUnit e in enemyUnits)
        // {
        //     e.TakeDamage(perEnemyFaithDamage, perEnemyPwrDamage, perEnemyDefDamage, currentCharacters[0], currentCharacters[currentCharacterIndex], currentCharacterIndex);
        //     if (e.IsDefeated()) enemiesRemaining--;
        //     yield return new WaitForSeconds(1);
        //     currentCharacterIndex++;
        // }

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

        currentCharacterIndex = 1; // Reset it each time

        foreach (EnemyUnit e in enemyUnits)
        {
            Skill enemySkill = e.SelectAttack();
            battlePanel.UpdateBattleText("Enemy used " + enemySkill.skillName + "!");
            playerUnit.TakeDamage(enemySkill.changeFaith, enemySkill.changePower, enemySkill.changeDef, currentCharacters[currentCharacterIndex], currentCharacters[0], 0);
            currentCharacterIndex++;

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
            battlePanel.UpdateBattleText("You won. You have made their lives better.");
        }

        else if (battleState == BattleState.LOST)
        {
            battlePanel.UpdateBattleText("You lost, you little bitch.");
        }

        yield break;
    }

}
