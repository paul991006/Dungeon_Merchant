using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using System.Collections.Generic;

public class PlayerCurrency : MonoBehaviour
{
    public int essence => PlayerData.Instance.essence;
    public int gold => PlayerData.Instance.gold;

    public void AddEssence(int amount)
    {
        PlayerData.Instance.essence += amount;
        Save();
    }

    public bool UseEssence(int amount)
    {
        if (PlayerData.Instance.essence < amount) return false;
        PlayerData.Instance.essence -= amount;
        Save();
        return true;
    }

    public void AddGold(int amount)
    {
        PlayerData.Instance.gold += amount;
        Save();
    }

    public bool UseGold(int amount)
    {
        if (PlayerData.Instance.gold < amount) return false;
        PlayerData.Instance.gold -= amount;
        Save();
        return true;
    }

    void Save()
    {
        string uid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(uid).Child("currency").UpdateChildrenAsync(new Dictionary<string, object>
        {
            { "essence", PlayerData.Instance.essence },
            { "gold", PlayerData.Instance.gold }
        });
    }
}
