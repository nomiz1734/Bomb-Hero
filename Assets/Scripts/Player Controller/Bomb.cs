using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float timeDestroy = 0.5f;
    public float damageDeal = 25f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,timeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        MoveBomb();
    }

    void MoveBomb()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
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

}
