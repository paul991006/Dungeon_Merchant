[System.Serializable]
public class UserData
{
    public string email;
    public int gold;
    public int level;
    public int exp;
    public long lastFarmTime;

    public UserData(string email, int gold, int level, int exp, long lastFarmTime)
    {
        this.email = email;
        this.gold = gold;
        this.level = level;
        this.exp = exp;
        this.lastFarmTime = lastFarmTime;
    }
}
