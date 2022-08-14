using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;

public class CharacterUnitBase : MonoBehaviour
{
    [HideInInspector] public bool isSelected = false;
    
    public CharacterStats characterStats;
    public HUDBar faithBar;
    public CharacterStatTooltipManager statTooltipManager;
    public EffectManager effectManager;

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

        float powerDamage = enemySkill.changePower;
        float defDamage = enemySkill.changeDef;
        float faithDamage = enemySkill.changeFaith;

        if (enemySkill.statusEffect != null)
        {
            AddStatusEffect(enemySkill.statusEffect);
            ApplyStatusEffect(enemySkill.statusEffect);

            if (enemySkill.statusEffect.doubleDamage)
            {
                powerDamage *= 2;
                defDamage *= 2;
                faithDamage *= 2;
            }
        }

        currentPower -= powerDamage / damageReducer;
        currentDef -= defDamage / damageReducer;

        float damageMultiplier = damageCasterPower - currentDef;
        if (damageMultiplier > 0)
        {
            currentFaith -= (faithDamage / damageReducer) * (1f + (0.1f * damageMultiplier));
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

        if (selfSkill.statusEffect != null)
        {
            AddStatusEffect(selfSkill.statusEffect);
            ApplyStatusEffect(selfSkill.statusEffect);
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

    public void AddStatusEffect(CharacterStatusEffect statusEffect)
    {
        statusEffects.Add(statusEffect);
        statusEffectsDurations.Add(statusEffect.numTurns);
        effectManager.AddStatusEffect(statusEffect);  
    }

    public void UpdateStatusEffect()
    {
        if (statusEffects.Count < 1) return;
        
        for (int i = 0; i < statusEffects.Count; i++)
        {
            statusEffectsDurations[i] -= 1;

            if (statusEffectsDurations[i] < 1)
            {
                ReverseStatusEffect(statusEffects[i]);
                effectManager.RemoveStatusEffect((int)statusEffects[i].effectIndex);
            }
        }

    }

    public bool IsDefeated()
    {
        isDefeated = currentFaith <= 0;

        return isDefeated;
    }

    private void ApplyStatusEffect(CharacterStatusEffect statusEffect)
    {
        Debug.Log("Applying " + statusEffect.name + " status effect");
        int enemyCast = statusEffect.castOnEnemy ? -1 : 1;

        switch (statusEffect.statusEffectType)
        {
            case StatusEffectTypeChanged.FAITH:
                currentFaith += enemyCast * statusEffect.statChangeValue;
                break;
            case StatusEffectTypeChanged.PWR:
                currentPower += enemyCast * statusEffect.statChangeValue;
                break;
            case StatusEffectTypeChanged.DEF:
                currentDef += enemyCast * statusEffect.statChangeValue;
                break;
            default:
                break;
        }
    }

    private void ReverseStatusEffect(CharacterStatusEffect statusEffect)
    {
        Debug.Log("Reversing " + statusEffect.name + " status effect");
        int enemyCast = statusEffect.castOnEnemy ? -1 : 1;

        switch (statusEffect.statusEffectType)
        {
            case StatusEffectTypeChanged.FAITH:
                currentFaith -= enemyCast * statusEffect.statChangeValue;
                break;
            case StatusEffectTypeChanged.PWR:
                currentPower -= enemyCast * statusEffect.statChangeValue;
                break;
            case StatusEffectTypeChanged.DEF:
                currentDef -= enemyCast * statusEffect.statChangeValue;
                break;
            default:
                break;
        }

        statTooltipManager.SetTooltipText(currentPower, currentDef);
    }
}
