using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour
{
    [Header("Texts")]
    public Text hpText;
    public Text attackText;
    public Text attackSpeedText;
    public Text defenseText;
    public Text essenceText;

    [Header("Texts")]
    public Text hpBtnText;
    public Text attackBtnText;
    public Text attackSpeedBtnText;
    public Text defenseBtnText;

    [Header("Upgrade Costs")]
    public int hpUpgradeCost;
    public int attackUpgradeCost;
    public int attackSpeedUpgradeCost;
    public int defenseUpgradeCost;

    PlayerStats stats;
    PlayerCurrency currency;
    PlayerUpgradeManager upgradeManager;

    void Start()
    {
        stats = FindFirstObjectByType<PlayerStats>();
        currency = FindFirstObjectByType<PlayerCurrency>();
        upgradeManager = FindFirstObjectByType<PlayerUpgradeManager>();
    }

    void Update()
    {
        //각 스탯 다음 레벨 기준 비용 계산
        int hpCost = upgradeManager.GetUpgradeCost(stats.hpLevel + 1);
        int atkCost = upgradeManager.GetUpgradeCost(stats.atkLevel + 1);
        int aspCost = Mathf.RoundToInt(upgradeManager.GetUpgradeCost(stats.aspLevel + 1) * 1.3f);
        int defCost = upgradeManager.GetUpgradeCost(stats.defLevel + 1);

        hpText.text = $"{stats.maxHp} -> {stats.maxHp + 2}";
        attackText.text = $"{stats.attackPower} -> {stats.attackPower + 1}";
        attackSpeedText.text = $"{stats.attackSpeed:F2} -> {(stats.attackSpeed + 0.01f):F2}";
        defenseText.text = $"{stats.defense} -> {stats.defense + 1}";
        hpBtnText.text = $"업그레이드\n{currency.essence} / {hpCost}";
        attackBtnText.text = $"업그레이드\n{currency.essence} / {atkCost}";
        attackSpeedBtnText.text = $"업그레이드\n{currency.essence} / {aspCost}";
        defenseBtnText.text = $"업그레이드\n{currency.essence} / {defCost}";
        essenceText.text = $"{currency.essence}";
    }
}

