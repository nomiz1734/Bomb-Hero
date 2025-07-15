using UnityEngine;

public class FlameDamageArea : MonoBehaviour
{
    public float damagePerSecond = 10f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player p = collision.GetComponent<Player>();
            if (p != null)
            {
                p.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}
