using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Login();    
    }

    private void Login()
    {
        //string customId = SystemInfo.deviceUniqueIdentifier; // ID duy nhất của thiết bị
        string customId = "E78A0443E7BA02C8"; //ID của player
        //string customId = "465C784D61E95068"; //ID test
        var request = new LoginWithCustomIDRequest { CustomId = customId, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login successful");
        GetTitleData();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("Login failed: " + error.GenerateErrorReport());
    }

    private void GetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), OnTitleDataSuccess, OnTitleDataFailure);
    }

    private void OnTitleDataSuccess(GetTitleDataResult result)
    {
        if(result.Data == null || !result.Data.ContainsKey("ShopItems"))
        {
            Debug.Log("No title data found for key");
            return;
        }

        string itemData = result.Data["ShopItems"];
        //Debug.Log("Item Data: " + itemData);

        //truyền dữ liệu từ playfab sang itemJsonDatabase
        ItemJsonDatabase.Instance.PopulateItemsFromPlayFab(itemData);
        GetUserData();
    }

    private void OnTitleDataFailure(PlayFabError error)
    {
        Debug.Log("Failed to get title data: " + error.GenerateErrorReport());
    }

    private void GetUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnUserDataSucces, OnUserDataFailure);
    }

    private void OnUserDataSucces(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("ShopItems"))
        {
            string userData = result.Data["ShopItems"].Value;
            ItemJsonDatabase.Instance.PopulateItemsFromUserData(userData);
            ItemJsonDatabase.Instance.isDataLoaded = true;
        }
    }

    private void OnUserDataFailure(PlayFabError error)
    {
        Debug.Log("Failed to get user data: " + error.GenerateErrorReport());
    }
}
