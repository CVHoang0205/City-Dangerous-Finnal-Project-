using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using LitJson;
using System.IO;
using System.Text;
using System;
using PlayFab.ClientModels;
using PlayFab;

public class ItemJsonDatabase : Singleton<ItemJsonDatabase>
{
    public List<GameItem> listItemInGame = new List<GameItem>();
    public UserStats userStats = new UserStats();
    public bool isDataLoaded = false;
    //private JsonData itemData;
    //private JsonData inGameItemData;
    private List<Item> listItem = new List<Item>();
    //private string filePath = "MyItem.txt";
    // Start is called before the first frame update
    void Start()
    {
        //LoadResourceFromTxt();
        //ConstructDatabase();
        //LoadDataFromLocalDB();
    }

    public void PopulateItemsFromPlayFab(string jsonData)
    {
        // Parse dữ liệu JSON từ playfab thành danh sách các Item
        var itemData = JsonConvert.DeserializeObject<List<Item>>(jsonData); 
        listItem.Clear();
        listItem.AddRange(itemData);
        Debug.Log("Add item from playfab successful");

        //Sau khi tải dữ liệu từ playfab, xây dựng lại cơ sở dữ liệu
        ConstructDatabase();
        //AddNewItemFirstTime();
    }

    public void PopulateItemsFromUserData(string jsonData)
    {
        var savedItems = JsonConvert.DeserializeObject<List<GameItem>>(jsonData);
        listItemInGame.Clear();
        listItemInGame.AddRange(savedItems);
        Debug.Log("Add user's data from playfab successful");
    }

    private void InitUserStats()
    {
        userStats.Atk = 0;
        userStats.Def = 0;
        userStats.Speed = 0;
        for (int i = 0; i < listItemInGame.Count; i++)
        {
            if (listItemInGame[i].IsEquip == true)
            {
                Debug.Log("Equipped Item: " + listItemInGame[i].item.Type + " | ATK: " + listItemInGame[i].item.Atk);

                userStats.Atk += listItemInGame[i].item.Atk;
                userStats.Def += listItemInGame[i].item.Def;
                userStats.Speed += listItemInGame[i].item.Spd;
            }
        }
        Debug.Log("Final ATK: " + userStats.Atk);
        ShopController.Instance.InitUserStats();
    }

    //private void LoadDataFromLocalDB()
    //{
    //    string filePathFull = Application.persistentDataPath + "/" + filePath;
    //    Debug.Log(filePathFull);
    //    if (!File.Exists(filePathFull))
    //    {
    //        //chua ton tai
    //        AddNewItemFirstTime();
    //        Save();
    //    }
    //    else
    //    {
    //        //da ton tai
    //        byte[] jsonByte = null;
    //        try
    //        {
    //            jsonByte = File.ReadAllBytes(filePathFull);
    //        }
    //        catch
    //        {

    //        }
    //        string jsonData = Encoding.ASCII.GetString(jsonByte);
    //        inGameItemData = JsonMapper.ToObject(jsonData);
    //        ConstructMyItemDB();
    //    }
    //}

    public void EquipItem(GameItem item)
    {
        UnEquipItem(item);
        for (int i = 0; i < listItemInGame.Count; i++)
        {
            if (item.item.Type == listItemInGame[i].item.Type && item.item.Id == listItemInGame[i].item.Id)
            {
                listItemInGame[i].IsEquip = true;
                break;
            }
        }
        Save();
        InitUserStats();
    }

    public void UnEquipItem(GameItem item)
    {
        for (int i = 0; i < listItemInGame.Count; i++)
        {
            if (item.item.Type == listItemInGame[i].item.Type)
            {
                listItemInGame[i].IsEquip = false;
            }
        }
        Save();
        InitUserStats();
    }

    public void PurchasedItem(GameItem item)
    {
        for (int i = 0; i < listItemInGame.Count; i++)
        {
            if (item.item.Type == listItemInGame[i].item.Type && item.item.Id == listItemInGame[i].item.Id)
            {
                listItemInGame[i].Purchased = true;
                break;
            }
        }
        Save();
    }

    //private void LoadResourceFromTxt()
    //{
    //    string filePath = "StreamingAsset/Item";
    //    TextAsset targetFile = Resources.Load<TextAsset>(filePath);
    //    itemData = JsonMapper.ToObject(targetFile.text);
    //}

    //private void AddNewItemFirstTime()
    //{
    //    for (int i = 0; i < listItem.Count; i++)
    //    {
    //        GameItem newGameItem = new GameItem();
    //        newGameItem.item = listItem[i];
    //        newGameItem.Purchased = false;
    //        newGameItem.IsEquip = false;
    //        listItemInGame.Add(newGameItem);
    //    }
    //}

