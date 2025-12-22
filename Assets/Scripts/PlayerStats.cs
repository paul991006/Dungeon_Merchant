using Firebase.Auth;
using Firebase.Database;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public int maxHp;
    public int attackPower;
    public float attackSpeed;
    public int defense;
    public int currentHp;

    [Header("Levels")]
    public int hpLevel;
    public int atkLevel;
    public int aspLevel;
    public int defLevel;

    public float attackRange = 1.5f;

    private DatabaseReference db;
    private string uid;

    void Awake()
    {
        uid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        db = FirebaseDatabase.DefaultInstance.RootReference;

        LoadStatsFromDatabase();
    }

    public void LoadStatsFromDatabase()
    {
        db.Child("users").Child(uid).Child("playerStats").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogWarning("Failed to load player stats from database. Using defaults.");
                    SetDefaultLevels();
                    RecalculateStats(true);
                    return;
                }

                if (!task.Result.Exists)
                {
                    SetDefaultLevels();
                    SaveStatsToDatabase();
                    RecalculateStats(true);
                    return;
                }

                var s = task.Result;

                hpLevel = s.Child("hpLevel").Exists ? int.Parse(s.Child("hpLevel").Value.ToString()) : 0;
                atkLevel = s.Child("atkLevel").Exists ? int.Parse(s.Child("atkLevel").Value.ToString()) : 0;
                aspLevel = s.Child("aspLevel").Exists ? int.Parse(s.Child("aspLevel").Value.ToString()) : 0;
                defLevel = s.Child("defLevel").Exists ? int.Parse(s.Child("defLevel").Value.ToString()) : 0;

                RecalculateStats(true);
            });
    }

    public void SaveStatsToDatabase()
    {
        var data = new Dictionary<string, object>
        {
            { "hpLevel", hpLevel },
            { "atkLevel", atkLevel },
            { "aspLevel", aspLevel },
            { "defLevel", defLevel }
        };

        //기존 playerStats 외 다른 데이터 덮어쓰기 방지
        db.Child("users").Child(uid).Child("playerStats").UpdateChildrenAsync(data);
    }

    void SetDefaultLevels()
    {
        hpLevel = 0;
        atkLevel = 0;
        aspLevel = 0;
        defLevel = 0;
    }

    public void RecalculateStats(bool fullHeal = false)
    {
        int prevMaxHp = maxHp;

        maxHp = Mathf.Min(20 + hpLevel * 2, 200);
        attackPower = Mathf.Min(10 + atkLevel * 1, 100);
        attackSpeed = Mathf.Min(1f + aspLevel * 0.01f, 2f);
        defense = Mathf.Min(10 + defLevel * 1, 100);

        if (fullHeal || currentHp <= 0) currentHp = maxHp;
        else currentHp += (maxHp - prevMaxHp);
    }

    public void TakeDamage(int damage)
    {
        int finalDamage = Mathf.Max(damage - defense, 1);
        currentHp -= finalDamage;

        if (currentHp <= 0)
        {
            currentHp = 0;
            Debug.Log("Player has died.");
        }
    }

    [System.Serializable]
    public class PlayerStatsLevelData
    {
        public int hpLevel;
        public int atkLevel;
        public int aspLevel;
        public int defLevel;

        public PlayerStatsLevelData(int hp, int atk, int atkSpd, int def)
        {
            hpLevel = hp;
            atkLevel = atk;
            aspLevel = atkSpd;
            defLevel = def;
        }
    }
}
