using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    HELMET,
    ARMOR,
    BOOTS,
    GLOVE,
    ACCESSORY
}

[CreateAssetMenu(fileName = "Equipment", menuName = "Scriptable Objects/Equipment", order = 5)]
public class Equipment : ScriptableObject
{
	public int equipmentID;
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
    public string equipmentName;
    [Multiline] public string equipmentDescription;
    public float changeFaith;
	public float changePower;
	public float changeDef;
    public bool isEquipped; // Not sure if we need this yet
    public EquipmentType equipmentType;
}
