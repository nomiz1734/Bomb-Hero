using UnityEngine;
using System.Collections;

public class SaveSystemSetup : MonoBehaviour {

	[SerializeField] private string fileName = "Profile.bin"; // file to save with the specified resolution
    [SerializeField] private bool dontDestroyOnLoad; // the object will move from one scene to another (you only need to add it once)

    void Awake()
	{
		SaveSystem.Initialize(fileName);
        if (!SaveSystem.HasKey("PlayerCoins"))
        {
            SaveSystem.SetInt("PlayerCoins", 1000);
       
        }
        if (dontDestroyOnLoad) DontDestroyOnLoad(transform.gameObject);
	}

	// hàm tạm để clear currentcoin
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {

            Debug.Log("All save data cleared.");
        }
    }

    // if the object is present in all game scenes, auto save before exiting
    // on some platforms there may not be an exit function, see the Unity help
    void OnApplicationQuit()
	{
		SaveSystem.SaveToDisk();
	}
	// if the object is present in all game scenes, auto save before switching scenes
	void OnApplicationFocus(bool focus)
	{
		if(!focus) SaveSystem.SaveToDisk();
	}
	// if the object is present in all game scenes, auto save before switching scenes
	void OnApplicationPause(bool pause)
	{
		if(pause) SaveSystem.SaveToDisk();
    }

}
