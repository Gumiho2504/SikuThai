using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class AuthInitialization : MonoBehaviour
{

    public GameObject registerPanel;
    public InputField usernameSingUpInput;
    public InputField passwordSignUpInput;
    public InputField usernameLoginInput;
    public InputField passwordLoginInput;
    public Text statusText;
    [SerializeField] private Text usernameText;
    [SerializeField] private Text coinText;
    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        await SignInCachedUserAsync();
        //SetupEvents();
        LoadCoins();

    }



    async Task SignInCachedUserAsync()
    {
        if (!AuthenticationService.Instance.SessionTokenExists)
        {
            registerPanel.SetActive(true);
            return;
        }

        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
            Debug.Log($"PlayerName: {AuthenticationService.Instance.PlayerInfo.Username}");
            string name = AuthenticationService.Instance.PlayerInfo.Username;
            int coin = await LoadCoins();
            TextUpdate(name, coin);
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {

            Debug.LogException(ex);
        }

    }

    void SetupEvents()
    {
        print("fuck");
        AuthenticationService.Instance.SignedIn += () =>
        {
            // Shows how to get a playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

            // Shows how to get an access token
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

        };

        AuthenticationService.Instance.SignInFailed += (err) =>
        {
            Debug.LogError(err);
        };

        AuthenticationService.Instance.SignedOut += () =>
        {
            Debug.Log("Player signed out.");
        };

        AuthenticationService.Instance.Expired += () =>
          {
              Debug.Log("Player session could not be refreshed and expired.");
          };
    }


    void TextUpdate(string userName, int coin)
    {
        usernameText.text = $"Username : {userName}";
        coinText.text = $"Total Coin : {coin}";
    }

    public async void RegisterUser()
    {
        string username = usernameSingUpInput.text;
        string password = passwordSignUpInput.text;
        print($"{username} - {password}");
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            statusText.text = "Please enter username and password.";
            return;
        }

        try
        {
            // Treat username as email for Unity Authentication

            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            statusText.text = "Registration successful!";
            Debug.Log("User registered with username (email): " + username);
            SaveCoins(500);
            int coin = await LoadCoins();
            TextUpdate(username, coin); s
        }
        catch (AuthenticationException e)
        {
            statusText.text = "Error: " + e.Message;
            Debug.LogError("Sign-up failed: " + e.Message);
        }
    }

    // Sign In User with Username and Password
    public async void LoginUser()
    {
        string username = usernameLoginInput.text;
        string password = passwordLoginInput.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            statusText.text = "Please enter username and password.";
            return;
        }

        try
        {

            string email = username + "@example.com"; // Add a domain to make it a valid email
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(email, password);
            statusText.text = "Login successful!";
            Debug.Log("User logged in: " + AuthenticationService.Instance.PlayerId);
        }
        catch (AuthenticationException e)
        {
            statusText.text = "Login failed: " + e.Message;
            Debug.LogError("Login failed: " + e.Message);
        }
    }

    // Sign Out
    public void LogoutUser()
    {
        AuthenticationService.Instance.SignOut(true);
        statusText.text = "User signed out.";
        Debug.Log("User logged out.");
    }



    public async void SaveCoins(int coins)
    {
        try
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "userCoins", coins }
            };
            await CloudSaveService.Instance.Data.ForceSaveAsync(data);
            Debug.Log($"Coins saved for {AuthenticationService.Instance.PlayerId}: {coins}");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save coins: " + e.Message);
        }
    }


    public async Task<int> LoadCoins()
    {
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "userCoins" });
        if (playerData.TryGetValue("userCoins", out var keyName))
        {
            Debug.Log($"keyName: {keyName.Value.GetAs<string>()}");
            return keyName.Value.GetAs<int>();
        }
        return 0;
    }



}