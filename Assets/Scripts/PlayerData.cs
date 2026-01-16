using Firebase.Auth;
using Firebase.Database;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public List<EquippedItemData> equippedItems = new();

    const string EQUIP_SAVE_KEY = "EQUIPMENT_DATA";

    public int hpLevel;
    public int atkLevel;
    public int aspLevel;
    public int defLevel;
    public int essence;
    public int maxClearedStage;
    public int maxClearedLevel;
    public long lastLogoutTime;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadEquipment();
        }
        else Destroy(gameObject);
    }

    public void SaveEquipment()
    {
        EquipmentSaveData save = new EquipmentSaveData
        {
            equippedItems = equippedItems
        };

        string json = JsonUtility.ToJson(save);
        PlayerPrefs.SetString(EQUIP_SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    public void LoadEquipment()
    {
        if (!PlayerPrefs.HasKey(EQUIP_SAVE_KEY)) return;

        string json = PlayerPrefs.GetString(EQUIP_SAVE_KEY);
        EquipmentSaveData save = JsonUtility.FromJson<EquipmentSaveData>(json);

        equippedItems = save.equippedItems ?? new();
    }

    public void LoadFromSnapshot(DataSnapshot snapshot)
    {
        var stats = snapshot.Child("playerStats");

        hpLevel = GetInt(stats, "hpLevel");
        atkLevel = GetInt(stats, "atkLevel");
        aspLevel = GetInt(stats, "aspLevel");
        defLevel = GetInt(stats, "defLevel");

        essence = GetInt(snapshot.Child("currency"), "essence");

        var dungeon = snapshot.Child("dungeonProgress");

        maxClearedStage = GetInt(dungeon, "maxClearedStage");
        maxClearedLevel = GetInt(dungeon, "maxClearedLevel");

        lastLogoutTime = snapshot.Child("meta").Child("lastLogoutTime").Exists
            ? long.Parse(snapshot.Child("meta").Child("lastLogoutTime").Value.ToString())
            : GetNowUnixTime();
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

    public void UpdateDungeonProgress(int stage, int level)
    {
        //더 낮은 기록이면 무시
        if (stage < maxClearedStage) return;
        if (stage == maxClearedStage && level <= maxClearedLevel) return;

        maxClearedStage = stage;
        maxClearedLevel = level;

        SaveDungeonProgressToDatabase();
    }

    void SaveDungeonProgressToDatabase()
    {
        string uid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        var data = new Dictionary<string, object>
        {
            { "maxClearedStage", maxClearedStage },
            { "maxClearedLevel", maxClearedLevel }
        };

        FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(uid).Child("dungeonProgress").UpdateChildrenAsync(data);
    }

    public void SaveLastLogoutTime()
    {
        lastLogoutTime = GetNowUnixTime();

        string uid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(uid).Child("meta").Child("lastLogoutTime").SetValueAsync(lastLogoutTime);
    }

    long GetNowUnixTime()
    {
        return System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    void OnApplicationQuit()
    {
        SaveLastLogoutTime();
    }

    void OnApplicationPause(bool pause)
    {
        if (pause) SaveLastLogoutTime();
    }
}