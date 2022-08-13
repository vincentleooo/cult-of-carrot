using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;

public class CharacterUnitBase : MonoBehaviour
{
    [HideInInspector] public bool isSelected;
    
    [SerializeField] protected CharacterStats characterStats;
    [SerializeField] private HUDBar faithBar;
    [SerializeField] private CharacterStatTooltipManager statTooltipManager;

    private float maxFaith;
    private float maxPower;
    private float maxDef;

    private float currentPower;
    private float currentFaith;
    private float currentDef;
    private bool isDefeated = false;

    protected List<CharacterStatusEffect> statusEffects = new List<CharacterStatusEffect>();
    protected List<int> statusEffectsDurations = new List<int>();

    private Animator characterAnimator;

    protected void Start()
    {
        maxFaith = characterStats.Faith;
		maxPower = characterStats.Power;
		maxDef = characterStats.Defence;

        currentFaith = maxFaith;
		currentPower = maxPower;
		currentDef = maxPower;

        faithBar.SetMaxValue(maxFaith);
        faithBar.SetValue(maxFaith);

        gameObject.GetComponentInChildren<TextMeshPro>().text = characterStats.charName;
        statTooltipManager.SetTooltipText(maxPower, maxDef);

        characterAnimator = GetComponent<Animator>();
    }

    public float GetCharacterPower()
    {
        return currentPower;
    }

    public void TakeDamage(Skill enemySkill, float damageCasterPower, int damageReducer = 1)
    {
        Debug.Log(characterStats.charName + " taking damage");

        // if (doubleDamage)
        // {
        //     Debug.Log("dealing double damage");
        //     faithDamage *= 2;
        //     pwrDamage *= 2;
        //     defDamage *= 2;
        // }

        if (enemySkill.statusEffects.Length > 0)
        {
            AddStatusEffects(enemySkill.statusEffects, enemySkill.statusEffectsDurations);
        }

        currentPower -= enemySkill.changePower / damageReducer;
        currentDef -= enemySkill.changeDef / damageReducer;

        float damageMultiplier = damageCasterPower - currentDef;
        if (damageMultiplier > 0)
        {
            currentFaith -= (enemySkill.changeFaith / damageReducer) * (1f + (0.1f * damageMultiplier));
        }
        else
        {
            currentFaith -= 5;
        }

        statTooltipManager.SetTooltipText(currentPower, currentDef);
        faithBar.SetValue(currentFaith);
    }

    public void CastOnSelf(Skill selfSkill)
    {
        // if (doubleBoost)
        // {
        //     faithBoost *= 2;
        //     pwrBoost *= 2;
        //     defBoost *= 2;
        // }

        if (selfSkill.statusEffects.Length > 0)
        {
            // print(selfSkill.statusEffects.ToString());
            AddStatusEffects(selfSkill.statusEffects, selfSkill.statusEffectsDurations);
        }

        currentPower += selfSkill.changePower;
        currentDef += selfSkill.changeDef;
        currentFaith += selfSkill.changeFaith + (1f + 0.1f * currentPower);

        statTooltipManager.SetTooltipText(currentPower, currentDef);
        faithBar.SetValue(currentFaith);
    }

    public bool TurnIsBlocked()
    {
        if (statusEffects.Count == 0) return false;

        return (statusEffects[0].statusEffectType == StatusEffectTypeChanged.BLOCKTURN)
               && (statusEffectsDurations[0] > 0);
    }

    public void AddStatusEffects(CharacterStatusEffect[] skillStatusEffects, int[] durations)
    {
        Assert.AreEqual(skillStatusEffects.Length, durations.Length);

        for (int i = 0; i < skillStatusEffects.Length; i++)
        {
            statusEffects.Add(skillStatusEffects[i]);
            statusEffectsDurations.Add(durations[i] - 1);  
        }

        ApplyStatusEffect();
    }

    public void UpdateStatusEffect()
    {
        if (statusEffects.Count < 1) return;

        // Decrement status effect timer at start of list
        statusEffectsDurations[0] -= 1;

        // Remove status effects when duration is over
        if (statusEffectsDurations[0] < 1)
        {
            ReverseStatusEffect();
            statusEffects.RemoveAt(0);
            statusEffectsDurations.RemoveAt(0);
        }

    }

    public bool IsDefeated()
    {
        isDefeated = currentFaith <= 0;

        return isDefeated;
    }

    private void ApplyStatusEffect()
    {
        CharacterStatusEffect statusEffectApplied = statusEffects[0];
        Debug.Log("Applying " + statusEffectApplied.name + " status effect");
        int enemyCast = statusEffectApplied.castOnEnemy ? -1 : 1;

        switch (statusEffectApplied.statusEffectType)
        {
            case StatusEffectTypeChanged.FAITH:
                currentFaith += enemyCast * statusEffectApplied.statChangeValue;
                break;
            case StatusEffectTypeChanged.PWR:
                currentPower += enemyCast * statusEffectApplied.statChangeValue;
                break;
            case StatusEffectTypeChanged.DEF:
                currentDef += enemyCast * statusEffectApplied.statChangeValue;
                break;
            default:
                break;
        }
    }

    private void ReverseStatusEffect()
    {
        CharacterStatusEffect statusEffectApplied = statusEffects[0];
        Debug.Log("Removing " + statusEffectApplied.name + " status effect");
        int enemyCast = statusEffectApplied.castOnEnemy ? -1 : 1;

        switch (statusEffectApplied.statusEffectType)
        {
            case StatusEffectTypeChanged.FAITH:
                currentFaith -= enemyCast * statusEffectApplied.statChangeValue;
                break;
            case StatusEffectTypeChanged.PWR:
                currentPower -= enemyCast * statusEffectApplied.statChangeValue;
                break;
            case StatusEffectTypeChanged.DEF:
                currentDef -= enemyCast * statusEffectApplied.statChangeValue;
                break;
            default:
                break;
        }
    }
}
