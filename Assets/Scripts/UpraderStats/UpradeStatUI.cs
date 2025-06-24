using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UpgradeStatUI : MonoBehaviour
{
    [Header("Level Buttons (UI)")]
    public Button[] levelButtons;
    public Sprite[] activeIcons;
    public Sprite[] grayIcons;

    [Header("Upgrade Info UI")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI costText;
    public Button buyButton;
    public TextMeshProUGUI textGold; // ✨ Gold UI hiển thị

    [Header("Upgrade Data")]
    [SerializeField] private string upgradeName;
    [SerializeField] private float[] upgradeValues;
    [SerializeField] private int[] upgradeCosts;

    [Header("Gameplay")]
    private int currentGold = 1000;
    private int currentLevel = 0;
    private int selectedLevel = -1;

    void Start()
    {

        currentGold = SaveSystem.GetInt("PlayerCoins");
        currentLevel = SaveSystem.GetInt(upgradeName + "_Level", 0);

        // Cập nhật UI
        UpdateGoldUI();
        buyButton.interactable = false;
        UpdateLevelButtons();
    }

    void Update()
    {
        currentGold = SaveSystem.GetInt("PlayerCoins");
        textGold.text=currentGold.ToString();
    }

    void UpdateGoldUI()
    {
        if (textGold != null)
        {
            textGold.text = $"{currentGold}";
        }
    }

    void UpdateLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = i;
            Button btn = levelButtons[i];

            if (i >= activeIcons.Length || i >= grayIcons.Length) continue;

            btn.image.sprite = (level <= currentLevel) ? activeIcons[i] : grayIcons[i];
            btn.interactable = (level == currentLevel || level == currentLevel + 1);

            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => OnClickLevel(level));
        }
    }

    void OnClickLevel(int level)
    {
        selectedLevel = level;

        nameText.text = upgradeName;
        descriptionText.text = GetDescriptionByStat(upgradeName, level);

        if (level == currentLevel)
        {
            costText.text = "Đã nâng cấp";
        }
        else if (level == 0)
        {
            costText.text = "Không có";
        }
        else if (level >= upgradeCosts.Length)
        {
            costText.text = "Không có dữ liệu";
        }
        else
        {
            costText.text = $"Cost: {upgradeCosts[level]}";
        }

        bool canUpgrade = (level == currentLevel + 1)
                        && (level < upgradeCosts.Length)
                        && (currentGold >= upgradeCosts[level]);

        buyButton.interactable = canUpgrade;
        buyButton.onClick.RemoveAllListeners();

        if (canUpgrade)
        {
            int levelToBuy = level;
            buyButton.onClick.AddListener(() => BuyUpgrade(levelToBuy));
        }

        Debug.Log($"Click level {level} | Gold: {currentGold}");
    }

    void BuyUpgrade(int level)
    {
        if (level != currentLevel + 1 || level >= upgradeCosts.Length) return;

        int cost = upgradeCosts[level];
        if (currentGold < cost) return;

        currentGold -= cost;
        currentLevel = level;

        // 🟢 Lưu chỉ số mới sau khi nâng cấp
        SaveSystem.SetInt("PlayerCoins", currentGold);
        SaveSystem.SetInt(upgradeName + "_Level", currentLevel);

        // (Nếu có thay đổi stat cụ thể như máu, giáp... thì lưu thêm)
        switch (upgradeName.ToLower())
        {
            case "max health":
            case "máu":
                SaveSystem.SetFloat("PlayerHp", upgradeValues[level]);
                break;
            case "armor":
                SaveSystem.SetFloat("PlayerShield", upgradeValues[level]);
                break;
            case "pickup":
                SaveSystem.SetFloat("PlayerRangePickUp", upgradeValues[level]);
                break;
            case "cooldown":
                SaveSystem.SetFloat("PlayerCoolDown", upgradeValues[level]);
                break;
        }

        SaveSystem.SaveToDisk(); // 🟢 Ghi xuống file

        UpdateLevelButtons();
        buyButton.interactable = false;

        nameText.text = "";
        descriptionText.text = "";
        costText.text = "";

        Debug.Log($"Đã nâng cấp {upgradeName} lên cấp {level + 1}. Vàng còn lại: {currentGold}");
    }


    private string GetStatKey(string name)
    {
        switch (name.ToLower())
        {
            case "health":
            case "max health":
                return "PlayerHp";
            case "armor":
                return "PlayerShield";
            case "pickup":
            case "range pickup":
                return "PlayerRangePickUp";
            case "cooldown":
                return "PlayerCoolDown";
            default:
                return name; // fallback
        }
    }

    private string GetDescriptionByStat(string name, int level)
    {
        switch (name.ToLower())
        {
            case "max health":
            case "health":
            case "máu":
                return new string[]
                {
                    "Đây là chỉ số máu của bạn, mặc định là 100",
                    "Nâng cấp chỉ số để được tăng thêm 20 máu",
                    "Nâng cấp chỉ số để được tăng thêm 30 máu",
                    "Nâng cấp chỉ số để được tăng thêm 40 máu",
                    "Nâng cấp chỉ số để được tăng thêm 50 máu",
                    "Nâng cấp chỉ số để được tăng thêm 60 máu"
                }[level];

            case "armor":
            case "giáp":
                return new string[]
                {
                    "Đây là chỉ số giáp mặc định, không có chống chịu",
                    "Tăng 10% kháng sát thương nhận vào",
                    "Tăng 15% kháng sát thương nhận vào",
                    "Tăng 20% kháng sát thương nhận vào",
                    "Tăng 25% kháng sát thương nhận vào",
                    "Tăng 30% kháng sát thương nhận vào"
                }[level];

            case "pickup":
            case "phạm vi nhặt":
                return new string[]
                {
                    "Phạm vi nhặt mặc định của bạn",
                    "Tăng phạm vi nhặt thêm 1 đơn vị",
                    "Tăng phạm vi nhặt thêm 2 đơn vị",
                    "Tăng phạm vi nhặt thêm 3 đơn vị",
                    "Tăng phạm vi nhặt thêm 4 đơn vị",
                    "Tăng phạm vi nhặt thêm 5 đơn vị"
                }[level];

            case "cooldown":
            case "giảm hồi chiêu":
                return new string[]
                {
                    "Thời gian hồi chiêu mặc định",
                    "Giảm hồi chiêu 5%",
                    "Giảm hồi chiêu 10%",
                    "Giảm hồi chiêu 15%",
                    "Giảm hồi chiêu 20%",
                    "Giảm hồi chiêu 25%"
                }[level];

            default:
                return "Không có mô tả.";
        }
    }
}
