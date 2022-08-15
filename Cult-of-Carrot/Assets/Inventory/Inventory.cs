using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory", order = 0)]
public class Inventory : ScriptableObject
{
    public Consumables[] consumablesList;
	public Equipment[] equipmentList;
	public Weapons[] weaponList;
	public CharacterStats playerStats;

	// 
}
