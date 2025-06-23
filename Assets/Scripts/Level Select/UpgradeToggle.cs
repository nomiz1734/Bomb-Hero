using UnityEngine;

public class UpgradeToggle : MonoBehaviour
{
    [Header("Upgrade Panel to Show/Hide")]
    public GameObject upgradePanel;

    // Gọi từ nút Upgrade
    public void ToggleUpgradePanel()
    {
        if (upgradePanel != null)
        {
            bool isActive = upgradePanel.activeSelf;
            upgradePanel.SetActive(!isActive);
        }
    }
}
