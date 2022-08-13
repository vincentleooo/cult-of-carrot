using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public void TakeDamage(float faithDamage, float pwrDamage, float defDamage, float damageCasterPower, bool doubleDamage = false)
    {
        Debug.Log(characterStats.charName + " taking damage");

        if (doubleDamage)
        {
            Debug.Log("dealing double damage");
            faithDamage *= 2;
            pwrDamage *= 2;
            defDamage *= 2;
        }

        currentPower -= pwrDamage;
        currentDef -= defDamage;

        float damageMultiplier = damageCasterPower - currentDef;
        if (damageMultiplier > 0)
        {
            currentFaith -= faithDamage * (1f + 0.1f * damageMultiplier);
        }
        else
        {
            currentFaith -= 5;
        }

        statTooltipManager.SetTooltipText(currentPower, currentDef);
        faithBar.SetValue(currentFaith);
    }

    public void CastOnSelf(float faithBoost, float pwrBoost, float defBoost, bool doubleBoost = false)
    {
        if (doubleBoost)
        {
            faithBoost *= 2;
            pwrBoost *= 2;
            defBoost *= 2;
        }

        currentPower += pwrBoost;
        currentDef += defBoost;
        currentFaith += faithBoost + (1f + 0.1f * currentPower);

        statTooltipManager.SetTooltipText(currentPower, currentDef);
        faithBar.SetValue(currentFaith);
    }

    public bool IsDefeated()
    {
        isDefeated = currentFaith <= 0;

        return isDefeated;
    }
}
