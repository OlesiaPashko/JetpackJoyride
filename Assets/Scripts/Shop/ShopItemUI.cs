using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public Text name;
    public Text price;
    public Text amount;
    public Image image;
    public Button buy;

    private ShopItemDetails currentItem;
    public void Init(ShopItemDetails itemDetails)
    {
        currentItem = itemDetails;
        price.text = itemDetails.Price.ToString();
        name.text = itemDetails.ShopItem.ToString();
        amount.text = itemDetails.Amount.ToString();
        image.sprite = itemDetails.Image;
        buy.onClick.AddListener(Buy);
    }

    private void Buy()
    {
        Debug.Log("Buy");
        if (DataHolder.TrySubtractCoinsCount(currentItem.Price))
        {
            DataHolder.IncrementAmount(currentItem.ShopItem);
            currentItem.Amount++;
            //coinsCount.text = DataHolder.GetCoinsCount().ToString();
            amount.text = currentItem.Amount.ToString();
        }
        else
        {
            Debug.Log("You have not enough coins to buy " + currentItem.ShopItem);
        }
    }
}
