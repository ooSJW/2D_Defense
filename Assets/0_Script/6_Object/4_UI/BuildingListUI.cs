using UnityEngine;

public partial class BuildingListUI : MonoBehaviour // Data Field
{
    [SerializeField] private GameObject buildingListBackground;
    [SerializeField] private RectTransform contentTransform;

}
public partial class BuildingListUI : MonoBehaviour // Initialize
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
public partial class BuildingListUI : MonoBehaviour // Property
{
    public void SpawnBuildingUI(BuildingName name)
    {
        BuildingUI buildingUI = MainSystem.Instance.PoolManager.Spawn("BuildingUI", contentTransform).GetComponent<BuildingUI>();
        buildingUI.Initialize(name);
    }

    public void OnOffbuildingListUI()
    {
        bool isActive = buildingListBackground.activeSelf;
        buildingListBackground.SetActive(!isActive);
    }
}