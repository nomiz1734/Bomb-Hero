using UnityEngine;

public class CoinPickUp : MonoBehaviour
{

    public int value;
    private bool movingToPlayer;
    public float speed;
    public float timeBetweenChecks = 0.2f;
    private float checkCounter;
    private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingToPlayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            checkCounter -= Time.deltaTime;

        }
        else
        {
            checkCounter -= Time.deltaTime;
            if (checkCounter <= 0)
            {
                checkCounter = timeBetweenChecks;
                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
                {
                    movingToPlayer = true;
                    speed += player.moveSpeed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Coin picked up: " + value);
            CoinController.instance.GetCoin(value);     
            
            Destroy(gameObject);
        }
    }
}
