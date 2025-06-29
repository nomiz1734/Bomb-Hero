using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private string selectedSceneName = "";

    private void Start()
    {
        startButton.gameObject.SetActive(false);
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
