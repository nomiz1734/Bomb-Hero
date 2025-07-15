using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float fallSpeed = 10f;
    public float damage = 20f;
    public GameObject explosionEffect;

    [Tooltip("Vị trí Y sẽ phát nổ (mặt đất hoặc vị trí người chơi)")]
    public float explodeY = 0f;

    private bool hasExploded = false;

    void Update()
    {
        // Rơi xuống
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        // Khi chạm đến điểm explodeY thì nổ
        if (!hasExploded && transform.position.y <= explodeY)
        {
            Explode();
        }
    }

    private void Explode()
    {
        hasExploded = true;

        // Ẩn sprite
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        // Gây damage nếu có player gần
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.5f); // bán kính nổ
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Player p = hit.GetComponent<Player>();
                if (p != null)
                {
                    p.TakeDamage(damage);
                }
            }
        }

        // Hiệu ứng nổ
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Xoá thiên thạch
        Destroy(gameObject);
    }

    // Debug vẽ vùng nổ
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
