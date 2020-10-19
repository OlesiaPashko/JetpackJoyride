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

    public void Init(SkinDetails skinDetails)
    {
        price.text = skinDetails.Price.ToString();
        name.text = skinDetails.Skin.ToString();
        image.sprite = skinDetails.Image;
        buy.onClick.AddListener(() => Debug.Log("buys"));
        //use.onClick.AddListener(() => Use(item));
    }
}
