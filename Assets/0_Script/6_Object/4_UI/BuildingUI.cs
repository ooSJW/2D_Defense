using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class BuildingUI : MonoBehaviour // Data Field
{
    [SerializeField] private Image buildingImage;

    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI costText;
}
public partial class BuildingUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {

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
public partial class BuildingUI : MonoBehaviour // 
{

}