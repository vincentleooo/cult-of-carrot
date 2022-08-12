using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonInfo : MonoBehaviour
{
	public int itemID;
	public string itemName;
	public TextMeshProUGUI priceText;
	public TextMeshProUGUI quantityText;
	private GameObject shopManager;
	private ShopManagerScript shopManagerBuyer;

	void Start()
	{
		shopManager = GameObject.Find("ShopManager");
		shopManagerBuyer = shopManager.GetComponent<ShopManagerScript>();
		Debug.Log(shopManager.name);
		GetComponent<Button>().onClick.AddListener(delegate {shopManagerBuyer.Buy();});
	}

    // Update is called once per frame
    void Update()
    {
		priceText.text = "Price: $" + shopManager.GetComponent<ShopManagerScript>().shopItems[2, itemID].ToString();
		quantityText.text = "Quantity: " + shopManager.GetComponent<ShopManagerScript>().shopItems[3, itemID].ToString();
    }
}
