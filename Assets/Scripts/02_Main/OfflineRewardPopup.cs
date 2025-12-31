using UnityEngine;
using UnityEngine.UI;

public class OfflineRewardPopup : MonoBehaviour
{
    [SerializeField] Text rewardText;

    public void Show(int reward)
    {
        if (reward <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        rewardText.text = $"⚔ 오프라인 보상 ⚔\n" + $"<color=#5A2A2A>Essence +{reward:N0}</color>";

        gameObject.SetActive(true);
    }
}
