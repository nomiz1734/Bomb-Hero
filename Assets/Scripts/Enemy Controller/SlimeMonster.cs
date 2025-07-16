using UnityEngine;

public class SlimeMonster : Enemy
{
    private Animator animator;
    private Transform target;
    [SerializeField] private float moveSpeed = 2f;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (target != null && currentHp > 0)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Flip sprite
            if (direction.x != 0)
            {
                GetComponent<SpriteRenderer>().flipX = direction.x > 0;
            }
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (animator != null)
        {
            animator.SetTrigger("hurt");
        }
    }

    protected override void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("death");
        }

        // Delay huỷ slime để cho animation death chạy
        Destroy(gameObject, 0.8f);
    }
}
