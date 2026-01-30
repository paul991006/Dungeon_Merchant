using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardResultUI : MonoBehaviour
{
    public static RewardResultUI Instance;

    [SerializeField] GameObject panel;
    [SerializeField] Transform content;
    [SerializeField] RewardItemSlotUI slotPrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Show(List<RewardItemResult> rewards)
    {
        ClearSlots();

        foreach (var reward in rewards)
        {
            var slot = Instantiate(slotPrefab, content);
            slot.Set(reward);
        }

        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ClearSlots()
    {
        foreach (Transform child in content) Destroy(child.gameObject);
    }

    public void OnClickConfirm()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("02_Main");
    }
}
