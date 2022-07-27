using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    private int maxFaith;
    private int maxPower;
    private int maxDef;
    public int currentFaith;
    public int currentPower;
    public int currentDef;
    private FaithBar faithBar;
    private PowerBar powerBar;
    private DefBar defBar;
    private Skills[] skills;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeStats(int faithChange, int pwrChange, int defChange)
    {
        currentFaith += faithChange;
        faithBar.SetFaith(currentFaith);

        currentPower += pwrChange;
        powerBar.SetPower(currentPower);

        currentDef += defChange;
        defBar.SetDef(currentDef);
    }

    // unique to each enemy type
    public void TakeDamage(int faithDamage, int pwrDamage, int defDamage)
    {
        currentFaith -= faithDamage;
        faithBar.SetFaith(currentFaith);

        currentPower -= pwrDamage;
        powerBar.SetPower(currentPower);

        currentDef -= defDamage;
        defBar.SetDef(currentDef);
    }

	public void SetStats(int faith, int power, int def)
	{
		faithBar = (GetComponentInChildren(typeof(FaithBar))) as FaithBar;
        powerBar = (GetComponentInChildren(typeof(PowerBar))) as PowerBar;
        defBar = (GetComponentInChildren(typeof(DefBar))) as DefBar;
		
		maxFaith = faith;
		maxPower = power;
		maxDef = def;

		currentFaith = maxFaith;
		currentPower = maxPower;
		currentDef = maxDef;
        faithBar.SetMaxFaith(maxFaith);
        powerBar.SetMaxPower(maxPower);
        defBar.SetMaxDef(maxDef);
	}

    public void SetSkills(Skills[] enemySkills)
    {
        skills = enemySkills;
    }
	
	public int Attack() {
        return 1;
    }
}
