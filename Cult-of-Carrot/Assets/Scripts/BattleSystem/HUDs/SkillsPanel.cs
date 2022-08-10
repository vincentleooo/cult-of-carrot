using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsPanel : MonoBehaviour
{
    public CharacterStats playerStats;
    public GameObject buttonPrefab;

    private List<GameObject> skillButtons;
    private int buttonWidth = 100;
    private int buttonSpacing = 10;

    // Start is called before the first frame update
    void Start()
    {
        CreateButtons();
    }

    private void CreateButtons()
    {
        List<Skill> playerSkills = playerStats.Skills;
        skillButtons = new List<GameObject>();
        int xPosition = buttonSpacing + (buttonWidth / 2);
        float panelWidth = buttonSpacing;

        for (int i = 0; i < playerSkills.Count; i++)
        {
            GameObject buttonGameObject = Instantiate(buttonPrefab, new Vector3(xPosition, 0, 0), Quaternion.identity);
            
            buttonGameObject.transform.SetParent(GetComponent<RectTransform>(), false);

            Skill skill = playerSkills[i];
            SkillButton skillButton = buttonGameObject.GetComponent<SkillButton>();
            skillButton.SetSkill(skill);
            skillButton.SetText(skill.skillName);

            // Set tooltip text
            TooltipManager tooltip = buttonGameObject.GetComponent<TooltipManager>();
            tooltip.SetTooltipText(skill.skillDescription);

            skillButtons.Add(buttonGameObject);
            xPosition += buttonSpacing + buttonWidth;
            panelWidth += buttonSpacing + buttonWidth;
        }

        // Resize skills panel to fit all skill buttons
        RectTransform rt = GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, panelWidth);

        DisableSkillButtons();
    }

    public void SetCurrentTurn(int currentTurn)
    {
        foreach (GameObject btn in skillButtons)
        {
            btn.GetComponent<SkillButton>().SetCurrentTurn(currentTurn);
        }
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
