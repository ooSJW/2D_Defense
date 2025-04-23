using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerBuildingData;

public partial class BuildingUI : MonoBehaviour // Data Field
{
    [SerializeField] private Button button;
    [SerializeField] private Image buildingImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI costText;

    private PlayerBuildingInformation playerBuildingInformation;

    private BuildingName buildingName;
}
public partial class BuildingUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        playerBuildingInformation = MainSystem.Instance.DataManager.PlayerBuildingData.GetData(buildingName);
    }
    public void Initialize(BuildingName nameValue)
    {
        buildingName = nameValue;
        Allocate();
        Setup();
    }
    private void Setup()
    {
        buildingImage.sprite = Resources.Load<Sprite>($"Icon/{playerBuildingInformation.name}");
        nameText.text = playerBuildingInformation.ui_name;
        descriptionText.text = playerBuildingInformation.description;
        costText.text = $"Cost : {playerBuildingInformation.cost}";
        transform.localScale = Vector3.one;
        button.onClick.AddListener(() => MainSystem.Instance.UIManager.UIController.UIButtonEvent.SelectBuilding(buildingName));
    }
}
public partial class BuildingUI : MonoBehaviour // 
{

}