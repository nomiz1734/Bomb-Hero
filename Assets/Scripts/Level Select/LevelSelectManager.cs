using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button startButton;

    [Header("Level Buttons")]
    [SerializeField] private Button[] levelButtons;
    [Header("Sprites")]
    [SerializeField] private Sprite lockSprite;         
    [SerializeField] private Sprite openSprite;         
    [SerializeField] private Sprite bossLockSprite;     
    [SerializeField] private Sprite bossOpenSprite;     

    private string selectedSceneName = "";
    private int currentLevel;

    private void Start()
    {
        currentLevel = SaveSystem.GetInt("Level", 1); 
        startButton.gameObject.SetActive(false);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelNumber = i + 1;
            bool isUnlocked = levelNumber <= currentLevel;

            // get image  button
            Image btnImage = levelButtons[i].GetComponent<Image>();
            if (btnImage != null)
            {
                // if boss (level 5)
                if (levelNumber == 5)
                {
                    btnImage.sprite = isUnlocked ? bossOpenSprite : bossLockSprite;
                }
                else
                {
                    btnImage.sprite = isUnlocked ? openSprite : lockSprite;
                }
            }

        
            levelButtons[i].interactable = isUnlocked;

      
            if (isUnlocked)
            {
                int episodeNumber = levelNumber; 
                levelButtons[i].onClick.AddListener(() => OnLevelSelected(episodeNumber));
            }
        }
    }

    public void OnLevelSelected(int episodeNumber)
    {
        selectedSceneName = "Esposide" + episodeNumber;
        startButton.gameObject.SetActive(true);
    }

    public void OnStartButtonClicked()
    {
        if (!string.IsNullOrEmpty(selectedSceneName))
        {
            SceneManager.LoadScene(selectedSceneName);
        }
    }
}
