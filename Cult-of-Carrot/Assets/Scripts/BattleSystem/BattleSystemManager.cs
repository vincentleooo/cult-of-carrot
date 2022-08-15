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

	// when battle ends
    public GameObject WinButton;
    public GameObject LoseButton;

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

    private int enemiesRemaining;
    private int currentTurn;

    private GetMouseClick getMouseClick;

	private int currentCharacterIndex = 0;

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
        playerUnit.UpdateStatusEffect();
        playerUnit.SetCurrentTurn(currentTurn);

        if (playerUnit.TurnIsBlocked())
        {
            yield return StartCoroutine(EnemiesAttack());
        }
        else
        {
            battlePanel.UpdateBattleText("Player's Turn. Carrot be with you.");
            skillsPanel.EnableSkillButtons();
            yield return null;
        }        
    }

    public void PlayerAttackChosen(Skill skill)
    {
        if (battleState != BattleState.PLAYERTURN) return;

        // Check if player has a target
        bool enemySelected = false;
        foreach (EnemyUnit e in enemyUnits)
        {
            if (!e.IsDefeated() && e.isSelected) enemySelected = true;
        }

        if (!playerUnit.isSelected && !enemySelected && !skill.isSingleTarget)
        {
            battlePanel.UpdateBattleText("No target selected!");
            return;
        }
        else 
		if (!playerUnit.isSelected && !skill.isEnemyCast)
        {
            battlePanel.UpdateBattleText("No target selected! Select yourself for self-cast skills.");
            return;
        }
        else if (!enemySelected && skill.isEnemyCast)
        {
            battlePanel.UpdateBattleText("No target selected! Select one enemy to attack.");
            return;
        }

        if (playerUnit.CanCastSkill(skill))
        {
            StartCoroutine(PlayerSkillAttack(skill));
            skillsPanel.DisableSkillButtons();

        }
        else
        {
            int skillTurnsLeft = playerUnit.GetSkillTurnsLeft(skill.skillName);
            battlePanel.UpdateBattleText("Cannot cast " + skill.skillName + " for the next " + skillTurnsLeft.ToString() + " turns.");
        }
    }

    IEnumerator PlayerSkillAttack(Skill skill)
    {

        battlePanel.UpdateBattleText("Player used " + skill.skillName + "!");

        // Single target, cast on Enemy skills
        if (skill.isSingleTarget && skill.isEnemyCast)
        {
            // EnemyUnit targetEnemy = enemyUnits[getMouseClick.enemyUnitIndex];

			foreach (EnemyUnit e in enemyUnits)
			{
				if (e.isSelected)
				{
					playerSkillAnim = Instantiate(skill.skillAnim, playerBattlePosition);
					playerSkillAnim.SetActive(true);
					
					e.TakeDamage(skill, playerUnit.GetCharacterPower());
					if (e.IsDefeated()) enemiesRemaining--;

					yield return new WaitForSeconds(1);
					yield return null;
				}
			}

            // playerSkillAnim = Instantiate(skill.skillAnim, enemyBattlePositions[getMouseClick.enemyUnitIndex]);
            
        }

        // Single target, cast on Self skills
        if (skill.isSingleTarget && !skill.isEnemyCast)
        {
            if (playerUnit.isSelected)
            {
                Debug.Log("Casting " + skill.skillName + " on self");

                playerSkillAnim = Instantiate(skill.skillAnim, playerBattlePosition);
                playerSkillAnim.SetActive(true);

                playerUnit.CastOnSelf(skill);
                yield return new WaitForSeconds(1);
                // StartCoroutine(CheckPlayerDeath());
            }
        }

        // Multi target, cast on Enemy skills
        if (!skill.isSingleTarget)
        {
            foreach (EnemyUnit e in enemyUnits)
            {
				playerSkillAnim = Instantiate(skill.skillAnim, playerBattlePosition);
                playerSkillAnim.SetActive(true);
                e.TakeDamage(skill, playerUnit.GetCharacterPower(), enemiesRemaining);
                if (e.IsDefeated())
                {
                    enemiesRemaining--;
                }
                yield return new WaitForSeconds(1);
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
			if (enemyUnits[i].IsDefeated()) continue;
            Skill enemySkill = enemyUnits[i].SelectAttack();

            enemySkillAnims[i] = Instantiate(enemySkill.skillAnim, enemyBattlePositions[i]);
            enemySkillAnims[i].SetActive(true);

            // battlePanel.UpdateBattleText("Enemy used " + enemySkill.skillName + "!");
            // playerUnit.TakeDamage(enemySkill, enemyUnits[i].GetCharacterPower());

            // currentCharacterIndex++;

            // yield return new WaitForSeconds(1);
            // StartCoroutine(CheckPlayerDeath());
		}

        foreach (EnemyUnit e in enemyUnits)
        {
            e.UpdateStatusEffect();
			if (e.IsDefeated()) continue;
            if (!e.TurnIsBlocked() && !e.IsDefeated())
            {
                Skill enemySkill = e.SelectAttack();
                battlePanel.UpdateBattleText("Enemy used " + enemySkill.skillName + "!");
                playerUnit.TakeDamage(enemySkill, e.GetCharacterPower());

                yield return new WaitForSeconds(1);
                // StartCoroutine(CheckPlayerDeath());

                if (playerUnit.IsDefeated())
                {
                    battleState = BattleState.LOST;

                    yield return StartCoroutine(EndBattle());
                    yield break;
                }
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
            battlePanel.UpdateBattleText("You lost. Enjoy the gulag.");
        }

        yield break;
    }

}
