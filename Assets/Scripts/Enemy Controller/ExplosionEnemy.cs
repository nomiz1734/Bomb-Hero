using UnityEngine;

public class ExplosionEnemy : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject explosionPrefabs;
    [SerializeField] private float timeDestroy = 3f;

    private void CreateExplosion()
    {
        if(explosionPrefabs != null)
        {
            Instantiate(explosionPrefabs,transform.position,Quaternion.identity);
        }
        Destroy(gameObject);
    }

    protected override void Start()
    {
        base.Start();
        Invoke(nameof(CreateExplosion), timeDestroy);
    }
    protected override void Die()
    {
        CreateExplosion();
        base.Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        CreateExplosion();
    }
}
