using UnityEditor;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Game is quitting..."); // sẽ thấy log này khi chạy trong editor
        EditorApplication.isPlaying = false; //Stop editor
        Application.Quit();
    }
}
