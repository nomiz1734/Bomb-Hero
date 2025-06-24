using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneSkipper : MonoBehaviour
{
    [Header("Scene to Load")]
    public string nextSceneName = "LevelSelect";

    public void SkipCutscene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}