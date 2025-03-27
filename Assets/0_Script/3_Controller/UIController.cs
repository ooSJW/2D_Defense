using UnityEngine;

public partial class UIController : MonoBehaviour // Data Field
{
    [field: SerializeField] public BuildingListUI BuildingListUI { get; private set; } = null;
    [SerializeField] private GameObject endStageUI;
}
public partial class UIController : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        endStageUI.SetActive(false);
        BuildingListUI.Initialize();
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
public partial class UIController : MonoBehaviour // 
{
    public void EndStage()
    {
        endStageUI.SetActive(true);
    }
}