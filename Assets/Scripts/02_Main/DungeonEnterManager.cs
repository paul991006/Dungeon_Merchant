using UnityEngine;

public class DungeonEnterManager : MonoBehaviour
{
    public static DungeonEnterManager Instance;

    public int currentStage;
    public int currentLevel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnterDungeon(int stage, int level)
    {
        currentStage = stage;
        currentLevel = level;
    }
}
