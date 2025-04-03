using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class UIController : MonoBehaviour // Data Field
{
    [field: SerializeField] public BuildingListUI BuildingListUI { get; private set; } = null;
    [field: SerializeField] public UIButtonEvent UIButtonEvent { get; private set; } = null;
    [field: SerializeField] public EndStageUI EndStageUI { get; private set; } = null;
}
public partial class UIController : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();
        BuildingListUI.Initialize();
        EndStageUI.Initialize();
    }
    private void Setup()
    {
        List<BuildingName> buildingNameList = MainSystem.Instance.PlayerManager.Player.UnlockedBuildingNameList;
        for (int i = 0; i < buildingNameList.Count; i++)
            BuildingListUI.SpawnBuildingUI(buildingNameList[i]);
    }
}
public partial class UIController : MonoBehaviour // 
{
    public void EndStage(bool isClear)
    {
        EndStageUI.InitialEndStageUI(isClear);
    }
}