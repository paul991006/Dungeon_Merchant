using UnityEngine;

public class Monster : MonoBehaviour
{
    public float maxHP = 30f;
    private float currentHP;

    void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0) Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
