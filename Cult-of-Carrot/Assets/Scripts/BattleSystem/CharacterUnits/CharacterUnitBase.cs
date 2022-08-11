using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private float getCurrentCharacterFaith;
    private float getCurrentCharacterPower;
    private float getCurrentCharacterDef;

    [SerializeField]
    private CharacterStats[] currentCharacters;

    private float characterToAttackFaith;
    private float characterToAttackPower;
    private float characterToAttackDef;

    private GameObject[] allFaithBars;
    private GameObject[] allPowerBars;
    private GameObject[] allDefBars;

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

        allFaithBars = GameObject.FindGameObjectsWithTag("FaithBar");
        allPowerBars = GameObject.FindGameObjectsWithTag("PowerBar");
        allDefBars = GameObject.FindGameObjectsWithTag("DefBar");
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

        faithBar.SetValue(faith);
        powerBar.SetValue(power);
        defBar.SetValue(def);
	}

    private void GetCurrentCharacterStats(float faith, float power, float def)
	{
		getCurrentCharacterFaith = faith;
		getCurrentCharacterPower = power;
		getCurrentCharacterDef = def;

        print("f: " + currentFaith + "; p: " + currentPower + "; d: " + currentDef);
	}

    private void GetCharacterBeingAttackedStats(int index)
    {
        characterToAttackFaith = allFaithBars[index].GetComponent<Slider>().value;
        characterToAttackPower = allPowerBars[index].GetComponent<Slider>().value;
        characterToAttackDef = allDefBars[index].GetComponent<Slider>().value;

        // switch (index)
        // {
        //     case ("Judas Iscariot"):
        //         characterToAttackFaith = GameObject.Find("Player(Clone)").GetComponentsInChildren<Slider>().value;
        //         characterToAttackPower = GameObject.Find("Player(Clone)").GetComponentsInChildren<Slider>().value;
        //         characterToAttackDef = GameObject.Find("Player(Clone)").GetComponentInChildren<Slider>().value;
        //         break;
            
        //     case ("Gsus Cries"):
        //         characterToAttackFaith = GameObject.Find("Priest1(Clone)").GetComponentInChildren<Slider>().value;
        //         characterToAttackPower = GameObject.Find("Priest1(Clone)").GetComponentInChildren<Slider>().value;
        //         characterToAttackDef = GameObject.Find("Priest1(Clone)").GetComponentInChildren<Slider>().value;
        //         break;

        //     case ("Gsus Cries Jr."):
        //         characterToAttackFaith = GameObject.Find("Priest2(Clone)").GetComponentInChildren<Slider>().value;
        //         characterToAttackPower = GameObject.Find("Priest2(Clone)").GetComponentInChildren<Slider>().value;
        //         characterToAttackDef = GameObject.Find("Priest2(Clone)").GetComponentInChildren<Slider>().value;
        //         break;
            
        //     default:
        //         throw new System.Exception("Character does not exist!");
        // }
        
        print("charToAttack => f: " + characterToAttackFaith + "; p: " + characterToAttackPower + "; d: " + characterToAttackDef);

    }

    public void TakeDamage(float faithDamage, float pwrDamage, float defDamage, CharacterStats character, CharacterStats characterBeingAttacked, int characterBeingAttackedIndex, bool doubleDamage = false)
    {
        IsDefeated(); // Do a check

        GetCurrentCharacterStats(character.Faith, character.Power, character.Defence);
        GetCharacterBeingAttackedStats(characterBeingAttackedIndex);

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

        characterToAttackFaith -= faithDamage * (1f + 0.1f * ((currentPower) - currentDef));

        Debug.Log("faith dealt: " + faithDamage);
        Debug.Log("total faith dealt: " + faithDamage * (1f + 0.1f * ((currentPower) - currentDef)));
        print("current faith: " + characterToAttackFaith);

        faithBar.SetValue(characterToAttackFaith);
        powerBar.SetValue(characterToAttackPower);
        defBar.SetValue(characterToAttackDef);
    }

    public bool IsDefeated()
    {
        if (currentFaith <= 0 || getCurrentCharacterFaith <= 0)
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
