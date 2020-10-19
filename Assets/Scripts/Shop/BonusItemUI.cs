using UnityEngine;
using UnityEngine.UI;

public class BonusItemUI : MonoBehaviour
{
    public Text name;
    public Text price;
    public Text amount;
    public Image image;
    public Button buy;

    private BonusDetails currentItem;
    public void Init(BonusDetails itemDetails)
    {
        currentItem = itemDetails;
        price.text = itemDetails.Price.ToString();
        name.text = itemDetails.Bonus.ToString();
        amount.text = itemDetails.Amount.ToString();
        image.sprite = itemDetails.Image;
        buy.onClick.AddListener(Buy);
    }

    private void Buy()
    {
        //Check is money is enough
        if (DataHolder.TrySubtractCoinsCount(currentItem.Price))
        {
            //Increment amount of bonus
            DataHolder.IncrementAmount(currentItem.Bonus);
            currentItem.Amount++;
            amount.text = currentItem.Amount.ToString();
        }
        else
        {
            Debug.Log("You have not enough coins to buy " + currentItem.Bonus);
        }
    }
}
