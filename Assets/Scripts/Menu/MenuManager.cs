using UnityEngine;

public class MenuManage : MonoBehaviour
{
    public GameObject MainMenu;         // Kéo từ Hierarchy: GameObject tên MainMenu
    public GameObject SettingPanel;     // Kéo từ Hierarchy: GameObject tên SettingPanel

    public void OpenSetting()
    {
        MainMenu.SetActive(false);
        SettingPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SettingPanel.SetActive(false);
        MainMenu.SetActive(true);
    }
}
