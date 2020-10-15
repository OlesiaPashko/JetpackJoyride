using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public GameObject ShopItem;
    public Canvas canvas;
    public Sprite[] sprites;
    public Text coinsCount;
    private Dictionary<ShopItem, ShopItemDetails> items;
    private Dictionary<ShopItem, GameObject> spawnedItems = new Dictionary<ShopItem, GameObject>();
    
    void Start()
    {
        coinsCount.text = DataHolder.GetCoinsCount().ToString();
        SetItems();

        SpawnItems();   
    }

    private void Update()
    {
        foreach (var item in items)
        {
            GameObject shopItem = spawnedItems[item.Key];
            shopItem.GetComponentsInChildren<Text>().First(x => x.name == "Amount").text = item.Value.Amount.ToString();
        }
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
        }
        else
        {
            Debug.Log("You have not enough coins to buy " + items[shopItem].Name);
        }
    }

    private void SetItems()
    {
        items = new Dictionary<ShopItem, ShopItemDetails>()
        {
            { global::ShopItem.Life, new ShopItemDetails(){ Name = "Life", Amount = DataHolder.GetAmount(global::ShopItem.Life), Image = sprites[0], Price = 20 } },
            { global::ShopItem.Boost, new ShopItemDetails(){ Name = "Boost", Amount = DataHolder.GetAmount(global::ShopItem.Boost), Image = sprites[1], Price = 20 } }
        };
    }

    private void SpawnItems()
    {
        float y = 230.5f;//start y coord to spawn
        foreach (var item in items)
        {
            GameObject shopItem = Instantiate(ShopItem, new Vector3(328f, y, 0f), Quaternion.identity);
            shopItem.GetComponentsInChildren<Text>().First(x => x.name == "Price").text = item.Value.Price.ToString();
            shopItem.GetComponentsInChildren<Text>().First(x => x.name == "Name").text = item.Key.ToString();
            shopItem.GetComponentsInChildren<Image>().First(x => x.name == "Image").sprite = item.Value.Image;
            shopItem.GetComponentsInChildren<Text>().First(x => x.name == "Amount").text = item.Value.Amount.ToString();
            shopItem.GetComponentsInChildren<Button>().First(x => x.name == "Buy").onClick.AddListener(() => Buy(item.Key));
            shopItem.transform.parent = canvas.transform;
            y -= 50f;
            spawnedItems.Add(item.Key, shopItem);
        }
    }

    

}
