using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static CoinController instance;
    private void Awake()
    {
        instance = this;
    }
    public int currentCoins;
    public CoinPickUp pickup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetCoin(int amount)
    {
        currentCoins += amount;
        Debug.Log("Current coin: " + currentCoins);     
        UIController.instance.UpdateCoins(currentCoins);
    }

    public void SpawnPickup(Vector3 position, int value)
    {
        Instantiate(pickup, position, Quaternion.identity).value = value;
    }
}
