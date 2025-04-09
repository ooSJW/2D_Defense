using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerBuildingData;

public partial class PlayerBuildingInfoUI : MonoBehaviour // Data Field
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI attackRangeText;
    [SerializeField] private TextMeshProUGUI attackDelayText;
    [SerializeField] private TextMeshProUGUI upgradCostText;
    [SerializeField] private TextMeshProUGUI resellCostText;

    [SerializeField] private Button upgradButton;
    [SerializeField] private Button resellButton;
}

public partial class PlayerBuildingInfoUI : MonoBehaviour // Data Property
{
    private PlayerBuilding playerBuilding;
    public PlayerBuilding PlayerBuilding
    {
        get => playerBuilding;
        private set
        {
            if (playerBuilding != value)
            {
                playerBuilding = value;
                if (playerBuilding != null)
                {
                    RefreshInfoUI();
                    gameObject.SetActive(true);
                }
                else
                    gameObject.SetActive(false);
            }
        }
    }
}


public partial class PlayerBuildingInfoUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        upgradButton.onClick.AddListener(UpgradBuilding);
        resellButton.onClick.AddListener(ResellBuilding);
        gameObject.SetActive(false);
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {

    }
}
public partial class PlayerBuildingInfoUI : MonoBehaviour // Property
{
    public void ChangePlayerBuilding(PlayerBuilding playerBuilding)
    {
        PlayerBuilding = playerBuilding;
    }

    private void RefreshInfoUI()
    {
        PlayerBuildingInformation info = PlayerBuilding.PlayerBuildingInformation;
        int currentIndex = PlayerBuilding.CurrentIndex;

        iconImage.sprite = Resources.Load<Sprite>($"Icon/{info.name}");
        nameText.text = $"�̸� : {info.ui_name}";
        attackRangeText.text = $"���� ���� : {info.attack_range_array[currentIndex]}m";
        attackDelayText.text = $"���� ��ٿ� : {info.attack_delay_array[currentIndex]}��";
        resellCostText.text = $"�ȱ� : {PlayerBuilding.GetResellCost()}";

        if (playerBuilding.IsActive)
        {
            upgradButton.gameObject.SetActive(true);
            resellButton.gameObject.SetActive(true);
            if (PlayerBuilding.CanUpgrad())
            {
                upgradCostText.text = $"���׷��̵�\nCost : {info.upgrad_cost}";
                upgradButton.interactable = true;
            }
            else
            {
                upgradCostText.text = "�ִ� ���� ����";
                upgradButton.interactable = false;
            }
        }
        else
        {
            upgradButton.gameObject.SetActive(false);
            resellButton.gameObject.SetActive(false);
        }
    }



    public void UpgradBuilding()
    {
        MainSystem.Instance.PlayerBuildingManager.PlayerBuildingController.UpgradBuilding();
        RefreshInfoUI();
    }

    public void ResellBuilding()
    {
        MainSystem.Instance.PlayerBuildingManager.PlayerBuildingController.ResellBuilding();
    }
}