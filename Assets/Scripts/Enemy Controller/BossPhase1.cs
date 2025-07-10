using UnityEngine;

public class BossPhase1 : Enemy
{
    [SerializeField] private float followSpeed = 3f;

    private Animator animator;
    private bool isAttacking = false;
    private Vector3 lastPosition;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("IsFlying", true); // Boss bay mặc định
        }

        lastPosition = transform.position;
    }

    protected override void Update()
    {
        base.Update();

        // Luôn giữ boss ở trục Z = 0 để không bị mất hình
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        FollowPlayer();

        // Kiểm tra xem boss có đang di chuyển (tức là đang tấn công) hay không
        isAttacking = (transform.position != lastPosition);
        lastPosition = transform.position;
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * followSpeed * Time.deltaTime;

        // Đã bỏ FlipEnemy (boss không quay mặt)
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        // Không play animation Hurt nếu đang di chuyển hoặc boss đã chết
        if (!isAttacking && animator != null && currentHp > 0)
        {
            animator.SetTrigger("Hurt");
        }
    }

    protected override void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        // Delay để animation chết hiển thị xong
        Invoke(nameof(DestroySelf), 1f);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player p = collision.GetComponent<Player>();
            if (p != null)
            {
                p.TakeDamage(enterDamage);

                // Gọi animation Attack
                if (animator != null)
                {
                    animator.SetTrigger("Attack");
                    animator.SetBool("IsFlying", false); // Tạm ngừng bay khi attack
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player p = collision.GetComponent<Player>();
            if (p != null)
            {
                p.TakeDamage(stayDamage * Time.deltaTime);
            }
        }
    }
}
