using UnityEngine;

public partial class UIButtonEvent : MonoBehaviour // Data Field 
{
    [SerializeField] private GameObject buildingListUI;
}
public partial class UIButtonEvent : MonoBehaviour // Property
{
    public void OnOffbuildingListUI()
    {
        bool isActive = buildingListUI.activeSelf;
        buildingListUI.SetActive(!isActive);
    }
}