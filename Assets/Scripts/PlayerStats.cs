using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 20;
    public int attackPower = 10;
    public float attackSpeed = 1.0f;
    public int defense = 10;
    public float attackRange = 1.5f;
    public int currentHealth;

    void Awake()
    {
        LoadStats();
        currentHealth = maxHealth;
    }

    private void OnApplicationQuit()
    {
        SaveStats();
    }

    public void TakeDamage(int damage)
    {
        int finalDamage = Mathf.Max(damage - defense, 1);
        currentHealth -= finalDamage;
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player has died.");
        }
    }

    public void SaveStats()
    {
        PlayerPrefs.SetInt("Player_MaxHp", maxHealth);
        PlayerPrefs.SetInt("Player_AttackPower", attackPower);
        PlayerPrefs.SetFloat("Player_AttackSpeed", attackSpeed);
        PlayerPrefs.SetInt("Player_Defense", defense);
        PlayerPrefs.Save();
    }

    public void LoadStats()
    {
        maxHealth = PlayerPrefs.GetInt("Player_MaxHp", 20);
        attackPower = PlayerPrefs.GetInt("Player_AttackPower", 10);
        attackSpeed = PlayerPrefs.GetFloat("Player_AttackSpeed", 1f);
        defense = PlayerPrefs.GetInt("Player_Defense", 10);
    }
}
