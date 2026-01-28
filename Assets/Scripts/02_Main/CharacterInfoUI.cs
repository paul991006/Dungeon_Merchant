using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour
{
    [Header("Stat Texts")]
    public Text hpText;
    public Text attackText;
    public Text attackSpeedText;
    public Text defenseText;
    public Text essenceText;

    [Header("Btn Texts")]
    public Text hpBtnText;
    public Text attackBtnText;
    public Text attackSpeedBtnText;
    public Text defenseBtnText;

    PlayerStats stats;
    PlayerCurrency currency;
    PlayerUpgradeManager upgradeManager;

    const int MAX_HP_LEVEL = 90;
    const int MAX_ATK_LEVEL = 90;
    const int MAX_ASP_LEVEL = 100;
    const int MAX_DEF_LEVEL = 90;

    void Start()
    {
        stats = FindFirstObjectByType<PlayerStats>();
        currency = FindFirstObjectByType<PlayerCurrency>();
        upgradeManager = FindFirstObjectByType<PlayerUpgradeManager>();
    }

    void Update()
    {
        if (stats == null || currency == null || upgradeManager == null) return;

        UpdateStatUI(
            "체력이",
            PlayerData.Instance.hpLevel,
            MAX_HP_LEVEL,
            stats.maxHp,
            stats.maxHp + 2,
            hpText,
            hpBtnText,
            lvl => upgradeManager.GetUpgradeCost(lvl)
        );

        UpdateStatUI(
            "공격력이",
            PlayerData.Instance.atkLevel,
            MAX_ATK_LEVEL,
            stats.attackPower,
            stats.attackPower + 1,
            attackText,
            attackBtnText,
            lvl => upgradeManager.GetUpgradeCost(lvl)
        );

        UpdateStatUI(
            "공격 속도가",
            PlayerData.Instance.aspLevel,
            MAX_ASP_LEVEL,
            stats.attackSpeed,
            stats.attackSpeed + 0.01f,
            attackSpeedText,
            attackSpeedBtnText,
            lvl => Mathf.RoundToInt(upgradeManager.GetUpgradeCost(lvl) * 1.3f),
            true
        );

        UpdateStatUI(
            "방어력이",
            PlayerData.Instance.defLevel,
            MAX_DEF_LEVEL,
            stats.defense,
            stats.defense + 1,
            defenseText,
            defenseBtnText,
            lvl => upgradeManager.GetUpgradeCost(lvl)
        );

        essenceText.text = $"골드 : {currency.gold}G    몬스터 정수 : {currency.essence}";
    }

    void UpdateStatUI(
        string statName,
        int currentLevel,
        int maxLevel,
        float currentValue,
        float nextValue,
        Text statText,
        Text buttonText,
        System.Func<int, int> costFunc,
        bool isFloat = false
    )
    {
        //최대치 처리
        if (currentLevel >= maxLevel)
        {
            statText.text = $"{statName} 최대치입니다";
            buttonText.text = "업그레이드\n완료";
            return;
        }

        int cost = costFunc(currentLevel + 1);

        statText.text = isFloat
            ? $"{currentValue:F2} → {nextValue:F2}"
            : $"{(int)currentValue} → {(int)nextValue}";

        buttonText.text = $"업그레이드\n{currency.essence} / {cost}";
    }
}

