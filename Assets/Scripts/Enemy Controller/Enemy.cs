using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float enemyMoveSpeed = 1f;
    protected Player player;

    [SerializeField] protected float maxHp = 50f;
    protected float currentHp;
    [SerializeField] private Image hpBar;
    [SerializeField] protected float enterDamage = 10f;
    [SerializeField] protected float stayDamage = 1f;
    [SerializeField] protected int expToGive = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        player = FindAnyObjectByType<Player>();
        currentHp = maxHp;
        UpdateHpBar();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        MoveToPlayer();
    }

    protected void MoveToPlayer()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyMoveSpeed * Time.deltaTime);
            FlipEnemy();
        }
    }

    protected void FlipEnemy()
    {
        if (player != null)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = player.transform.position.x < transform.position.x;
            }
        }
    }

    public virtual void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();
        if (currentHp <= 0)
        {
            Die();
        }
        
    }

    protected virtual void Die()
    {
        float rand = Random.value;
        if (rand < 0.1f)
        {
            CoinController.instance.SpawnPickup(transform.position, 1);
        }
        else
        {           
             EXPLVController.instance.SpawnPickup(transform.position, expToGive);
        }
        Destroy(gameObject);
    }
    protected void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp/maxHp;
        }
    }
}
