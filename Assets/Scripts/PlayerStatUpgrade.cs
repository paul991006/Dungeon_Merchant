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
        stats.maxHealth += 2;
        stats.currentHealth = stats.maxHealth;
        stats.SaveStats();
    }

    public void UpgradeAttack()
    {
        stats.attackPower += 1;
        stats.SaveStats();
    }

    public void UpgradeAttackSpeed()
    {
        stats.attackSpeed += 0.01f;
        stats.SaveStats();
    }

    public void UpgradeDefense()
    {
        stats.defense += 1;
        stats.SaveStats();
    }
}
