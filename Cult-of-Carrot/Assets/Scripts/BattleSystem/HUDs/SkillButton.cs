using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButton : MonoBehaviour
{
    public AttackEvent attackEvent;
    private Skill skill;
    private Button button;


    void Start()
    {
        button = gameObject.GetComponent<Button>();
    }

    public void SetSkill(Skill skill)
    {
        this.skill = skill;

        if (button == null)
        {
            button = gameObject.GetComponent<Button>();
        }

        button.onClick.AddListener(() => {
            attackEvent.Raise(skill);
        });

        TMP_Text buttonTMP = gameObject.GetComponentInChildren<TMP_Text>();
        buttonTMP.SetText(skill.skillName);

        TooltipManager tooltip = gameObject.GetComponent<TooltipManager>();
        tooltip.SetTooltipText(skill.skillDescription);
    }

}
