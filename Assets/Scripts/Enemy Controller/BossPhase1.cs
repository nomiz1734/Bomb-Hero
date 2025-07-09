using UnityEngine;

public class BossPhase1 : Enemy
{
    [SerializeField] private float followSpeed = 3f;

    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("IsFlying", true); 
        }
    }

    protected override void Update()
    {
        base.Update();
       
    }

  
   



    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (animator != null)
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

        // Gọi base.Die() để Destroy
        Invoke(nameof(DestroySelf), 1f); // Delay cho animation chết
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player != null)
        {
            Debug.LogWarning("abc");
            player.TakeDamage(enterDamage);
           
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player != null)
        {
            player.TakeDamage(stayDamage * Time.deltaTime);
        }
    }
}
