using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //[SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject endGame;
    [SerializeField] private GameObject gamePause;
    [SerializeField] private TMP_Text endGameText;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //MainMenu();
    }

    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
        //mainMenu.SetActive(true);
        endGame.SetActive(false);
        gamePause.SetActive(false);
        Time.timeScale = 0f; 
    }

    public void EndGame(string text)
    {
        
        endGame.SetActive(true);
        gamePause.SetActive(false);
        endGameText.text = text;      
        Time.timeScale = 0f;
    }

    public void GamePause()
    {
        //mainMenu.SetActive(false);
        endGame.SetActive(false);
        gamePause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        //mainMenu.SetActive(false);
        endGame.SetActive(false);
        gamePause.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ContinueGame()
    {
        //mainMenu.SetActive(false);
        endGame.SetActive(false);
        gamePause.SetActive(false);
        Time.timeScale = 1f;
    }

   
}
