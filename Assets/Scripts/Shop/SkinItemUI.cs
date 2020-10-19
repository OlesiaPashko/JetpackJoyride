using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinItemUI : MonoBehaviour
{
    public Text name;
    public Text price;
    public Image image;
    public Button buy;
    public Button use;

    private SkinDetails currentSkin;
    public void Init(SkinDetails skinDetails)
    {
        currentSkin = skinDetails;
        name.text = skinDetails.Skin.ToString();
        image.sprite = skinDetails.Image;

        buy.onClick.AddListener(Buy);
        use.onClick.AddListener(Use);

        if (skinDetails.IsBought)
        {
            InitBought();
        }
        else
        {
            InitNotBought();
        }
    }

    private void Update()
    {
        if (use.IsActive() && use.interactable == false && DataHolder.GetActiveSkin() != currentSkin.Skin)
            use.interactable = true;
    }
    private void InitBought()
    {
        if (DataHolder.GetActiveSkin() == currentSkin.Skin)
        {
            use.interactable = false;
        }
        buy.gameObject.SetActive(false);
        price.enabled = false;
    }

    private void InitNotBought()
    {
        price.text = currentSkin.Price.ToString();
        use.gameObject.SetActive(false);
    }

    private void Buy()
    {
        //Check if money is enought
        if (DataHolder.TrySubtractCoinsCount(currentSkin.Price))
        {
            //Mark skin as bought
            currentSkin.IsBought = true;
            DataHolder.SetBought(currentSkin.Skin);

            //Show bought item
            buy.gameObject.SetActive(false);
            use.gameObject.SetActive(true);
            price.enabled = false;

            //Apply skin
            Use();
        }
        else
        {
            Debug.Log("You have not enough coins to buy " + currentSkin.Skin);
        }
    }

    private void Use()
    {
        if (DataHolder.IsBought(currentSkin.Skin))
        {
            DataHolder.SetActiveSkin(currentSkin.Skin);

            use.gameObject.SetActive(true);
            use.interactable = false;

        }
        else
        {
            Debug.Log("You have not enough coins to buy " + currentSkin.Skin);
        }
    }
}
