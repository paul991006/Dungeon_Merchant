using UnityEngine;
using System.Collections;

public class TimeRewardManager : MonoBehaviour
{
    [SerializeField] PlayerCurrency currency;

    const float TOTAL_REQUIRED_ESSENCE = 894000f;
    const float WEIGHT_SUM = 1275f;
    const float TOTAL_HOURS = 720f;
    const float SEGMENT_TIME = TOTAL_HOURS / 50f;

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(RewardLoop());
    }

    private void Start()
    {
        GiveOfflineReward();
    }

    IEnumerator RewardLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            GiveReward();
        }
    }

    void GiveReward()
    {
        int progressIndex = GetProgressIndex(); //1 ~ 50
        if (progressIndex <= 0) return;

        float essencePerHour = CalculateEssencePerHour(progressIndex);
        int reward = Mathf.Max(1, Mathf.FloorToInt(essencePerHour / 360f));

        if (reward > 0) currency.AddEssence(reward);
    }

    void GiveOfflineReward()
    {
        long now = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        long last = PlayerData.Instance.lastLogoutTime;

        long offlineSeconds = now - last;
        if (offlineSeconds <= 0) return;

        int progressIndex = GetProgressIndex();
        if (progressIndex <= 0) return;

        float essencePerHour = CalculateEssencePerHour(progressIndex);

        int reward = Mathf.FloorToInt(essencePerHour * (offlineSeconds / 3600f));

        if (reward > 0) currency.AddEssence(reward);

        PlayerData.Instance.lastLogoutTime = now;
        PlayerData.Instance.SaveLastLogoutTime();
    }


    float CalculateEssencePerHour(int progressIndex)
    {
        return (TOTAL_REQUIRED_ESSENCE * (progressIndex / WEIGHT_SUM)) / SEGMENT_TIME;
    }

    int GetProgressIndex()
    {
        int stage = Mathf.Max(1, PlayerData.Instance.maxClearedStage);
        int level = Mathf.Max(1, PlayerData.Instance.maxClearedLevel);

        return (stage - 1) * 10 + level;
    }
}
