using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCombatController : MonoBehaviour
{
    public LayerMask monsterLayer;
    [SerializeField] private Transform attackPoint;

    private Animator anim;
    private PlayerStats stats;

    private float attackTimer;
    private int lastAttackIndex = -1;

    void Awake()
    {
        anim = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        //던전 씬이 아닐 경우 실행 안 함
        if (GameManager.Instance.combatMode != CombatMode.Dungeon)
            return;

        attackTimer += Time.deltaTime;

        if (!Input.GetMouseButton(0)) return;

        //공격속도 기반 쿨타임
        if (attackTimer < 1f / stats.attackSpeed) return;

        TriggerRandomAttack();
        attackTimer = 0f;
    }

    void TriggerRandomAttack()
    {
        int attackIndex;
        do
        {
            attackIndex = Random.Range(1, 4); //Attack1 ~ Attack3
        }
        while (attackIndex == lastAttackIndex);

        lastAttackIndex = attackIndex;
        anim.SetTrigger($"Attack{attackIndex}");
    }
}
