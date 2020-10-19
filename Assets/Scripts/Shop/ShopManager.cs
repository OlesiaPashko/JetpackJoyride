using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour//shopUiManager
{

    public ShopItemUI shopItem;
    public SkinItemUI skinItem;
    public Canvas canvas;
    public Sprite[] sprites;
    public Sprite[] skinImages;
    public Text coinsCount;
    private List<ShopItemDetails> items;
    private List<SkinDetails> skins;

    private Skins activeSkin;

    void Start()
    {
        coinsCount.text = DataHolder.GetCoinsCount().ToString();
        activeSkin = DataHolder.GetActiveSkin();

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

    /*

    private void BuySkin(Skins skin)
    {
        //Check if money is enought
        if (DataHolder.TrySubtractCoinsCount(skins[skin].Price))
        {
            //Mark skin as bought
            skins[skin].isBought = true;
            DataHolder.SetBought(skin);
            coinsCount.text = DataHolder.GetCoinsCount().ToString();

            //Show bought item
            Button[] buttons = spawnedSkins[skin].GetComponentsInChildren<Button>(true);
            buttons.First(x => x.name == "Buy").gameObject.SetActive(false);
            buttons.First(x => x.name == "Use").gameObject.SetActive(true);
            spawnedSkins[skin].GetComponentsInChildren<Text>().First(x => x.name == "Price").enabled = false;

            //Apply skin
            UseSkin(skin);
        }
        else
        {
            Debug.Log("You have not enough coins to buy " + skin);
        }
    }

    private void UseSkin(Skins skin)
    {
        if (DataHolder.IsBought(skin))
        {
            activeSkin = skin;
            DataHolder.SetActiveSkin(skin);

            GameObject gameObjectOfSkinThatWasActive = spawnedSkins.First((x) => x.Value.GetComponentsInChildren<Button>()
                                            .Any(y => (y.name == "Use" && y.interactable == false))).Value;
            Button useButtonOfSkinThatWasActive = gameObjectOfSkinThatWasActive.GetComponentsInChildren<Button>().First(x=>x.name == "Use");
            useButtonOfSkinThatWasActive.interactable = true;

            Button useButton = spawnedSkins[skin].GetComponentsInChildren<Button>().First(x => x.name == "Use");
            useButton.gameObject.SetActive(true);
            useButton.interactable = false;
            
        }
        else
        {
            Debug.Log("You have not enough coins to buy " + skin);
        }
    }
    */
    private void SetItems() 
    {
        items = new List<ShopItemDetails>()
        {
            new ShopItemDetails(){ ShopItem = ShopItem.Life, Amount = DataHolder.GetAmount(global::ShopItem.Life), Image = sprites[0], Price = 20 },
            new ShopItemDetails(){ ShopItem = ShopItem.Boost, Amount = DataHolder.GetAmount(global::ShopItem.Boost), Image = sprites[1], Price = 20 } 
        };
    }

    private void SpawnItems()
    {
        float scaleFactor = canvas.scaleFactor;

        shopItem.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

        float y = (canvas.pixelRect.y + (canvas.pixelRect.height / 2)) + 50f * scaleFactor;
        float xToSpawn = (canvas.pixelRect.x + (canvas.pixelRect.width / 2));

        foreach (var item in items)
        {
            ShopItemUI itemUI = Instantiate(shopItem, new Vector3(xToSpawn, y, 0f), Quaternion.identity);

            itemUI.Init(item);

            itemUI.transform.parent = canvas.transform;

            y -= 50f * scaleFactor;
        }
    }

    private void SetSkins()
    {
        skins = new List<SkinDetails>()
        {
            new SkinDetails(){ Skin = Skins.Default, Price = 0, IsBought = true, Image = skinImages[0]},
            new SkinDetails(){ Skin = Skins.Knight, Price = 100, IsBought = DataHolder.IsBought(Skins.Knight), Image = skinImages[1]}
        };
    }

    private void SpawnSkins()
    {
        float scaleFactor = canvas.scaleFactor;
        skinItem.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        float y = (canvas.pixelRect.y + (canvas.pixelRect.height / 2)) - 50f * scaleFactor;
        float xToSpawn = (canvas.pixelRect.x + (canvas.pixelRect.width / 2));

        foreach (var skin in skins)
        {
            SkinItemUI skinObject = Instantiate(skinItem, new Vector3(xToSpawn, y, 0f), Quaternion.identity);
            //skinObject.GetComponentsInChildren<Text>().First(x => x.name == "Name").text = skin.Key.ToString();
            //skinObject.GetComponentsInChildren<Image>().First(x => x.name == "Image").sprite = skin.Value.Image;

            ///AddSkinsButtonsListeners(skinObject, skin.Key);

            //if (skin.Value.isBought)
            //{
            //    SpawnBoughtSkin(skin.Key, skinObject);
            //}
            //else
            //{
            //    SpawnNotBoughtSkin(skin, skinObject);
            //}
            skinObject.Init(skin);
            skinObject.transform.parent = canvas.transform;
            y -= 50f * scaleFactor;
            //spawnedSkins.Add(skin.Key, skinObject);
        }
    }

    /*private void AddSkinsButtonsListeners(GameObject skinObject, Skins skin)
    {
        var useButton = skinObject.GetComponentsInChildren<Button>().First(x => x.name == "Use");
        useButton.onClick.AddListener(() => UseSkin(skin));
        var buyButton = skinObject.GetComponentsInChildren<Button>().First(x => x.name == "Buy");
        buyButton.onClick.AddListener(() => BuySkin(skin));
    }

    private void SpawnBoughtSkin(Skins skin, GameObject skinObject)
    {
        var useButton = skinObject.GetComponentsInChildren<Button>().First(x => x.name == "Use");
        if (activeSkin == skin)
         {
             useButton.interactable = false;
         }
         skinObject.GetComponentsInChildren<Button>().First(x => x.name == "Buy").gameObject.SetActive(false);
         skinObject.GetComponentsInChildren<Text>().First(x => x.name == "Price").enabled = false;
    }

    private void SpawnNotBoughtSkin(KeyValuePair<Skins, SkinDetails> skin, GameObject skinObject)
    {
        skinObject.GetComponentsInChildren<Text>().First(x => x.name == "Price").text = skin.Value.Price.ToString();
        skinObject.GetComponentsInChildren<Button>().First(x => x.name == "Use").gameObject.SetActive(false);
    }*/

}
