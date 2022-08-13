using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private float getCurrentCharacterFaith;
    private float getCurrentCharacterPower;
    private float getCurrentCharacterDef;
    private float characterToAttackFaith;
    private float characterToAttackPower;
    private float characterToAttackDef;
    private GameObject[] allFaithBars;
    private GameObject[] allPowerBars;
    private GameObject[] allDefBars;
    private GameObject[] allFaithNumbers;
    private int allFaithNumbersIndex = 0;
    private GameObject[] allMaxFaithNumbers;
    private int allMaxFaithNumbersIndex = 0;

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

        allFaithBars = GameObject.FindGameObjectsWithTag("FaithBar");
        allPowerBars = GameObject.FindGameObjectsWithTag("PowerBar");
        allDefBars = GameObject.FindGameObjectsWithTag("DefBar");

        allFaithNumbers = GameObject.FindGameObjectsWithTag("FaithNumber");
        allMaxFaithNumbers = GameObject.FindGameObjectsWithTag("MaxFaithNumber");
        SetAllCurrentFaith();
        SetAllMaxFaith();
    }

    void Update()
    {
        characterAnimator.SetBool("isDefeated", isDefeated);
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
        
        print("charToAttack => f: " + characterToAttackFaith + "; p: " + characterToAttackPower + "; d: " + characterToAttackDef);
    }

    public void TakeDamage(float faithDamage, float pwrDamage, float defDamage, CharacterStats character, CharacterStats characterBeingAttacked, int characterBeingAttackedIndex, bool doubleDamage = false)
    {
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

        statTooltipManager.SetTooltipText(currentPower, currentDef);

        faithBar.SetValue(characterToAttackFaith);
        // powerBar.SetValue(characterToAttackPower);
        // defBar.SetValue(characterToAttackDef);
        SetAllCurrentFaith();
    }

    private void SetAllMaxFaith()
    {
        allMaxFaithNumbersIndex = 0; // Reset it each time

        // set the max Faith values for all the characters' Faith bars
        foreach (var gameObjects in allMaxFaithNumbers)
        {
            var gameObjectText = gameObjects.GetComponent<TextMeshProUGUI>();
            gameObjectText.text = "/" + allFaithBars[allMaxFaithNumbersIndex].GetComponent<Slider>().value.ToString();
            allMaxFaithNumbersIndex++;
        }
    }

    private void SetAllCurrentFaith()
    {
        allFaithNumbersIndex = 0; // Reset it each time

        // update the current Faith values for all the characters' Faith bars
        foreach (var gameObjects in allFaithNumbers)
        {
            var gameObjectText = gameObjects.GetComponent<TextMeshProUGUI>();
            gameObjectText.text = allFaithBars[allFaithNumbersIndex].GetComponent<Slider>().value.ToString();
            allFaithNumbersIndex++;
        }
    }

    public bool IsDefeated()
    {
        // Not sure which condition is triggering the death (didn't check) but just use all lol
        if (currentFaith <= 0 || getCurrentCharacterFaith <= 0 || characterToAttackFaith <= 0)
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
