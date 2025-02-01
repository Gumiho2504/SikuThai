using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthInitialization : MonoBehaviour
{

    public GameObject registerPanel, loginPanel;
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
            string facebookId = AuthenticationService.Instance.PlayerInfo.GetFacebookId();
            Debug.Log($"PlayerName: {AuthenticationService.Instance.PlayerInfo.GetFacebookId()}");
            Debug.Log($"facebookId: {facebookId}");
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
            await Task.Delay(1000);
            int coin = await LoadCoins();
            TextUpdate(username, coin);
            await Task.Delay(1000);
            registerPanel.SetActive(false);
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
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            statusText.text = "Login successful!";
            Debug.Log("User logged in: " + AuthenticationService.Instance.PlayerId);
            if (AuthenticationService.Instance.PlayerId != null)
            {
                int coin = await LoadCoins();
                TextUpdate(username, coin);
                await Task.Delay(1000);
                loginPanel.SetActive(false);
            }
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("User logged out.");
    }



    public static async void SaveCoins(int coins)
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


    public static async Task<int> LoadCoins()
    {
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "userCoins" });
        if (playerData.TryGetValue("userCoins", out var keyName))
        {
            Debug.Log($"keyName: {keyName.Value.GetAs<string>()}");
            return keyName.Value.GetAs<int>();
        }
        return 0;
    }



    public void LaunchGame()
    {
        if (AuthenticationService.Instance.PlayerId == null)
        {
            statusText.text = "Please login first.";
            loginPanel.SetActive(true);
            return;
        }
        else
        {
            SceneManager.LoadScene("SikuThai");
        }

    }



}