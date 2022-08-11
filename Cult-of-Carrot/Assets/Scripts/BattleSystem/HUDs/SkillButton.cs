using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public AttackEvent attackEvent;
    private Attack attack;
    private Skill skill;
    private Button button;
    private int nextTurn = 0;
    private int currentTurn = 0;

    void Start()
    {
        button = gameObject.GetComponent<Button>();
    }

    public void SetSkill(Skill skill)
    {
        this.skill = skill;
        this.attack = ScriptableObject.CreateInstance<Attack>();
        attack.skill = skill;

        if (button == null)
        {
            button = gameObject.GetComponent<Button>();
        }

        button.onClick.AddListener(() => {
            if (nextTurn <= currentTurn)
            {
                nextTurn = currentTurn + skill.cooldown;
                attack.canCast = true;
            }

            else
            {
                attack.canCast = false;
            }

            attackEvent.Raise(attack);
        });
    }

    public void SetText(string text)
    {
        gameObject.GetComponentInChildren<Text>().text = text;
    }

    public void SetCurrentTurn(int currentTurn)
    {
        this.currentTurn = currentTurn;
    }
}
