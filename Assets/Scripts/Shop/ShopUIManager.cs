using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{

    public ShopItemUI shopItem;
    public SkinItemUI skinItem;
    public Canvas canvas;
    public Sprite[] sprites;
    public Sprite[] skinImages;
    public Text coinsCount;
    private List<ShopItemDetails> items;
    private List<SkinDetails> skins;
    private float itemHeight = 50f;

    void Start()
    {
        DataHolder.SetUnbought(Skins.Knight);//for testing
        coinsCount.text = DataHolder.GetCoinsCount().ToString();

        SetItems();
        SetSkins();

        SpawnItems();
        SpawnSkins();
    }

    private void Update()
    {
        coinsCount.text = DataHolder.GetCoinsCount().ToString();
    }


    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void SetItems() 
    {
        items = new List<ShopItemDetails>()
        {
            new ShopItemDetails(){ ShopItem = ShopItem.Life, Amount = DataHolder.GetAmount(global::ShopItem.Life), Image = sprites[0], Price = 20 },
            new ShopItemDetails(){ ShopItem = ShopItem.Boost, Amount = DataHolder.GetAmount(global::ShopItem.Boost), Image = sprites[1], Price = 20 } 
        };
    }

    private void SetSkins()
    {
        skins = new List<SkinDetails>()
        {
            new SkinDetails(){ Skin = Skins.Default, Price = 0, IsBought = true, Image = skinImages[0]},
            new SkinDetails(){ Skin = Skins.Knight, Price = 100, IsBought = DataHolder.IsBought(Skins.Knight), Image = skinImages[1]}
        };
    }


    private void SpawnItems()
    {
        float scaleFactor = canvas.scaleFactor;

        shopItem.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

        float y = (canvas.pixelRect.y + (canvas.pixelRect.height / 2)) + itemHeight * scaleFactor;
        float xToSpawn = (canvas.pixelRect.x + (canvas.pixelRect.width / 2));

        foreach (var item in items)
        {
            ShopItemUI itemUI = Instantiate(shopItem, new Vector3(xToSpawn, y, 0f), Quaternion.identity);

            itemUI.Init(item);

            itemUI.transform.parent = canvas.transform;

            y -= itemHeight * scaleFactor;
        }
    }


    private void SpawnSkins()
    {
        float scaleFactor = canvas.scaleFactor;
        skinItem.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        float y = (canvas.pixelRect.y + (canvas.pixelRect.height / 2)) - itemHeight * scaleFactor;
        float xToSpawn = (canvas.pixelRect.x + (canvas.pixelRect.width / 2));

        foreach (var skin in skins)
        {
            SkinItemUI skinObject = Instantiate(skinItem, new Vector3(xToSpawn, y, 0f), Quaternion.identity);
            skinObject.Init(skin);
            skinObject.transform.parent = canvas.transform;
            y -= itemHeight * scaleFactor;
        }
    }

    
}
