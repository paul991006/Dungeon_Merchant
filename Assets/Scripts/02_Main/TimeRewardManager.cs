using UnityEngine;
using System.Collections;

public class TimeRewardManager : MonoBehaviour
{
    public static TimeRewardManager Instance;

    [SerializeField] PlayerCurrency currency;
    [SerializeField] OfflineRewardPopup offlineRewardPopup;

    public int lastOfflineReward { get; private set; }

    const float TOTAL_REQUIRED_ESSENCE = 894000f;
    const float WEIGHT_SUM = 1275f;
    const float TOTAL_HOURS = 720f;
    const float SEGMENT_TIME = TOTAL_HOURS / 50f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(RewardLoop());
    }

    IEnumerator RewardLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            GiveReward();
        }
    }

    private void Start()
    {
        StartCoroutine(WaitAndGiveOfflineReward());
    }

    IEnumerator WaitAndGiveOfflineReward()
    {
        yield return new WaitUntil(() => PlayerData.Instance != null);
        yield return new WaitUntil(() => PlayerData.Instance.isLoaded);

        TryGiveOfflineReward();
    }

    void GiveReward()
    {
        int progressIndex = GetProgressIndex(); //1 ~ 50
        if (progressIndex <= 0) return;

        float essencePerHour = CalculateEssencePerHour(progressIndex);
        int reward = Mathf.Max(1, Mathf.FloorToInt(essencePerHour / 360f));

        currency.AddEssence(reward);
    }

    void GiveOfflineReward()
    {
        lastOfflineReward = 0;

        long now = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        long last = PlayerData.Instance.lastLogoutTime;

        long offlineSeconds = now - last;
        if (offlineSeconds <= 0) return;

        int progressIndex = GetProgressIndex();
        if (progressIndex <= 0) return;

        float essencePerHour = CalculateEssencePerHour(progressIndex);

        int reward = Mathf.FloorToInt(essencePerHour * (offlineSeconds / 3600f));

        if (reward <= 0) return;

        lastOfflineReward = reward;
        currency.AddEssence(reward);
        offlineRewardPopup.Show(reward);

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

    public void TryGiveOfflineReward()
    {
        if (PlayerData.Instance.offlineRewardChecked) return;

        GiveOfflineReward();

        PlayerData.Instance.offlineRewardChecked = true;
        PlayerData.Instance.SaveOfflineRewardChecked();
    }
}
