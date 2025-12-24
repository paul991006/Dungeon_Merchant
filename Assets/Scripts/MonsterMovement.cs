using System.Collections;
using System.Xml.Serialization;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform targetPoint; //몬스터가 설 위치
    public float arriveDelay = 1;

    private Animator animator;
    private Rigidbody2D rb;

    private bool isMoving = true;
    private bool canFight = false;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isMoving)
        {
            animator.SetBool("isRun", false);
            return;
        }

        if (targetPoint == null) return;

        float dirX = Mathf.Sign(targetPoint.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(dirX * moveSpeed, rb.linearVelocity.y);

        animator.SetBool("isRun", true);

        //이동 애니메이션
        if (animator != null) animator.SetBool("isRun", true);

        //도착 체크
        if (Mathf.Abs(transform.position.x - targetPoint.position.x) < 0.05f)
        {
            Arrive();
        }
    }

    void Arrive()
    {
        isMoving = false;

        rb.linearVelocity = Vector2.zero;

        if (animator != null) animator.SetBool("isRun", false);

        StartCoroutine(ArriveDelayCoroutine());
    }

    IEnumerator ArriveDelayCoroutine()
    {
        canFight = false;
        yield return new WaitForSeconds(arriveDelay);
        canFight = true;
    }

    public bool CanFight()
    {
        return canFight;
    }

    //몬스터 죽을 때 이동 중지
    public void StopMovement()
    {
        isMoving = false;
        canFight = false;
        if (rb != null) rb.linearVelocity = Vector2.zero;
    }
}
