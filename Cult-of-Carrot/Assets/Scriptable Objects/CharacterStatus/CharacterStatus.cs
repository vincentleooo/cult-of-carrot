using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatus", menuName = "Scriptable Objects/CharacterStatus", order = 3)]
public class CharacterStatus : ScriptableObject 
{
    public string charName = "";
    public float[] position = new float[2];
    public GameObject characterGameObject;
    public int node = 1;
    public float maxFaith = 100;
    public float faith = 100;
}
