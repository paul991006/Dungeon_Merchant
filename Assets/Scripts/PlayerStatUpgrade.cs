using UnityEngine;

public class PlayerStatUpgrade : MonoBehaviour
{
    private PlayerStats stats;

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    public void UpgradeHp()
    {
        stats.maxHp += 2;
        stats.currentHp = stats.maxHp;
        stats.SaveStatsToDatabase();
    }

    public void UpgradeAttack()
    {
        stats.attackPower += 1;
        stats.SaveStatsToDatabase();
    }

    public void UpgradeAttackSpeed()
    {
        stats.attackSpeed += 0.01f;
        stats.SaveStatsToDatabase();
    }

    public void UpgradeDefense()
    {
        stats.defense += 1;
        stats.SaveStatsToDatabase();
    }
}
