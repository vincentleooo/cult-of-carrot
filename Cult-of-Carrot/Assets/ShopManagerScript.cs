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
	public GameObject[] prefabs;
	private GameObject newPrefab;

	// Fisher-Yates Shuffle
	void Shuffle(GameObject[] a)
	{
		// Loops through array backwards
		for (int i = a.Length-1; i > 0; i--)
		{
			// Randomize a number between 0 and i (so that the range decreases each time)
			int rnd = Random.Range(0,i);
			
			// Save the value of the current i, otherwise it'll overright when we swap the values
			GameObject temp = a[i];
			
			// Swap the new and old values
			a[i] = a[rnd];
			a[rnd] = temp;
		}
	}

	void Start()
	{
		Shuffle(prefabs);

		for (int i = 0; i < prefabs.Length; i++)
		{
			newPrefab = Instantiate<GameObject>(prefabs[i]);
			newPrefab.transform.SetParent(GameObject.Find("Content").transform, false);
		}

		coinsTxt.text = "Coins: " + coins.ToString();

		// IDs
		// ! PlayerPrefs requires starting from 1 instead of 0.
		shopItems[1, 1] = 1;
		shopItems[1, 2] = 2;
		shopItems[1, 3] = 3;
		shopItems[1, 4] = 4;

		// Price
		shopItems[2, 1] = 10;
		shopItems[2, 2] = 20;
		shopItems[2, 3] = 30;
		shopItems[2, 4] = 40;

		// Quantity
		shopItems[3, 1] = 0;
		shopItems[3, 2] = 0;
		shopItems[3, 3] = 0;
		shopItems[3, 4] = 0;
	}

	public void Buy()
	{
		Debug.Log("Buy.");
		GameObject buttonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

		if (coins >= shopItems[2, buttonRef.GetComponent<ButtonInfo>().itemID])
		{
			coins -= shopItems[2, buttonRef.GetComponent<ButtonInfo>().itemID];
			shopItems[3, buttonRef.GetComponent<ButtonInfo>().itemID]++;
			buttonRef.GetComponent<ButtonInfo>().quantityText.text = shopItems[3, buttonRef.GetComponent<ButtonInfo>().itemID].ToString();
		}
	}

	void Update()
	{
		coinsTxt.text = "Coins: " + coins.ToString();
	}
}
