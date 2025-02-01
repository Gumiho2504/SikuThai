

public class User
{
    public string name { get; set; }
    public string avatar { get; set; }
    public int coinBalance { get; set; }

    public User(string name, string avatar, int coinBalance)
    {
        this.name = name;
        this.avatar = avatar;
        this.coinBalance = coinBalance;
    }
}