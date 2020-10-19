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
        //Set image, name and currentSkin
        currentSkin = skinDetails;
        name.text = skinDetails.Skin.ToString();
        image.sprite = skinDetails.Image;

        //Set listeners on buttons
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
        //Activate use button if active skin was changed
        if (use.IsActive() && use.interactable == false && DataHolder.GetActiveSkin() != currentSkin.Skin)
            use.interactable = true;
    }
    private void InitBought()
    {
        //Show active skin
        if (DataHolder.GetActiveSkin() == currentSkin.Skin)
        {
            use.interactable = false;
        }
        
        //Dont show price text and buy button
        price.enabled = false;
        buy.gameObject.SetActive(false);
    }

    private void InitNotBought()
    {
        //Show price text and dont show use button
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
        //Check is bought
        if (DataHolder.IsBought(currentSkin.Skin))
        {
            //Save new active skin
            DataHolder.SetActiveSkin(currentSkin.Skin);

            //Show skin used
            use.gameObject.SetActive(true);
            use.interactable = false;

        }
        else
        {
            Debug.Log("You have not enough coins to buy " + currentSkin.Skin);
        }
    }
}
