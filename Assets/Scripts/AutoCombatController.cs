using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AutoCombatController : MonoBehaviour
{
    public LayerMask monsterLayer;    
   
    [SerializeField] private Transform attackPoint; 

    private Animator anim;
    private PlayerStats stats;
    private PlayerMovement movement;

    private float attackTimer;
    private int lastAttackIndex = -1;

    void Awake()
    {
        anim = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        Transform autoTarget = FindClosestMonster();

        if (autoTarget != null) FaceTarget(autoTarget);

        if (!CanAttack()) return;

        bool manualAttack = Input.GetMouseButtonDown(0);

        if (!manualAttack && autoTarget == null) return;

        //쿨타임 체크
        if (attackTimer < 1f / stats.attackSpeed) return;

        TriggerRandomAttack();
        attackTimer = 0f;
    }

    Transform FindClosestMonster()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            stats.attackRange,
            monsterLayer
        );

        float minDist = float.MaxValue;
        Transform closest = null;

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(attackPoint.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = hit.transform;
            }
        }

        return closest;
    }

    void FaceTarget(Transform target)
    {
        float dir = target.position.x - transform.position.x;
        if (Mathf.Abs(dir) < 0.01f) return;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(dir) * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    void TriggerRandomAttack()
    {
        int attackIndex;

        //같은 공격 연속 방지
        do attackIndex = Random.Range(1, 4);
        while (attackIndex == lastAttackIndex);

        lastAttackIndex = attackIndex;

        anim.SetTrigger($"Attack{attackIndex}");
    }

    bool CanAttack()
    {
        if (!movement.IsGrounded()) return false;
        if (!movement.IsIdle()) return false;
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) return false;
        if (anim.IsInTransition(0)) return false;
        return true;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        PlayerStats s = GetComponent<PlayerStats>();
        if (s == null || attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, s.attackRange);
    }
#endif
}
