using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public GameObject ShopItem;
    public Canvas canvas;
    public Sprite[] sprites;
    private Dictionary<ShopItem, ShopItemDetails> items;
    private Dictionary<ShopItem, GameObject> spawnedItems = new Dictionary<ShopItem, GameObject>();

    void Start()
    {
        items = new Dictionary<ShopItem, ShopItemDetails>()
        {
            { global::ShopItem.Life, new ShopItemDetails(){ Name = "Life", Amount = 0, Image = sprites[0], Price = 20 } },
            { global::ShopItem.Boost, new ShopItemDetails(){ Name = "Boost", Amount = 0, Image = sprites[1], Price = 20 } }
        };
        float y = 230.5f;
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

    private void Update()
    {
        foreach (var item in items)
        {
            GameObject shopItem = spawnedItems[item.Key];
            shopItem.GetComponentsInChildren<Text>().First(x => x.name == "Amount").text = item.Value.Amount.ToString();
        }
    }

    public void Buy(ShopItem shopItem)
    {
        items[shopItem].Amount++;
    }

}
