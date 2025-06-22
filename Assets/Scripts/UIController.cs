using TMPro;
using UnityEngine;
using UnityEngine.UI; // Optional: If you plan to use UI elements

public class UIController : MonoBehaviour
{
    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }

    public Slider expLevelSlider;
    public TMP_Text expLevelText;
    public TMP_Text CoinText;

    public int a, b;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateExp(int currentExp, int levelExp, int currentLevel)
    {
        expLevelSlider.maxValue = levelExp;
        expLevelSlider.value = currentExp;
        expLevelText.text = "Level: " + currentLevel;
    }

    public void UpdateCoins(int coins)
    {
        CoinText.text = "Coins: " + coins;
    }
}
