using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnitBase : MonoBehaviour
{
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

    private  Animator characterAnimator;

    // Start is called before the first frame update
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

        statTooltipManager.SetTooltipText(maxPower, maxDef);

        characterAnimator = GetComponent<Animator>();
    }

    // void Update()
    // {
    //     characterAnimator.SetBool("isDefeated", isDefeated);
    // }

    public void TakeDamage(float faithDamage, float pwrDamage, float defDamage, bool doubleDamage = false)
    {
        if (doubleDamage)
        {
            faithDamage *= 2;
            pwrDamage *= 2;
            defDamage *= 2;
        }

        currentPower -= pwrDamage;
        currentDef -= defDamage;
        currentFaith -= faithDamage * (1f + 0.1f * (currentPower - currentDef));

        faithBar.SetValue(currentFaith);
        statTooltipManager.SetTooltipText(currentPower, currentDef);
    }

    public bool IsDefeated()
    {
        isDefeated = currentFaith <= 0;
        return isDefeated;
    }

}
