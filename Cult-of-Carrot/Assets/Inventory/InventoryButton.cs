using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour
{
	private Transform childObj;
	public TextMeshProUGUI faithText;
	public TextMeshProUGUI powerText;
	public TextMeshProUGUI defText;
	private GameObject newPrefab;
	public Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        childObj = transform.Find("Inventory");
		childObj.gameObject.SetActive(false);
    }

	public void Clicked()
	{
		childObj.gameObject.SetActive(true);
		foreach (Consumables consumables in inventory.consumablesList)
		{
			if (consumables.Quantity() > 0)
			{
				newPrefab = Instantiate<GameObject>(consumables.prefabInventory);
				newPrefab.transform.SetParent(GameObject.Find("InventoryConsumables").transform, false);
			}
		}
		foreach (Weapons weapons in inventory.weaponList)
		{
			if (weapons.Quantity() > 0)
			{
				newPrefab = Instantiate<GameObject>(weapons.prefabInventory);
				newPrefab.transform.SetParent(GameObject.Find("InventoryWeapons").transform, false);
			}
		}
		foreach (Equipment equipment in inventory.equipmentList)
		{
			if (equipment.Quantity() > 0)
			{
				newPrefab = Instantiate<GameObject>(equipment.prefabInventory);
				newPrefab.transform.SetParent(GameObject.Find("InventoryEquipment").transform, false);
			}
		}
		foreach (var btn in GameObject.FindObjectsOfType<ShopButton>(true))
		{
			btn.canClick = false;
			btn.RemoveListeners();
		}
	}

	public void Exited()
	{
		for (int i=0; i < GameObject.Find("InventoryConsumables").transform.childCount; i++)
		{
			Destroy(GameObject.Find("InventoryConsumables").transform.GetChild(i).gameObject);
		}
		for (int i=0; i < GameObject.Find("InventoryWeapons").transform.childCount; i++)
		{
			Destroy(GameObject.Find("InventoryWeapons").transform.GetChild(i).gameObject);
		}
		for (int i=0; i < GameObject.Find("InventoryEquipment").transform.childCount; i++)
		{
			Destroy(GameObject.Find("InventoryEquipment").transform.GetChild(i).gameObject);
		}
		foreach (var btn in GameObject.FindObjectsOfType<ShopButton>(true))
		{
			btn.canClick = true;
			btn.AddListeners();
		}
		childObj.gameObject.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        faithText.text = "Faith: " + inventory.playerStats.Faith.ToString();
        powerText.text = "Power: " + inventory.playerStats.Power.ToString();
        defText.text = "Def: " + inventory.playerStats.Defence.ToString();
    }
}
