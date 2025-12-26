using Firebase.Auth;
using Firebase.Database;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public int hpLevel;
    public int atkLevel;
    public int aspLevel;
    public int defLevel;
    public int essence;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadFromSnapshot(DataSnapshot snapshot)
    {
        var stats = snapshot.Child("playerStats");

        hpLevel = GetInt(stats, "hpLevel");
        atkLevel = GetInt(stats, "atkLevel");
        aspLevel = GetInt(stats, "aspLevel");
        defLevel = GetInt(stats, "defLevel");

        essence = GetInt(snapshot.Child("currency"), "essence");
    }

    int GetInt(DataSnapshot snap, string key)
    {
        return snap.Child(key).Exists
            ? int.Parse(snap.Child(key).Value.ToString())
            : 0;
    }

    public bool UseEssence(int amount)
    {
        if (essence < amount) return false;
        essence -= amount;
        SaveCurrencyToDatabase();
        return true;
    }

    public void SaveStatsToDatabase()
    {
        string uid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        var data = new Dictionary<string, object>
        {
            { "hpLevel", hpLevel },
            { "atkLevel", atkLevel },
            { "aspLevel", aspLevel },
            { "defLevel", defLevel }
        };

        FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(uid).Child("playerStats").UpdateChildrenAsync(data);
    }

    public void SaveCurrencyToDatabase()
    {
        string uid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        var data = new Dictionary<string, object>
        {
            { "essence", essence }
        };

        FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(uid).Child("currency").UpdateChildrenAsync(data);
    }

}

