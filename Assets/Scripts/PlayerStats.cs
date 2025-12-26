using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public int maxHp;
    public int attackPower;
    public float attackSpeed;
    public int defense;
    public int currentHp;

    [Header("Levels")]
    public int hpLevel;
    public int atkLevel;
    public int aspLevel;
    public int defLevel;

    public float attackRange = 1.5f;

    void Start()
    {
        ApplyFromPlayerData();
    }

    //PlayerData ¡æ PlayerStats ¹Ý¿µ
    public void ApplyFromPlayerData(bool fullHeal = true)
    {
        hpLevel = PlayerData.Instance.hpLevel;
        atkLevel = PlayerData.Instance.atkLevel;
        aspLevel = PlayerData.Instance.aspLevel;
        defLevel = PlayerData.Instance.defLevel;

        RecalculateStats(fullHeal);
    }

    public void RecalculateStats(bool fullHeal = false)
    {
        int prevMaxHp = maxHp;

        maxHp = Mathf.Min(20 + hpLevel * 2, 200);
        attackPower = Mathf.Min(10 + atkLevel * 1, 100);
        attackSpeed = Mathf.Min(1f + aspLevel * 0.01f, 2f);
        defense = Mathf.Min(10 + defLevel * 1, 100);

        if (fullHeal || currentHp <= 0) currentHp = maxHp;
        else currentHp += (maxHp - prevMaxHp);
    }

    public void TakeDamage(int damage)
    {
        int finalDamage = Mathf.Max(damage - defense, 1);
        currentHp -= finalDamage;

        if (currentHp <= 0)
        {
            currentHp = 0;
            Debug.Log("Player has died.");
        }
    }

    [System.Serializable]
    public class PlayerStatsLevelData
    {
        public int hpLevel;
        public int atkLevel;
        public int aspLevel;
        public int defLevel;

        public PlayerStatsLevelData(int hp, int atk, int atkSpd, int def)
        {
            hpLevel = hp;
            atkLevel = atk;
            aspLevel = atkSpd;
            defLevel = def;
        }
    }
}
