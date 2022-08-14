using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ShopManagerScript : MonoBehaviour
{

	public float coins;
	public TextMeshProUGUI coinsTxt;
	public Consumables[] consumableItems;
	public Equipment[] equipmentItems;
	public Weapons[] weaponItems;
	private GameObject newPrefab;
	public bool unbuy = false;
	public UnityEvent onApplicationExit;

	// Fisher-Yates Shuffle
	void Shuffle(Consumables[] a)
	{
		// Loops through array backwards
		for (int i = a.Length - 1; i > 0; i--)
		{
			// Randomise a number between 0 and i (so that the range decreases each time)
			int rnd = Random.Range(0, i);

			// Save the value of the current i, otherwise it'll overright when we swap the values
			Consumables temp = a[i];

			// Swap the new and old values
			a[i] = a[rnd];
			a[rnd] = temp;
		}
	}

	void Shuffle(Equipment[] a)
	{
		// Loops through array backwards
		for (int i = a.Length - 1; i > 0; i--)
		{
			// Randomise a number between 0 and i (so that the range decreases each time)
			int rnd = Random.Range(0, i);

			// Save the value of the current i, otherwise it'll overright when we swap the values
			Equipment temp = a[i];

			// Swap the new and old values
			a[i] = a[rnd];
			a[rnd] = temp;
		}
	}

	void Shuffle(Weapons[] a)
	{
		// Loops through array backwards
		for (int i = a.Length - 1; i > 0; i--)
		{
			// Randomise a number between 0 and i (so that the range decreases each time)
			int rnd = Random.Range(0, i);

			// Save the value of the current i, otherwise it'll overright when we swap the values
			Weapons temp = a[i];

			// Swap the new and old values
			a[i] = a[rnd];
			a[rnd] = temp;
		}
	}

	void Start()
	{
		InstantiateConsumables();
		InstantiateEquipment();
		InstantiateWeapons();

		coinsTxt.text = "Coins: " + coins.ToString();
	}

	void InstantiateConsumables()
	{
		Shuffle(consumableItems);

		for (int i = 0; i < consumableItems.Length; i++)
		{
			Debug.Log("Hellu");
			newPrefab = Instantiate<GameObject>(consumableItems[i].prefab);
			newPrefab.transform.SetParent(GameObject.Find("Consumables").transform, false);
		}
	}

	void InstantiateEquipment()
	{
		Shuffle(equipmentItems);

		for (int i = 0; i < 7; i++)
		{
			Debug.Log("Hellu");
			newPrefab = Instantiate<GameObject>(equipmentItems[i].prefab);
			newPrefab.transform.SetParent(GameObject.Find("Equipment").transform, false);
		}
	}

	void InstantiateWeapons()
	{
		Shuffle(weaponItems);

		for (int i = 0; i < 3; i++)
		{
			Debug.Log("Hellu");
			newPrefab = Instantiate<GameObject>(weaponItems[i].prefab);
			newPrefab.transform.SetParent(GameObject.Find("Equipment").transform, false);
		}
	}

	public void Buy()
	{
		if (!unbuy)
		{
			Debug.Log("Buy.");
			GameObject buttonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

			if (coins >= buttonRef.GetComponent<ShopButton>().Price())
			{
				coins -= buttonRef.GetComponent<ShopButton>().Price();
				buttonRef.GetComponent<ShopButton>().ModifyQuantity(1);
				buttonRef.GetComponent<ShopButton>().quantityText.text = buttonRef.GetComponent<ShopButton>().Quantity().ToString();
			}
		}
		else { Unbuy(); }
	}

	public void Unbuy()
	{
		GameObject buttonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

		if (buttonRef.GetComponent<ShopButton>().Quantity() > 0)
		{
			coins += buttonRef.GetComponent<ShopButton>().Price();
			buttonRef.GetComponent<ShopButton>().ModifyQuantity(-1);
			buttonRef.GetComponent<ShopButton>().quantityText.text = buttonRef.GetComponent<ShopButton>().Quantity().ToString();
		}
	}

	void Update()
	{
		coinsTxt.text = "Coins: " + coins.ToString();
	}

	void OnApplicationQuit()
    {
        onApplicationExit.Invoke();
    }
}
