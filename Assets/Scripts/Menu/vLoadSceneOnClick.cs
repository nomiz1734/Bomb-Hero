using UnityEngine;
using UnityEngine.SceneManagement;

public class vLoadSceneOnClick : MonoBehaviour
{
    public string sceneToLoad = "abc"; 
    public float delayBeforeLoad = 2f; 
    public AudioSource audioSource;    

    public void LoadSceneGame()
    {
        StartCoroutine(LoadSceneWithDelay());
    }

    private System.Collections.IEnumerator LoadSceneWithDelay()
    {
       
        if (audioSource != null)
        {
            audioSource.Play();
        }

      
        yield return new WaitForSeconds(delayBeforeLoad);

      
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
