using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackEvent", menuName = "Scriptable Objects/AttackEvent", order = 3)]
public class AttackEvent : ScriptableObject 
{
    private readonly List<AttackEventListener> eventListeners = new List<AttackEventListener>();

    public void Raise(Skill skill)
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised(skill);
    }

    public void RegisterListener(AttackEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(AttackEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}