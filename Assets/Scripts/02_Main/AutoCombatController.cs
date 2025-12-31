using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AutoCombatController : MonoBehaviour
{
    public LayerMask monsterLayer;    
   
    [SerializeField] private Transform attackPoint; 

    private Animator anim;
    private PlayerStats stats;
    public AttackHit attackHit;

    private float attackTimer;
    private int lastAttackIndex = -1;

    void Awake()
    {
        anim = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
        attackHit = GetComponentInChildren<AttackHit>();
    }

    void Update()
    {
        if (GameManager.Instance.combatMode != CombatMode.Main) return;

        attackTimer += Time.deltaTime;

        Transform autoTarget = FindClosestMonster();

        if (autoTarget == null) return;

        FaceTarget(autoTarget);

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
            MonsterMovement mm = hit.GetComponent<MonsterMovement>();
            
            if (mm == null) continue;

            if (!mm.CanFight()) continue;

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

        float scaleX = Mathf.Abs(transform.localScale.x);
        transform.localScale = new Vector3(
            dir > 0 ? scaleX : -scaleX,
            transform.localScale.y,
            transform.localScale.z
        );
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

    public void AE_AttackHit()
    {
        attackHit.AE_AttackHit();
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
