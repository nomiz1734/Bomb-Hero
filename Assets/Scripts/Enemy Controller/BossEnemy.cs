using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speedNormalBullet = 20f;
    [SerializeField] private float speedRoundBullet = 10f;
    [SerializeField] private float hpValue= 50f;
    [SerializeField] private GameObject miniEnemyPrefab;
    [SerializeField] private float skillCooldown = 2f;
    [SerializeField] private GameObject endGameOrb; 
    private float skillTimer = 0f;
    private bool isFirstTime = true;
    private int level;
    private int nextLevel;
    protected override void Update()
    {
        base.Update();
        if (Time.time >= skillTimer){
            UseSkill();
        }
    }
    protected override void Start()
    {
        base.Start();
        level = SaveSystem.GetInt("Level", 1);
        nextLevel = level + 1;
        if (nextLevel > 4)
        {
            nextLevel = 4;
        }
    }
    protected override void Die()
    {
        Instantiate(endGameOrb, transform.position, Quaternion.identity);
        isFirstTime = SaveSystem.GetBool(level+ "_Completed", true);
        if (isFirstTime)
        {
            SaveSystem.SetInt(level+ "_Completed", 1);
            SaveSystem.SetInt(nextLevel+ "_Unlocked", 1);
            SaveSystem.SetInt("Level", nextLevel);
            SaveSystem.SaveToDisk();
        }
        base.Die();

    }
    private void OnEnable()
    {
        Teleport();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(enterDamage);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(stayDamage);
            }
        }
    }

    private void ShootNormalBullet()
    {
        if(player != null)
        {
            Vector3 directionToPlayer = player.transform.position - firePoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
            BossBullet enemyBullet = bullet.AddComponent<BossBullet>();
            enemyBullet.SetMovementDirection(directionToPlayer * speedNormalBullet);
        }
     
    }

    private void ShootRoundBullet()
    {
        const int bulletCount = 12;
        float angleStep = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
            BossBullet enemyBullet = bullet.AddComponent<BossBullet>();
            enemyBullet.SetMovementDirection(direction * speedRoundBullet);
        }

    }

    private void AutoHeal(float hpAmount) { 
        currentHp = Mathf.Min(currentHp + hpAmount, maxHp);
        UpdateHpBar();
    }

    private void SpawnMiniEnemy() { 
        Instantiate(miniEnemyPrefab, transform.position, Quaternion.identity);
        miniEnemyPrefab.transform.localScale = new Vector3(3, 3, 1);
    }
    private void Teleport()
    {
        if(player != null)
        {
            transform.position = player.transform.position + new Vector3(1f, 0f, 0f);
        }
    }

    private void ChooseRandomSkill() { 
        int randomSkill = Random.Range(0, 4);
        switch (randomSkill)
        {
            case 0:
                ShootNormalBullet();
                break;
            case 1:
                ShootRoundBullet();
                break;
            case 2:
                AutoHeal(hpValue);
                break;
            case 3:
                SpawnMiniEnemy();
                break;
            case 4:
                Teleport();
                break;
        }
    }

    private void UseSkill()
    {
        skillTimer = Time.time + skillCooldown;
        ChooseRandomSkill();
    }
}
