using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    public static Action OnMonsterDead;

    public float maxHP = 30f;
    private float currentHP;

    private MonsterMovement movement;
    private Animator animator;
    private Collider2D col;
    private Rigidbody2D rb;

    private bool isDead = false;

    void Awake()
    {
        currentHP = maxHP;
        animator = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<MonsterMovement>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
            return;
        }

        if (animator != null) animator.SetTrigger("Hit");
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        if (movement != null) movement.StopMovement();

        if (animator != null)
        {
            animator.ResetTrigger("Hit");
            animator.SetTrigger("Die");
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        if (col != null) col.enabled = false;
        
        OnMonsterDead?.Invoke();

        Destroy(gameObject, 0.7f);
    }
}
