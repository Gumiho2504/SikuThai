using System;
using System.Collections.Generic;
using Unity.Services.CloudSave;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public async void SaveUserProfile(User profile)
    {
        var data = new Dictionary<string, object>
        {
            { "Username", profile.name },
            { "Avatar", profile.avatar },
            { "CoinBalance", profile.coinBalance }
        };

        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
    }

    public async void LoadUserProfile()
    {
        var data = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "Username", "Avatar", "CoinBalance" });

        if (data.ContainsKey("Username"))
        {
            string username = data["Username"].ToString();
            string avatar = data["Avatar"].ToString();
            int coinBalance = Convert.ToInt32(data["CoinBalance"]);

            User profile = new User(username, avatar, coinBalance);
            Debug.Log("Profile loaded: " + profile.name);
        }
    }
}