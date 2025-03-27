using UnityEngine;

public partial class BuildingListUI : MonoBehaviour // Data Field
{
    [SerializeField] private GameObject buildingList;

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
public partial class BuildingListUI : MonoBehaviour // public
{
    public void OnOffbuildingListUI()
    {
        bool isActive = buildingList.activeSelf;
        buildingList.SetActive(!isActive);
    }
}