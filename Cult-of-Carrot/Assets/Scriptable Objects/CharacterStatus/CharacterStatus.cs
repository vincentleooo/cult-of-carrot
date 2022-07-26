using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatus", menuName = "Scriptable Objects/CharacterStatus", order = 3)]
public class CharacterStatus : ScriptableObject 
{
    public string charName = "";
    public GameObject characterGameObject;
    public int node = 1;
}
