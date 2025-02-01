using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private int coinBalance;

    public void EarnCoins(int amount)
    {
        coinBalance += amount;
        Debug.Log("Coins earned: " + amount);
        SaveCoinBalance();
    }

    public void SpendCoins(int amount)
    {
        if (coinBalance >= amount)
        {
            coinBalance -= amount;
            Debug.Log("Coins spent: " + amount);
            SaveCoinBalance();
        }
        else
        {
            Debug.Log("Not enough coins.");
        }
    }

    private void SaveCoinBalance()
    {
        // Save the coin balance to Cloud Save
        var profileManager = GetComponent<UserManager>();
        profileManager.SaveUserProfile(new User("Username", "Avatar", coinBalance));
    }

    public void LoadCoinBalance()
    {
        // Load the coin balance from Cloud Save
        var profileManager = GetComponent<UserManager>();
        profileManager.LoadUserProfile();
    }
}
