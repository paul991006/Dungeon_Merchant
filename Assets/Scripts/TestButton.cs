using UnityEngine;
using UnityEngine.SceneManagement;

public class TestButton : MonoBehaviour
{
    public void OnClickClear()
    {
        int stage = DungeonSelectionData.Stage;
        int level = DungeonSelectionData.Level;

        //개별 레벨 클리어 처리
        DungeonProgressManager.Instance.SetCleared(stage, level);

        // 던전 클리어 처리
        PlayerData.Instance.UpdateDungeonProgress(stage, level);

        Debug.Log($"던전 클리어: Stage {stage} / Level {level}");

        // 메인 씬으로 이동
        SceneManager.LoadScene("02_Main");
    }
}
