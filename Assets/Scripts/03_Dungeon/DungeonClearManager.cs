using System.Collections.Generic;
using UnityEngine;

public class DungeonClearManager : MonoBehaviour
{
    public static DungeonClearManager Instance;
    public ItemData[] dropItems;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ClearDungeon()
    {
        int stage = DungeonSelectionData.Stage;
        int level = DungeonSelectionData.Level;

        List<RewardItemResult> rewards = GiveDungeonReward(stage, level);

        DungeonProgressManager.Instance.SetCleared(stage, level);
        PlayerData.Instance.UpdateDungeonProgress(stage, level);

        RewardResultUI.Instance.Show(rewards);
    }

    List<RewardItemResult> GiveDungeonReward(int stage, int level)
    {
        List<RewardItemResult> results = new List<RewardItemResult>();

        if (dropItems == null || dropItems.Length == 0) return results;

        int rewardCount = GetRewardCount(stage, level);

        for (int i = 0; i < rewardCount; i++)
        {
            ItemData item = dropItems[Random.Range(0, dropItems.Length)];
            ItemGrade grade = RollGradeByDungeon(stage, level);

            InventoryManager.Instance.AddItem(item, grade);
            results.Add(new RewardItemResult(item, grade));
        }

        return results;
    }

    int GetRewardCount(int stage, int level)
    {
        int difficulty = stage * 10 + level;

        if (difficulty <= 15) return Random.Range(1, 3);
        if (difficulty <= 30) return Random.Range(2, 5);
        return Random.Range(3, 7);
    }

    ItemGrade RollGradeByDungeon(int stage, int level)
    {
        int difficulty = stage * 10 + level;
        int roll = Random.Range(0, 100);

        if (difficulty <= 15)
        {
            if (roll < 70) return ItemGrade.Common;
            if (roll < 90) return ItemGrade.Rare;
            return ItemGrade.Epic;
        }
        else if (difficulty <= 30)
        {
            if (roll < 50) return ItemGrade.Common;
            if (roll < 80) return ItemGrade.Rare;
            if (roll < 95) return ItemGrade.Epic;
            return ItemGrade.Unique;
        }
        else
        {
            if (roll < 30) return ItemGrade.Common;
            if (roll < 60) return ItemGrade.Rare;
            if (roll < 85) return ItemGrade.Epic;
            if (roll < 95) return ItemGrade.Unique;
            return ItemGrade.Legendary;
        }
    }
}