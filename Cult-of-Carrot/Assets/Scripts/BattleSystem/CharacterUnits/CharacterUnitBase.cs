using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnitBase : MonoBehaviour
{
    [SerializeField] private CharacterStats characterStats;
    [SerializeField] private HUDBar powerBar;
    [SerializeField] private HUDBar faithBar;
    [SerializeField] private HUDBar defBar;

    private int maxFaith;
    private int maxPower;
    private int maxDef;

    private int currentPower;
    private int currentFaith;
    private int currentDef;

    // Start is called before the first frame update
    void Start()
    {
        maxFaith = characterStats.Faith;
		maxPower = characterStats.Power;
		maxDef = characterStats.Defence;

        faithBar.SetMaxValue(maxFaith);
        powerBar.SetMaxValue(maxPower);
        defBar.SetMaxValue(maxDef);

        SetStats(characterStats.Faith, characterStats.Power, characterStats.Defence);

    }

    private void SetStats(int faith, int power, int def)
	{
		currentFaith = faith;
		currentPower = power;
		currentDef = def;

        faithBar.SetValue(faith);
        powerBar.SetValue(power);
        defBar.SetValue(def);
	}

    public void TakeDamage(int faithDamage, int pwrDamage, int defDamage)
    {
        currentFaith -= faithDamage;
        faithBar.SetValue(currentFaith);

        currentPower -= pwrDamage;
        powerBar.SetValue(currentPower);

        currentDef -= defDamage;
        defBar.SetValue(currentDef);
    }

    public bool IsDefeated()
    {
        return currentFaith <= 0;
    }

}
