using UnityEngine;
using UnityEngine.SceneManagement;

public class TestButton : MonoBehaviour
{
    public ItemData[] dropItems;

    public void OnClickClear()
    {
        int stage = DungeonSelectionData.Stage;
        int level = DungeonSelectionData.Level;

        GiveDungeonReward(stage, level);

            //개별 레벨 클리어 처리
        DungeonProgressManager.Instance.SetCleared(stage, level);

            // 던전 클리어 처리
        PlayerData.Instance.UpdateDungeonProgress(stage, level);

            // 메인 씬으로 이동
        SceneManager.LoadScene("02_Main");
    }

    void GiveDungeonReward(int stage, int level)
    {
        if (dropItems == null || dropItems.Length == 0) return;

        int rewardCount = GetRewardCount(stage, level);

        for (int i = 0; i < rewardCount; i++)
        {
            ItemData dropItem = dropItems[Random.Range(0, dropItems.Length)];

            ItemGrade grade = RollGradeByDungeon(stage, level);

            InventoryManager.Instance.AddItem(dropItem, grade);
        }

    }

    int GetRewardCount(int stage, int level)
    {
        int difficulty = stage * 10 + level;

        if (difficulty <= 15) return Random.Range(1, 3);
        else if (difficulty <= 30) return Random.Range(2, 5);
        else return Random.Range(3, 7);
    }

    ItemGrade RollGradeByDungeon(int stage, int level)
    {
        int difficulty = stage * 10 + level;
        int roll = Random.Range(0, 100);

            // 초반 던전
        if (difficulty <= 15)
        {
            if (roll < 70) return ItemGrade.Common;
            if (roll < 90) return ItemGrade.Rare;
            return ItemGrade.Epic;
        }
            // 중반 던전
        else if (difficulty <= 30)
        {
            if (roll < 50) return ItemGrade.Common;
            if (roll < 80) return ItemGrade.Rare;
            if (roll < 95) return ItemGrade.Epic;
            return ItemGrade.Unique;
        }
            // 후반 던전
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
