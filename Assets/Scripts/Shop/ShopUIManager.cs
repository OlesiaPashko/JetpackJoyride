using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{

    public BonusItemUI bonusItem;
    public SkinItemUI skinItem;
    public Canvas canvas;
    public Sprite[] bonusImages;
    public Sprite[] skinImages;
    public Text coinsCount;
    private List<BonusDetails> bonuses;
    private List<SkinDetails> skins;
    private float itemHeight = 50f;

    void Start()
    {
        coinsCount.text = DataHolder.GetCoinsCount().ToString();

        SetBonuses();
        SetSkins();

        SpawnBonuses();
        SpawnSkins();
    }

    private void Update()
    {
        //Show coins count
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

    private void SetBonuses() 
    {
        bonuses = new List<BonusDetails>()
        {
            new BonusDetails(){ Bonus = Bonus.Life, Amount = DataHolder.GetAmount(Bonus.Life), Image = bonusImages[0], Price = 20 },
            new BonusDetails(){ Bonus = Bonus.Boost, Amount = DataHolder.GetAmount(Bonus.Boost), Image = bonusImages[1], Price = 20 } 
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


    private void SpawnBonuses()
    {
        //Scale bonus prefab
        float scaleFactor = canvas.scaleFactor;
        bonusItem.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

        //Get coords to instatiate bonus items
        float yToSpawn = (canvas.pixelRect.y + (canvas.pixelRect.height / 2)) + itemHeight * scaleFactor;
        float xToSpawn = (canvas.pixelRect.x + (canvas.pixelRect.width / 2));

        //Instantiate all bonuses inside canvas
        foreach (var bonus in bonuses)
        {
            BonusItemUI bonusUI = Instantiate(bonusItem, new Vector3(xToSpawn, yToSpawn, 0f), Quaternion.identity);
            bonusUI.Init(bonus);
            bonusUI.transform.parent = canvas.transform;
            yToSpawn -= itemHeight * scaleFactor;
        }
    }


    private void SpawnSkins()
    {
        //Scale bonus prefab
        float scaleFactor = canvas.scaleFactor;
        skinItem.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

        //Get coords to instatiate skins
        float yToSpawn = (canvas.pixelRect.y + (canvas.pixelRect.height / 2)) - itemHeight * scaleFactor;
        float xToSpawn = (canvas.pixelRect.x + (canvas.pixelRect.width / 2));

        //Instantiate all skins inside canvas
        foreach (var skin in skins)
        {
            SkinItemUI skinUI = Instantiate(skinItem, new Vector3(xToSpawn, yToSpawn, 0f), Quaternion.identity);
            skinUI.Init(skin);
            skinUI.transform.parent = canvas.transform;
            yToSpawn -= itemHeight * scaleFactor;
        }
    }

    
}
