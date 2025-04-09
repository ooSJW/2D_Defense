using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerBuildingData;

public partial class StoreBuildingUI : MonoBehaviour // Data Field
{

    [SerializeField] private Image iconImage;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI unlockCostText;
    [SerializeField] private TextMeshProUGUI unlockLevelText;
    [SerializeField] private TextMeshProUGUI placeCostText;

    [SerializeField] private GameObject lockObject;
    [SerializeField] private Button buyButton;

    private PlayerBuildingInformation playerBuildingInformation;
    private BuildingName buildingName;
}
public partial class StoreBuildingUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        buyButton.interactable = false;
        lockObject.SetActive(false);
        if (!Enum.TryParse<BuildingName>(playerBuildingInformation.name, true, out buildingName))
            Debug.LogWarning($"Name Parse Error[name : {playerBuildingInformation.name}]");
    }
    public void Initialize(PlayerBuildingInformation info, bool isStore = false)
    {
        playerBuildingInformation = info;
        Allocate();
        Setup();
        InitialUI(isStore);
    }
    private void Setup()
    {
        buyButton.onClick.AddListener(UnlockBuilding);
    }
}
public partial class StoreBuildingUI : MonoBehaviour // Property
{
    private void InitialUI(bool isStore)
    {
        iconImage.sprite = Resources.Load<Sprite>($"Icon/{playerBuildingInformation.name}");
        nameText.text = playerBuildingInformation.ui_name;
        descriptionText.text = playerBuildingInformation.description;
        unlockCostText.text = $"{playerBuildingInformation.unlock_cost} Coin";
        unlockLevelText.text = $"{playerBuildingInformation.unlock_level}레벨에 구매 가능";
        placeCostText.text = $"배치 비용 : {playerBuildingInformation.cost}";
        RefreshUI();

        placeCostText.gameObject.SetActive(!isStore);
        buyButton.gameObject.SetActive(isStore);
    }

    public void RefreshUI()
    {
        bool isUnlock = MainSystem.Instance.PlayerManager.Player.UnlockedBuildingNameList.Contains(buildingName);
        if (isUnlock)
        {
            unlockCostText.text = "보유 중";
            buyButton.interactable = false;
            lockObject.SetActive(false);
        }
        else
        {
            int playerCoin = MainSystem.Instance.PlayerManager.Coin;
            bool canBuy = MainSystem.Instance.PlayerManager.Player.Level >= playerBuildingInformation.unlock_level ? true : false;

            buyButton.interactable = playerCoin >= playerBuildingInformation.unlock_cost ? true : false;
            lockObject.SetActive(!canBuy);
        }
    }

    public void UnlockBuilding()
    {
        MainSystem.Instance.PlayerManager.Player.UnlockBuilding(buildingName);
        MainSystem.Instance.PlayerManager.SpendCoin(playerBuildingInformation.unlock_cost);
        MainSystem.Instance.UIManager.UIController.RefreshLobbyUI();
    }
}