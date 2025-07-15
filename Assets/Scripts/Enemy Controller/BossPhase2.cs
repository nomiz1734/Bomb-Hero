using UnityEngine;

public class BossPhase2 : Enemy
{
    [Header("Movement & Attack")]
    [SerializeField] private float followSpeed = 3f;

    [Header("Meteor Settings")]
    public GameObject meteorPrefab;

    [Header("Slime Spawn Settings")]
    public GameObject slimePrefab;
    public float slimeSpawnInterval = 15f;
    public int slimeSpawnCount = 3;
    public float slimeSpawnRadius = 2f;

    private Animator animator;
    private Vector3 lastPosition;
    private bool isAttacking = false;
    private bool isPreparingAttack = false;
    private bool lastWalkState = false;
    [Header("Visual Effect")]
    public GameObject flameEffectPrefab;
    private GameObject flameInstance;
    // Giai đoạn máu
    private bool phase80Triggered = false;
    private bool phase40Triggered = false;
    private bool phase10Triggered = false;
    private bool flameStarted = false;
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
        InvokeRepeating(nameof(SpawnSlimes), 5f, slimeSpawnInterval);

    }


    protected override void Update()
    {
        base.Update();

        // Kiểm tra máu để chuyển phase
        float hpPercent = currentHp / maxHp;
        // Gọi flame circle khi còn dưới 70% máu
        if (!flameStarted && hpPercent <= 0.7f)
        {
            flameStarted = true;

            if (flameEffectPrefab != null && flameInstance == null)
            {
                flameInstance = Instantiate(flameEffectPrefab, transform);
                flameInstance.transform.localPosition = Vector3.zero;
            }
        }

        if (!phase80Triggered && hpPercent <= 0.8f)
        {
            phase80Triggered = true;
            CancelInvoke(nameof(CastMeteor));
            InvokeRepeating(nameof(CastMeteor), 1f, 8f);
        }

        if (!phase40Triggered && hpPercent <= 0.4f)
        {
            phase40Triggered = true;
            CancelInvoke(nameof(CastMeteor));
            InvokeRepeating(nameof(CastMeteor), 1f, 4f);
        }

        if (!phase10Triggered && hpPercent <= 0.1f)
        {
            phase10Triggered = true;
            CancelInvoke(nameof(CastMeteor));
            InvokeRepeating(nameof(CastMeteor), 1f, 1f);
        }

        // Giữ Z = 0
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        if (!isAttacking && !isPreparingAttack)
        {
            FollowPlayer();
        }

        bool isMoving = Vector3.Distance(transform.position, lastPosition) > 0.01f;
        lastPosition = transform.position;

        bool shouldWalk = isMoving && !isAttacking && !isPreparingAttack &&
                          !IsPlayingAnimation("d_cleave") && !IsPlayingAnimation("d_take_hit");

        if (shouldWalk != lastWalkState)
        {
            animator.SetBool("d_walk", shouldWalk);
            lastWalkState = shouldWalk;
        }
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * followSpeed * Time.deltaTime;

        FlipEnemy();
    }

    private void FlipEnemy()
    {
        if (player == null) return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.flipX = player.transform.position.x > transform.position.x;
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (animator != null && currentHp > 0 && !isAttacking && !IsPlayingAnimation("d_cleave"))
        {
            animator.ResetTrigger("d_cleave");
            animator.SetTrigger("d_take_hit");
        }
    }

    protected override void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("death");
        }

        if (flameInstance != null)
        {
            Destroy(flameInstance);
        }

        CancelInvoke();
        Invoke(nameof(DestroySelf), 1f);
    }


    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isAttacking && !isPreparingAttack)
        {
            isPreparingAttack = true;
            Invoke(nameof(TryAttack), 0.5f);
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

    private void TryAttack()
    {
        isPreparingAttack = false;

        if (!isAttacking && !IsPlayingAnimation("d_cleave"))
        {
            isAttacking = true;

            if (animator != null)
            {
                animator.ResetTrigger("d_take_hit");
                animator.SetTrigger("d_cleave");
            }

            if (player != null)
            {
                player.TakeDamage(enterDamage);
            }

            Invoke(nameof(ResetAttack), 1f);
        }
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }

    private bool IsPlayingAnimation(string animName)
    {
        if (animator == null) return false;

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        return state.IsName(animName) && state.normalizedTime < 1f;
    }

    public void CastMeteor()
    {
        if (player == null || meteorPrefab == null) return;

        Vector3 targetPos = player.transform.position;
        Vector3 spawnPos = new Vector3(targetPos.x, targetPos.y + 6f, 0f);

        GameObject meteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);

        Meteor m = meteor.GetComponent<Meteor>();
        if (m != null)
        {
            m.explodeY = targetPos.y;
        }
    }
    private void SpawnSlimes()
    {
        if (slimePrefab == null) return;

        for (int i = 0; i < slimeSpawnCount; i++)
        {
            Vector2 offset = Random.insideUnitCircle.normalized * slimeSpawnRadius;
            Vector3 spawnPos = transform.position + new Vector3(offset.x, offset.y, 0);
            Instantiate(slimePrefab, spawnPos, Quaternion.identity);
        }
    }

}
