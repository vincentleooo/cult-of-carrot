using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomAttackEvent : UnityEvent<Attack>
{

}

public class AttackEventListener : MonoBehaviour
{
    public AttackEvent Event;
    public CustomAttackEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(Attack attack)
    {
        Response.Invoke(attack);
    }
}
