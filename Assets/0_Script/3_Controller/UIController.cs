using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class UIController : MonoBehaviour // Data Field
{
    [field: SerializeField] public BuildingListUI BuildingListUI { get; private set; } = null;
    [field: SerializeField] public UIButtonEvent UIButtonEvent { get; private set; } = null;
    [field: SerializeField] public EndStageUI EndStageUI { get; private set; } = null;
    [field: SerializeField] public PlayerInfoUI PlayerInfoUI { get; private set; } = null;
    [field: SerializeField] public OptionUI OptionUI { get; private set; } = null;
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

        SceneType sceneType = MainSystem.Instance.SceneManager.ActiveScene.SceneType;
        switch (sceneType)
        {
            case SceneType.Initialize:
                break;
            case SceneType.NotCombat:
                PlayerInfoUI.Initialize();
                OptionUI.Initialize();
                break;
            case SceneType.Combat:
                BuildingListUI.Initialize();
                SpawnBuilding();
                EndStageUI.Initialize();
                break;
        }
    }
    private void Setup()
    {

    }
}
public partial class UIController : MonoBehaviour // Not CombatScene Property
{

}

public partial class UIController : MonoBehaviour // CombatScene Property
{
    public void SpawnBuilding()
    {
        List<BuildingName> buildingNameList = MainSystem.Instance.PlayerManager.Player.UnlockedBuildingNameList;
        for (int i = 0; i < buildingNameList.Count; i++)
            BuildingListUI.SpawnBuildingUI(buildingNameList[i]);
    }
    public void EndStage(bool isClear)
    {
        EndStageUI.InitialEndStageUI(isClear);
    }
}