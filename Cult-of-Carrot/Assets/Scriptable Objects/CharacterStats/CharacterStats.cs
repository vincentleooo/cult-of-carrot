using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Scriptable Objects/CharacterStats", order = 0)]
public class CharacterStats : ScriptableObject 
{
    public string charName = "";
    public GameObject characterGameObject;
    public int node = 1;
    public int Faith = 30;
    public int Power = 10;
    public int Defence = 5;
}
