using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public GameObject ShopItem;
    public GameObject skinItem;
    public Canvas canvas;
    public Sprite[] sprites;
    public Sprite[] skinImages;
    public Text coinsCount;
    private Dictionary<ShopItem, ShopItemDetails> items;
    private Dictionary<ShopItem, GameObject> spawnedItems = new Dictionary<ShopItem, GameObject>();
    private Dictionary<Skins, SkinDetails> skins;
    private Dictionary<Skins, GameObject> spawnedSkins = new Dictionary<Skins, GameObject>();
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


    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Buy(ShopItem shopItem)
    {
        if (DataHolder.TrySubtractCoinsCount(items[shopItem].Price))
        {
            items[shopItem].Amount++;
            DataHolder.IncrementAmount(shopItem);
            coinsCount.text = DataHolder.GetCoinsCount().ToString();
            spawnedItems[shopItem].GetComponentsInChildren<Text>().First(x => x.name == "Amount").text = items[shopItem].Amount.ToString();
        }
        else
        {
            Debug.Log("You have not enough coins to buy " + shopItem);
        }
    }

    private void BuySkin(Skins skin)
    {
        if (DataHolder.TrySubtractCoinsCount(skins[skin].Price))
        {
            skins[skin].isBought = true;
            DataHolder.SetBought(skin);
            coinsCount.text = DataHolder.GetCoinsCount().ToString();

            spawnedSkins[skin].GetComponentsInChildren<Button>(true).First(x => x.name == "Buy").gameObject.SetActive(false);
            spawnedSkins[skin].GetComponentsInChildren<Button>(true).First(x => x.name == "Use").gameObject.SetActive(true);
            spawnedSkins[skin].GetComponentsInChildren<Text>().First(x => x.name == "Price").enabled = false;
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

    private void SetItems()
    {
        items = new Dictionary<ShopItem, ShopItemDetails>()
        {
            { global::ShopItem.Life, new ShopItemDetails(){ Amount = DataHolder.GetAmount(global::ShopItem.Life), Image = sprites[0], Price = 20 } },
            { global::ShopItem.Boost, new ShopItemDetails(){ Amount = DataHolder.GetAmount(global::ShopItem.Boost), Image = sprites[1], Price = 20 } }
        };
    }

    private void SpawnItems()
    {
        float y = 230.5f;//start y coord to spawn
        foreach (var item in items)
        {
            GameObject shopItem = Instantiate(ShopItem, new Vector3(328f, y, 0f), Quaternion.identity);
            Text[] texts = shopItem.GetComponentsInChildren<Text>();
            texts.First(x => x.name == "Price").text = item.Value.Price.ToString();
            texts.First(x => x.name == "Name").text = item.Key.ToString();
            texts.First(x => x.name == "Amount").text = item.Value.Amount.ToString();
            shopItem.GetComponentsInChildren<Image>().First(x => x.name == "Image").sprite = item.Value.Image;
            shopItem.GetComponentsInChildren<Button>().First(x => x.name == "Buy").onClick.AddListener(() => Buy(item.Key));
            shopItem.transform.parent = canvas.transform;
            y -= 50f;
            spawnedItems.Add(item.Key, shopItem);
        }
    }

    private void SetSkins()
    {
        skins = new Dictionary<Skins, SkinDetails>()
        {
            { Skins.Default, new SkinDetails(){ Price = 0, isBought = true, Image = skinImages[0]}},
            { Skins.Knight, new SkinDetails(){ Price = 100, isBought = DataHolder.IsBought(Skins.Knight), Image = skinImages[1]} }
        };
    }

    private void SpawnSkins()
    {
        float y = 129.5f;//start y coord to spawn
        foreach (var skin in skins)
        {
            GameObject skinObject = Instantiate(skinItem, new Vector3(328f, y, 0f), Quaternion.identity);
            skinObject.GetComponentsInChildren<Text>().First(x => x.name == "Name").text = skin.Key.ToString();
            skinObject.GetComponentsInChildren<Image>().First(x => x.name == "Image").sprite = skin.Value.Image;

            AddSkinsButtonsListeners(skinObject, skin.Key);

            if (skin.Value.isBought)
            {
                SpawnBoughtSkin(skin.Key, skinObject);
            }
            else
            {
                SpawnNotBoughtSkin(skin, skinObject);
            }
            skinObject.transform.parent = canvas.transform;
            y -= 50f;
            spawnedSkins.Add(skin.Key, skinObject);
        }
    }

    private void AddSkinsButtonsListeners(GameObject skinObject, Skins skin)
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
    }

}
