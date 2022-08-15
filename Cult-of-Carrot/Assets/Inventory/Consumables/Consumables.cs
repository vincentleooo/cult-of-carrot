using UnityEngine;

[CreateAssetMenu(fileName = "Consumables", menuName = "Scriptable Objects/Consumables", order = 0)]
public class Consumables : ScriptableObject
{
	public int consumableID;
	public string consumableName;
	public float price;
	private int _quantity = 0;
	public GameObject prefab;
	public GameObject prefabInventory;
	[Multiline] public string consumableDesc = "";
	public float changeFaith;
	public float changePower;
	public float changeDef;
    public int numTurns; // Not sure if we need this yet
    public StatusEffectTypeChanged[] statusEffectType; // Not sure if we need this yet

	public void SetQuantity(int newQuantity)
	{
		_quantity = newQuantity;
	}

	public int Quantity()
	{
		return _quantity;
	}
}
