using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // unique to each enemy type
    public void TakeDamage(int damage)
    {
        currentFaith -= damage;
        faithBar.SetFaith(currentFaith);
        currentPower -= damage;
        powerBar.SetPower(currentPower);
        currentDef -= damage;
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

}