    public List<GameItem> GetAllItemOfType(string type)
    {
        List<GameItem> listItem = new List<GameItem>();
        for (int i = 0; i < listItemInGame.Count; i++)
        {
            if (type == listItemInGame[i].item.Type)
            {
                listItem.Add(listItemInGame[i]);
            }
        }
        return listItem;
    }

    public int GetIdOfItemsEquiped(string type)
    {
        //Debug.Log("Type tìm kiếm: " + type);
        //Debug.Log("listItemInGame: " + listItemInGame.Count);
        for (int i = 0; i < listItemInGame.Count; i++)
        {
            if (type == listItemInGame[i].item.Type && listItemInGame[i].IsEquip == true)
            {
                //Debug.Log("Vật phẩm đã trang bị: " + listItemInGame[i].item.Id + " thuộc loại: " + type);
                return listItemInGame[i].item.Id;
            }
        }
        //Debug.LogWarning("Không tìm thấy vật phẩm được trang bị thuộc loại: " + type);
        return 0;
    }

    private void ConstructDatabase()
    {
        //for (int i = 0; i < itemData.Count; i++)
        //{
        //    Item item = new Item();
        //    item.Id = (int)itemData[i]["Id"];
        //    item.Type = (string)itemData[i]["Type"];
        //    item.Price = (int)itemData[i]["Price"];
        //    item.Atk = (int)itemData[i]["Atk"];
        //    item.Def = (int)itemData[i]["Def"];
        //    item.Spd = (int)itemData[i]["Spd"];
        //    listItem.Add(item);
        //}

        //listItemInGame.Clear();
        for (int i = 0; i < listItem.Count; i++)
        {
            GameItem gameItem = new GameItem
            {
                item = listItem[i],
                Purchased = false,
                IsEquip = false
            };
            listItemInGame.Add(gameItem);
        }
    }

    //private void ConstructMyItemDB()
    //{
    //    for (int i = 0; i < inGameItemData.Count; i++)
    //    {
    //        GameItem gameItem = new GameItem();
    //        gameItem.item = new Item();
    //        gameItem.item.Id = (int)inGameItemData[i]["item"]["Id"];
    //        gameItem.item.Type = (string)inGameItemData[i]["item"]["Type"];
    //        gameItem.item.Price = (int)inGameItemData[i]["item"]["Price"];
    //        gameItem.item.Atk = (int)inGameItemData[i]["item"]["Atk"];
    //        gameItem.item.Def = (int)inGameItemData[i]["item"]["Def"];
    //        gameItem.item.Spd = (int)inGameItemData[i]["item"]["Spd"];
    //        gameItem.Purchased = (bool)inGameItemData[i]["Purchased"];
    //        gameItem.IsEquip = (bool)inGameItemData[i]["IsEquip"];
    //        listItemInGame.Add(gameItem);
    //    }
    //}

    private void Save()
    {
        //InitUserStats();
        //string jsonData = JsonConvert.SerializeObject(listItemInGame.ToArray(), Formatting.Indented);
        //string filePathFull = Application.persistentDataPath + "/" + filePath;

        //byte[] jsonByte = Encoding.ASCII.GetBytes(jsonData);

        //if (!Directory.Exists(Path.GetDirectoryName(filePathFull)))
        //{
        //    Directory.CreateDirectory(Path.GetDirectoryName(filePathFull));
        //}
        //if (!File.Exists(filePathFull))
        //{
        //    File.Create(filePathFull).Close();
        //}

        //try
        //{
        //    File.WriteAllBytes(filePathFull, jsonByte);
        //}
        //catch (Exception e)
        //{
        //    Debug.LogWarning("Cannot save " + e.Message);
        //}

        InitUserStats();
        var data = new Dictionary<string, string>
        {
            {"ShopItems", JsonConvert.SerializeObject(listItemInGame.ToArray(), Formatting.Indented) }
        };

        var request = new UpdateUserDataRequest { Data = data };
        PlayFabClientAPI.UpdateUserData(request, OnDataUpdateSuccess, OnUpdateDatafailure);
    }

    private void OnDataUpdateSuccess(UpdateUserDataResult result)
    {
        Debug.Log("User data updated successfully");
    }

    private void OnUpdateDatafailure(PlayFabError error)
    {
        Debug.Log("Failed to update user data: " + error.GenerateErrorReport());
    }

    public int GetEquippedWeaponAtk()
    {
        foreach(GameItem gameItem in listItemInGame)
        {
            if(gameItem.IsEquip && gameItem.item.Type == "Weapons")
            {
                Debug.Log("ID: " + gameItem.item.Id + ", ATK: " + gameItem.item.Atk);
                return gameItem.item.Atk;
            }
        }
        return 0;
    }
}

public class UserStats
{
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Speed { get; set; }
}

public class GameItem
{
    public bool Purchased { get; set; }
    public bool IsEquip { get; set; }
    public Item item { get; set; }
}

public class Item
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int Price { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Spd { get; set; }
}
