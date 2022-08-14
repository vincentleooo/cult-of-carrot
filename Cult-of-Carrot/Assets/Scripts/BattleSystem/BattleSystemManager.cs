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
    //for skill anim
    private GameObject playerSkillAnim;
    // private GameObject enemySkillAnim;
    private GameObject[] enemySkillAnims = {null,null,null};

    public Transform[] enemyBattlePositions;
    public Transform playerBattlePosition;

    public TextMeshProUGUI currentRoundText;

    private PlayerUnit playerUnit;
    [HideInInspector] public EnemyUnit[] enemyUnits;

    private BattleState battleState;

    private bool playerHasClicked = true;
    private int enemiesRemaining;
    private int currentTurn;
    private int currentCharacterIndex = 0;

    private GetMouseClick getMouseClick;

    // when battle ends
    public GameObject WinButton;
    public GameObject LoseButton;

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
        for (int i = 0; i < enemySkillAnims.Length; i++)
        {
            if (enemySkillAnims[i] != null)
            {
                Destroy(enemySkillAnims[i]);
            }
        }
        

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


        // Check if in the middle of 
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
        Debug.Log("Player used " + skill.skillName + "!");

        // Single target, cast on Enemy skills
        if (skill.isSingleTarget && skill.isEnemyCast)
        {
            EnemyUnit targetEnemy = enemyUnits[getMouseClick.enemyUnitIndex];

            // playerSkillAnim = Instantiate(skill.skillAnim, enemyBattlePositions[getMouseClick.enemyUnitIndex]);
            playerSkillAnim = Instantiate(skill.skillAnim, playerBattlePosition);
            playerSkillAnim.SetActive(true);
            

            targetEnemy.TakeDamage(skill.changeFaith, skill.changePower, skill.changeDef, playerUnit.GetCharacterPower());
            if (targetEnemy.IsDefeated()) enemiesRemaining--;
            yield return new WaitForSeconds(1);
        }

        // Single target, cast on Self skills
        if (skill.isSingleTarget && skill.isSelfCast)
        {
            if (playerUnit.isSelected)
            {
                Debug.Log("Casting " + skill.skillName + " on self");

                playerSkillAnim = Instantiate(skill.skillAnim, playerBattlePosition);
                playerSkillAnim.SetActive(true);

                playerUnit.CastOnSelf(skill.changeFaith, skill.changePower, skill.changeDef);
                yield return new WaitForSeconds(1);
                // StartCoroutine(CheckPlayerDeath());
            }
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
        Destroy(playerSkillAnim);

        battlePanel.UpdateBattleText("Enemy's turn. You better start praying.");

        yield return new WaitForSeconds(2);

        currentCharacterIndex = 1; // Reset it each time


        for (int i = 0; i < enemyUnits.Length; i++)
        {
            Skill enemySkill = enemyUnits[i].SelectAttack();


            enemySkillAnims[i] = Instantiate(enemySkill.skillAnim, enemyBattlePositions[i]);
            enemySkillAnims[i].SetActive(true);

            battlePanel.UpdateBattleText("Enemy used " + enemySkill.skillName + "!");
            playerUnit.TakeDamage(enemySkill.changeFaith, enemySkill.changePower, enemySkill.changeDef, enemyUnits[i].GetCharacterPower());
            currentCharacterIndex++;

            yield return new WaitForSeconds(1);
            // StartCoroutine(CheckPlayerDeath());

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

    IEnumerator CheckPlayerDeath()
    {
        if (playerUnit.IsDefeated())
        {
            battleState = BattleState.LOST;

            yield return StartCoroutine(EndBattle());
            yield break;
        }
    }

    IEnumerator EndBattle()
    {
        for (int i = 0; i < enemySkillAnims.Length; i++)
        {
            if (enemySkillAnims[i] != null)
            {
                Destroy(enemySkillAnims[i]);
            }
        }
        Destroy(playerSkillAnim);
        
        if (battleState == BattleState.WIN)
        {
            WinButton.gameObject.SetActive(true);
            battlePanel.UpdateBattleText("You won. You have made their lives better.");
        }

        else if (battleState == BattleState.LOST)
        {
            LoseButton.gameObject.SetActive(true);
            battlePanel.UpdateBattleText("You lost. Enjoy the gulag");
        }

        yield break;
    }


}
