using UnityEngine;

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
        cleared[stage, level] = true;
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
}
