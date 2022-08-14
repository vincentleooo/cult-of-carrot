using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public enum InventoryType
{
	CONSUMABLES,
	EQUIPMENT,
	WEAPONS
}

public class ShopButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private int itemID;
	private string itemName;
	private string itemDesc;
	private float price;
	private int quantity;
	public TextMeshProUGUI priceText;
	public TextMeshProUGUI quantityText;
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI descText;
	public TextMeshProUGUI equipmentText;
	public Consumables shopItems;
	public Equipment shopEquipmentItems;
	public Weapons shopWeaponsItems;
	public InventoryType inventoryType;
	public EquipmentType equipmentType;
	private ShopManagerScript shopManager;
	public float buttonDelay = 0.1f;
	private Transform childObj;

	// IEnumerator DisableButtonTemp(Button button, float seconds)
	// {
	// 	yield return new WaitForSeconds(seconds);
	// 	button.interactable = true;
	// }

	void Start()
	{
		if (inventoryType == InventoryType.EQUIPMENT)
		{
			itemID = shopEquipmentItems.equipmentID;
			itemName = shopEquipmentItems.equipmentName;
			itemDesc = shopEquipmentItems.equipmentDescription;
			price = shopEquipmentItems.price;
			quantity = shopEquipmentItems.Quantity();
			equipmentType = shopEquipmentItems.equipmentType;
			equipmentText.text = equipmentType.ToString();
		} else if (inventoryType == InventoryType.CONSUMABLES)
		{
			itemID = shopItems.consumableID;
			itemName = shopItems.consumableName;
			itemDesc = shopItems.consumableDesc;
			price = shopItems.price;
			quantity = shopItems.Quantity();
		}
		else if (inventoryType == InventoryType.WEAPONS)
		{
			itemID = shopWeaponsItems.weaponID;
			itemName = shopWeaponsItems.weaponName;
			itemDesc = shopWeaponsItems.weaponDescription;
			price = shopWeaponsItems.price;
			quantity = shopWeaponsItems.Quantity();
		}
		
		shopManager = GameObject.Find("ShopManager").GetComponent<ShopManagerScript>();
		GetComponent<Button>().onClick.AddListener(delegate {Clicked();});
		priceText.text = "$" + price.ToString();
		nameText.text = itemName;
		descText.text = itemDesc;
		childObj = transform.Find("DescBG");
		childObj.gameObject.SetActive(false);
		quantityText.text = quantity.ToString();
	}

	public void OnPointerEnter(PointerEventData eventData)
    {
		childObj.gameObject.SetActive(true);
    }

	public void OnPointerExit(PointerEventData eventData)
    {
		childObj.gameObject.SetActive(false);
    }

	void Clicked()
	{
		shopManager.Buy();

		if (inventoryType == InventoryType.EQUIPMENT)
		{
			shopEquipmentItems.SetQuantity(quantity);
		} else if (inventoryType == InventoryType.CONSUMABLES)
		{
			shopItems.SetQuantity(quantity);
		} else if (inventoryType == InventoryType.WEAPONS)
		{
			shopWeaponsItems.SetQuantity(quantity);
		}

		quantityText.text = quantity.ToString();
	}

	public float Price()
	{
		return price;
	}

	public int Quantity()
	{
		return quantity;
	}

	public void ModifyQuantity(int differenceSigned)
	{
		quantity += differenceSigned;
	}
}
