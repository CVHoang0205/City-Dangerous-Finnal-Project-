using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopController : Singleton<ShopController>
{
    public GameObject itemPrefabs;
    public GameObject parents;

    public Button[] listButton;
    public TextMeshProUGUI[] userStats;

    private static string[] listType = { "Weapons", "Hat", "Pants", "Shield" };
    // Start is called before the first frame update
    void Start()
    {
        ClickButtonType(0);
        listButton[0].onClick.AddListener(() => ClickButtonType(0));
        listButton[1].onClick.AddListener(() => ClickButtonType(1));
        listButton[2].onClick.AddListener(() => ClickButtonType(2));
        listButton[3].onClick.AddListener(() => ClickButtonType(3));

        //InitUserStats();
    }

    public void InitUserStats()
    {
        //userStats[0].text = "ATK: " + ItemJsonDatabase.Instance.userStats.Atk;
        //userStats[1].text = "DEF: " + ItemJsonDatabase.Instance.userStats.Def;
        //userStats[2].text = "SPD: " + ItemJsonDatabase.Instance.userStats.Speed;
    }

    private void ClickButtonType(int type)
    {
        for (int i = 0; i < 4; i++)
        {
            listButton[i].GetComponent<Image>().color = Color.white;
        }
        listButton[type].GetComponent<Image>().color = Color.green;
        CreateItem(listType[type]);
    }

    public void CreateItem(string type)
    {
        foreach (Transform child in parents.transform)
        {
            Destroy(child.gameObject);
        }

        List<GameItem> itemInGame = ItemJsonDatabase.Instance.GetAllItemOfType(type);
        for (int i = 0; i < itemInGame.Count; i++)
        {
            GameObject item = Instantiate(itemPrefabs, parents.transform);
            item.GetComponent<InitItem>().InitItemUI(itemInGame[i]);
        }
    }
}
