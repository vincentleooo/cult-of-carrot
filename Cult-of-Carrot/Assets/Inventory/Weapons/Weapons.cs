using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapons", menuName = "Scriptable Objects/Weapons", order = 5)]
public class Weapons : ScriptableObject
{
	public int weaponID;
	public float price;
	private int _quantity = 0;
	public GameObject prefab;
	public GameObject prefabInventory;

	public void SetQuantity(int newQuantity)
	{
		_quantity = newQuantity;
	}

	public int Quantity()
	{
		return _quantity;
	}
    public string weaponName;
    [Multiline] public string weaponDescription;
    public float changeFaith;
	public float changePower;
	public float changeDef;
	public WeaponSkills weaponSkill;
    public bool isEquipped; // Not sure if we need this yet
}
