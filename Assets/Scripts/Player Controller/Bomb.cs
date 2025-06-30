using UnityEngine;

public class Bomb : Weapon
{
    public static Bomb instance;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float timeDestroy = 0.5f;
    public float damageDeal = 25f;
    private int moveDirection = 1; // Default direction

    public void SetDirection(int direction)
    {
        moveDirection = direction;
    }
    void Awake()
    {
        instance = this;        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetStats();
        Destroy(gameObject,timeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        MoveBomb(moveDirection);
        if (statsUpdated == true)
        {
            statsUpdated = false;
            SetStats();
        }
    }
    

    void MoveBomb(int a)
    {
        switch (a) { 
          case 0:
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                break;
            case 1:
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                break;
            case 2:
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
                break;
            case 3:
                transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
                break;
        }
        //transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bomb hit: " + collision.name);

        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Bomb deals damage to: " + enemy.name);
                enemy.TakeDamage(damageDeal);
            }
            Destroy(gameObject);
        }
    }
    public void SetStats()
    {
        damageDeal = stats[weaponLevel].damage;
        moveSpeed = stats[weaponLevel].speed;
        timeDestroy = stats[weaponLevel].duration;
        Hand.instance.SetCoolDown(stats[weaponLevel].cooldown);
        Hand.instance.SetAmmo((int)stats[weaponLevel].amount);
    }
}
