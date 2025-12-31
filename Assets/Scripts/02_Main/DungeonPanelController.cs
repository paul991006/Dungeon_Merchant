using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonPanelController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject stagePanel;
    [SerializeField] GameObject levelPanel;
    [SerializeField] LevelButton[] levelButtons;
    [SerializeField] StageButton[] stageButtons;

    private int currentStage = -1;

    DungeonProgressManager progress;

    private void Awake()
    {
        progress = DungeonProgressManager.Instance;
    }

    void OnEnable()
    {
        stagePanel.SetActive(true);
        levelPanel.SetActive(false);
        currentStage = -1;

        RefreshStageButtons();
    }

    public void OnClickStage(int stage)
    {
        if (!progress.IsStageUnlocked(stage))
        {
            Debug.Log("아직 잠긴 스테이지입니다.");
            return;
        }


        currentStage = stage;
        levelPanel.SetActive(true);

        RefreshLevelButtons();
    }

    public void OnClickLevel(int level)
    {
        if (currentStage < 0) return;

        if (!progress.IsLevelUnlocked(currentStage, level)) return;

        DungeonSelectionData.Stage = currentStage;
        DungeonSelectionData.Level = level;

        SceneManager.LoadScene("03_Dungeon");
    }

    void RefreshStageButtons()
    {
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stage = i + 1;
            bool unlocked = progress.IsStageUnlocked(stage);
            stageButtons[i].SetState(unlocked);
        }
    }


    void RefreshLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = i + 1;

            bool unlocked = progress.IsLevelUnlocked(currentStage, level);
            bool cleared = progress.IsCleared(currentStage, level);

            levelButtons[i].SetState(unlocked, cleared);
        }
    }
}
