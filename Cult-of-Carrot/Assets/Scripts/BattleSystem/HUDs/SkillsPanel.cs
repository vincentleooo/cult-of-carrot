using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsPanel : MonoBehaviour
{
    public CharacterStats playerStats;
    public GameObject buttonPrefab;

    private List<RectTransform> buttonTranforms;
    private List<GameObject> skillButtons;

    void Start()
    {
        CreateButtons();
    }

    private void CreateButtons()
    {
        List<Skill> playerSkills = playerStats.Skills;

        // Includes parent component. Add 1 to index when referencing from list
        buttonTranforms = new List<RectTransform>(GetComponentsInChildren<RectTransform>());
        skillButtons = new List<GameObject>();

        for (int i = 0; i < playerSkills.Count; i++)
        {
            Transform buttonPosition = buttonTranforms[i+1].transform;
            GameObject buttonGameObject = Instantiate(buttonPrefab, buttonPosition);

            Skill skill = playerSkills[i];
            SkillButton skillButton = buttonGameObject.GetComponent<SkillButton>();
            skillButton.SetSkill(skill);

            skillButtons.Add(buttonGameObject);
        }

        DisableSkillButtons();
    }

    public void DisableSkillButtons()
    {
        foreach (GameObject btn in skillButtons)
        {
            btn.GetComponent<Button>().interactable = false;
        }
    }

    public void EnableSkillButtons()
    {
        foreach (GameObject btn in skillButtons)
        {
            btn.GetComponent<Button>().interactable = true;
        }
    }

}
