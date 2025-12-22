using UnityEngine;

[System.Serializable]
public class PlayerStatsData
{
    public int maxHp;
    public int attackDmg;
    public float attackSpeed;
    public int defense;

    public PlayerStatsData(int hp, int atk, float asp, int def)
    {
        maxHp = hp;
        attackDmg = atk;
        attackSpeed = asp;
        defense = def;
    }
}
