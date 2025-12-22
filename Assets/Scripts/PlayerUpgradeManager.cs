using UnityEngine;

public class PlayerUpgradeManager : MonoBehaviour
{
    private PlayerStats stats;
    private PlayerCurrency currency;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        currency = GetComponent<PlayerCurrency>();
    }

    public int GetUpgradeCost(int level)
    {
        if (level <= 10) return 5 + level * 5;
        if (level <= 30) return 60 + (level - 10) * 20;
        if (level <= 60) return 460 + (level - 30) * 60;
        else return 2260 + (level - 60) * 150;
    }

    public void UpgradeHp()
    {
        TryUpgrade(ref stats.hpLevel, 90);
    }

    public void UpgradeAttack()
    {
        TryUpgrade(ref stats.atkLevel, 90);
    }

    public void UpgradeAttackSpeed()
    {
        TryUpgrade(ref stats.aspLevel, 100, 1.3f);
    }

    public void UpgradeDefense()
    {
        TryUpgrade(ref stats.defLevel, 90);
    }

    void TryUpgrade(ref int level, int maxLevel, float weight = 1f)
    {
        if (level >= maxLevel) return;

        int cost = Mathf.RoundToInt(GetUpgradeCost(level + 1) * weight);
        if (!currency.UseEssence(cost)) return;

        level++;
        stats.RecalculateStats();
        stats.SaveStatsToDatabase();
    }
}
