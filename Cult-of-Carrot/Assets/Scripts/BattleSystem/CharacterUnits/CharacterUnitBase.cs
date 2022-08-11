using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnitBase : MonoBehaviour
{
    [SerializeField] protected CharacterStats characterStats;
    [SerializeField] private HUDBar powerBar;
    [SerializeField] private HUDBar faithBar;
    [SerializeField] private HUDBar defBar;

    private float maxFaith;
    private float maxPower;
    private float maxDef;

    private float currentPower;
    private float currentFaith;
    private float currentDef;
    private bool isDefeated = false;

    private  Animator characterAnimator;

    protected void Start()
    {
        maxFaith = characterStats.Faith;
		maxPower = characterStats.Power;
		maxDef = characterStats.Defence;

        faithBar.SetMaxValue(maxFaith);
        powerBar.SetMaxValue(maxPower);
        defBar.SetMaxValue(maxDef);

        SetStats(characterStats.Faith, characterStats.Power, characterStats.Defence);

        characterAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        characterAnimator.SetBool("isDefeated", isDefeated);
    }

    private void SetStats(float faith, float power, float def)
	{
		currentFaith = faith;
		currentPower = power;
		currentDef = def;

        print("f: " + currentFaith + "; p: " + currentPower + "; d: " + currentDef);

        faithBar.SetValue(faith);
        powerBar.SetValue(power);
        defBar.SetValue(def);
	}

    public void TakeDamage(float faithDamage, float pwrDamage, float defDamage, bool doubleDamage = false)
    {
        if (doubleDamage)
        {
            Debug.Log("dealing double damage");
            faithDamage *= 2;
            pwrDamage *= 2;
            defDamage *= 2;
        }

        print("currPower: " + currentPower);
        currentPower -= pwrDamage;
        print("currPower2: " + currentPower);
        print("pwrDmg: " + pwrDamage);


        print("currDef: " + currentDef);
        currentDef -= defDamage;
        print("currDef2: " + currentDef);
        print("defDmg: " + defDamage);

        currentFaith -= faithDamage * (1f + 0.1f * ((currentPower) - currentDef));

        Debug.Log("faith dealt: " + faithDamage);
        Debug.Log("total faith dealt: " + faithDamage * (1f + 0.1f * ((currentPower) - currentDef)));


        defBar.SetValue(currentDef);
        powerBar.SetValue(currentPower);
        faithBar.SetValue(currentFaith);
    }

    public bool IsDefeated()
    {
        if (currentFaith <= 0)
        {
            isDefeated = true;
        }

        else
        {
            isDefeated = false;
        }

        return isDefeated;
    }
}
