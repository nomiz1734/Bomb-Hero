using System.Collections;
using UnityEngine;

public class BossPhase1 : Enemy
{
    [Header("Boss Settings")]
    [SerializeField] private float followSpeed = 3f;
    [SerializeField] private GameObject bossPhase2Prefab; // Prefab, không phải object trong scene
    [SerializeField] private GameObject spawnEffectPrefab;
    [SerializeField] private float offsetFromPlayer = 2f;

    private bool isDead = false;
    private Vector3 deathPosition;
    private Animator animator;
    private bool isAttacking = false;
    private Vector3 lastPosition;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("IsFlying", true);
        }

        lastPosition = transform.position;
    }

    protected override void Update()
    {
        if (isDead) return;

        base.Update();

        // Giữ boss ở Z = 0
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        FollowPlayer();

        isAttacking = (transform.position != lastPosition);
        lastPosition = transform.position;
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * followSpeed * Time.deltaTime;

        FlipEnemy();
    }

    protected void FlipEnemy()
    {
        if (player != null)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = player.transform.position.x > transform.position.x;
            }
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (!isAttacking && animator != null && currentHp > 0)
        {
            animator.SetTrigger("Hurt");
        }
    }

    protected override void Die()
    {
        if (isDead) return;
        isDead = true;

        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        deathPosition = transform.position;

        // Gọi hiệu ứng và sinh boss mới sau delay
        Invoke(nameof(SpawnNextPhaseAndDestroy), 2.5f);
    }

    private void SpawnNextPhaseAndDestroy()
    {
        if (spawnEffectPrefab != null)
        {
            Instantiate(spawnEffectPrefab, deathPosition, Quaternion.identity);
        }

        StartCoroutine(SpawnBossPhase2());
    }

    private IEnumerator SpawnBossPhase2()
    {
        yield return new WaitForSeconds(1f); // Delay sau hiệu ứng

        if (bossPhase2Prefab != null)
        {
            Vector3 spawnPos = deathPosition;
            spawnPos.z = 0; // Ép trục Z về 0

            GameObject newBoss = Instantiate(bossPhase2Prefab, spawnPos, Quaternion.identity);

            var boss2Script = newBoss.GetComponent<BossPhase2>();
            if (boss2Script != null)
            {
                boss2Script.enabled = true;
            }
        }

        Destroy(gameObject); // Huỷ boss1
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player p = collision.GetComponent<Player>();
            if (p != null)
            {
                p.TakeDamage(enterDamage);

                if (animator != null)
                {
                    animator.SetTrigger("Attack");
                    animator.SetBool("IsFlying", false);
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (animator != null)
            {
                animator.SetBool("IsFlying", true);
            }
        }
    }
}
