using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.UIElements; // Optional: If you plan to use UI elements

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [SerializeField] private GameManager gameManager;
    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        gameManager.StartGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ContinueGame() { 
        gameManager.ContinueGame();
    }
    public void MainMenu() { 
        gameManager.MainMenu();
    }
    public void ResetGame() {
        //if (GameManager.instance != null) Destroy(GameManager.instance.gameObject);
        //if (UIController.instance != null) Destroy(UIController.instance.gameObject);
        //if (CoinController.instance != null) Destroy(CoinController.instance.gameObject);

        // Reset static variables
        // ...

        SceneManager.LoadScene("LevelSelect");
    }

    public Slider expLevelSlider;
    public TMP_Text expLevelText;
    public TMP_Text CoinText;
    public Image exp;    
    

    public int a, b;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.T))
        //{
        //    GameEndAnimation();
        //}
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
        SaveCoin(); // Save coins whenever they are updated

    }

    void SaveCoin()
    {
        int currentCoins = CoinController.instance.currentCoins;
        int coins= SaveSystem.GetInt("PlayerCoins",0);
        SaveSystem.SetInt("PlayerCoins", coins + currentCoins);
        //SaveSystem.SaveToDisk();
    }
}
