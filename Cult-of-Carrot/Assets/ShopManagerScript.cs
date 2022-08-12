using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{

	public int[,] shopItems = new int[5, 5];
	public float coins;
	public TextMeshProUGUI coinsTxt;
	public ShopItems[] items;
	private GameObject newPrefab;
	public bool unbuy = false;

	// Fisher-Yates Shuffle
	void Shuffle(ShopItems[] a)
	{
		// Loops through array backwards
		for (int i = a.Length-1; i > 0; i--)
		{
			// Randomise a number between 0 and i (so that the range decreases each time)
			int rnd = Random.Range(0, i);
			
			// Save the value of the current i, otherwise it'll overright when we swap the values
			ShopItems temp = a[i];
			
			// Swap the new and old values
			a[i] = a[rnd];
			a[rnd] = temp;
		}
	}

	void Start()
	{
		Shuffle(items);

		for (int i = 0; i < items.Length; i++)
		{
			Debug.Log("Hellu");
			newPrefab = Instantiate<GameObject>(items[i].prefab);
			newPrefab.transform.SetParent(GameObject.Find("Content").transform, false);
		}

		coinsTxt.text = "Coins: " + coins.ToString();
	}

	public void Buy()
	{
		if (!unbuy)
		{
			Debug.Log("Buy.");
			GameObject buttonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

			if (coins >= buttonRef.GetComponent<ButtonInfo>().price)
			{
				coins -= buttonRef.GetComponent<ButtonInfo>().price;
				buttonRef.GetComponent<ButtonInfo>().quantity++;
				buttonRef.GetComponent<ButtonInfo>().quantityText.text = buttonRef.GetComponent<ButtonInfo>().quantity.ToString();
			}
		} else {Unbuy();}
	}

	public void Unbuy()
	{
		GameObject buttonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

		if (buttonRef.GetComponent<ButtonInfo>().quantity > 0)
		{
			coins += buttonRef.GetComponent<ButtonInfo>().price;
			buttonRef.GetComponent<ButtonInfo>().quantity--;
			buttonRef.GetComponent<ButtonInfo>().quantityText.text = buttonRef.GetComponent<ButtonInfo>().quantity.ToString();
		}
	}

	void Update()
	{
		coinsTxt.text = "Coins: " + coins.ToString();
	}
}
