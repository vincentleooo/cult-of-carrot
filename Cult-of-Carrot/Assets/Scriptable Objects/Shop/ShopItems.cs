using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItems", menuName = "Scriptable Objects/ShopItems", order = 0)]
public class ShopItems : ScriptableObject
{
    public string itemName = "";
	public float price;
	public int quantity;
	public GameObject prefab;
	[Multiline] public string itemDesc = "";
}
