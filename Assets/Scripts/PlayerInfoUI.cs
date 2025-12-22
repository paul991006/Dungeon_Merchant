using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour
{
    [Header("Texts")]
    public Text hpText;
    public Text attackText;
    public Text attackSpeedText;
    public Text defenseText;

    private PlayerStats stats;

    void Start()
    {
        stats = FindFirstObjectByType<PlayerStats>();
        UpdateUI();
    }

    void Update()
    {
        UpdateUI(); // 스탯 변경 즉시 반영
    }

    void UpdateUI()
    {
        if (stats == null) return;

        hpText.text = $"캐릭터 체력 : {stats.currentHealth} / {stats.maxHealth}";
        attackText.text = $"캐릭터 공격력 : {stats.attackPower}";
        attackSpeedText.text = $"캐릭터 공격 속도 : {stats.attackSpeed}";
        defenseText.text = $"캐릭터 방어력 : {stats.defense}";
    }
}

