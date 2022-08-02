using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsPanel : MonoBehaviour
{
    public CharacterStats playerStats;
    public GameObject buttonPrefab;
    public AttackEvent attackEvent;

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
        Skills[] playerSkills = playerStats.Skills;
        skillButtons = new List<GameObject>();
        int xPosition = buttonSpacing + (buttonWidth / 2);
        float panelWidth = buttonSpacing;

        for (int i = 0; i < playerSkills.Length; i++)
        {
            GameObject buttonGameObject = Instantiate(buttonPrefab, new Vector3(xPosition, 0, 0), Quaternion.identity);
            
            buttonGameObject.transform.SetParent(GetComponent<RectTransform>(), false);
            buttonGameObject.GetComponentInChildren<Text>().text = playerSkills[i].skillName;

            Skills skill = playerSkills[i];
            buttonGameObject.GetComponent<Button>().onClick.AddListener(() => {
                attackEvent.Raise(skill);
            });
            skillButtons.Add(buttonGameObject);
            xPosition += buttonSpacing + buttonWidth;
            panelWidth += buttonSpacing + buttonWidth;
        }

        // Resize skills panel to fit all skill buttons
        RectTransform rt = GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, panelWidth);
    }

}
