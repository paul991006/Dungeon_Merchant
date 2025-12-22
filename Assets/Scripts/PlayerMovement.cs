using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float jumpForce = 7.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool grounded = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public bool IsGrounded()
    {
        return grounded;
    }

    public bool IsIdle()
    {
        return Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        // 이동
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);

        // 방향
        if (x != 0)
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && grounded && CanJump())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetTrigger("Jump");
            grounded = false;
            anim.SetBool("Grounded", false);
        }

        bool CanJump()
        {
            if (!grounded) return false;
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) return false;
            if (anim.IsInTransition(0)) return false;
            return true;

        }

        bool isAttacking = anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
 
        if (isAttacking)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // 애니메이션
        anim.SetFloat("AirSpeedY", rb.linearVelocity.y);
        anim.SetInteger("AnimState", Mathf.Abs(x) > 0 ? 1 : 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            grounded = true;
            anim.SetBool("Grounded", true);
        }
    }
}
