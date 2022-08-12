using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyOrSellButton : MonoBehaviour
{
	public TextMeshProUGUI buyOrSellMode;
	public ShopManagerScript shopManager;
	private string buyOrSellText;
	// Start is called before the first frame update
	void Start()
	{
		buyOrSellText = "Buy Mode";
	}

	public void Clicked()
	{
		if (shopManager.unbuy)
		{
			shopManager.unbuy = false;
			buyOrSellText = "Buy Mode";
		}
		else
		{
			shopManager.unbuy = true;
			buyOrSellText = "Sell Mode";
		}
	}

	// Update is called once per frame
	void Update()
	{
		buyOrSellMode.text = buyOrSellText;
	}
}
