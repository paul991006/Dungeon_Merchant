using UnityEngine;

public class AttackHit : MonoBehaviour
{
    public LayerMask monsterLayer;

    private PlayerStats stats;

    void Awake()
    {
        stats = GetComponentInParent<PlayerStats>();
    }

    //Animation Event
    public void AE_AttackHit()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            stats.attackRange,
            monsterLayer
        );

        foreach (var hit in hits)
        {
            Monster m = hit.GetComponent<Monster>();
            if (m == null) continue;

            MonsterMovement mm = hit.GetComponent<MonsterMovement>();
            if (mm != null && !mm.CanFight()) continue;

            m.TakeDamage(stats.attackPower);
        }
    }
}
