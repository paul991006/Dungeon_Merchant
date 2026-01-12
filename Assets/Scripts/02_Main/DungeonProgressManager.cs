using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using System.Collections.Generic;

public class DungeonProgressManager : MonoBehaviour
{
    public static DungeonProgressManager Instance;

    public bool[,] cleared = new bool[6, 11];

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

    public bool IsCleared(int stage, int level)
    {
        return cleared[stage, level];
    }

    public void SetCleared(int stage, int level)
    {
        if (cleared[stage, level]) return;

        cleared[stage, level] = true;

        SaveClearedToDatabase(stage, level);
    }

    void SaveClearedToDatabase(int stage, int level)
    {
        string uid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        var updates = new Dictionary<string, object>
        {
            { $"{stage}_{level}", true }
        };

        FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(uid).Child("dungeonProgress").Child("cleared").UpdateChildrenAsync(updates);
    }

    public void LoadFromSnapshot(DataSnapshot snapshot)
    {
        cleared = new bool[6, 11];
        var clearedSnapshot = snapshot.Child("dungeonProgress").Child("cleared");

        if (!clearedSnapshot.Exists) return;

        foreach (var child in clearedSnapshot.Children)
        {
            string[] parts = child.Key.Split('_');
            if (parts.Length != 2) continue;

            int stage = int.Parse(parts[0]);
            int level = int.Parse(parts[1]);

            if (stage >= 1 && stage <= 5 && level >= 1 && level <= 10)
            {
                cleared[stage, level] = true;
            }
        }
    }

    public bool IsStageUnlocked(int stage)
    {
        if (stage == 1) return true;

        for (int level = 1; level <= 10; level++)
        {
            if (!cleared[stage - 1, level]) return false;
        }

        return true;
    }

    public bool IsLevelUnlocked(int stage, int level)
    {
        if (!IsStageUnlocked(stage)) return false;

        if (level == 1) return true;

        return cleared[stage, level - 1];
    }

    public int GetCurrentProgressStage()
    {
        int currentStage = 1;

        for (int stage = 1; stage <= 5; stage++)
        {
            bool anyCleared = false;

            for (int level = 1; level <= 10; level++)
            {
                if (cleared[stage, level])
                {
                    anyCleared = true;
                    currentStage = stage;
                }
            }

            if (!anyCleared) break;
        }

        return currentStage;
    }
}
