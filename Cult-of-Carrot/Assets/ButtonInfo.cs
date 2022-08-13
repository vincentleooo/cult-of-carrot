using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonInfo : MonoBehaviour
{
	public int itemID;
	public string itemName;
	public float price;
	public int quantity;
	public TextMeshProUGUI priceText;
	public TextMeshProUGUI quantityText;
	private ShopManagerScript shopManager;
	public float buttonDelay = 0.1f;

	// IEnumerator DisableButtonTemp(Button button, float seconds)
	// {
	// 	yield return new WaitForSeconds(seconds);
	// 	button.interactable = true;
	// }

	void Start()
	{
		shopManager = GameObject.Find("ShopManager").GetComponent<ShopManagerScript>();
		GetComponent<Button>().onClick.AddListener(delegate {shopManager.Buy();});
	}

	// void Clicked()
	// {
	// 	GetComponent<Button>().interactable = false;
	// 	unbuyState = true;
	// 	StartCoroutine(DisableButtonTemp(GetComponent<Button>(), buttonDelay));
	// 	GetComponent<Button>().onClick.Invoke();
	// 	unbuyState = false;
	// }

    // Update is called once per frame
    void Update()
    {
		priceText.text = "Price: $" + price.ToString();
		quantityText.text = "Quantity: " + quantity.ToString();
    }
}
