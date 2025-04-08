using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class UIController : MonoBehaviour // Data Field
{
    [field: SerializeField] public UIButtonEvent UIButtonEvent { get; private set; } = null;
    [Header("CombatScene Member")]
    [field: SerializeField] public BuildingListUI BuildingListUI { get; private set; } = null;
    [field: SerializeField] public EndStageUI EndStageUI { get; private set; } = null;

    [Header("NotCombatScene Member")]
    [field: SerializeField] public PlayerInfoUI PlayerInfoUI { get; private set; } = null;
    [field: SerializeField] public OptionUI OptionUI { get; private set; } = null;
    [field: SerializeField] public StoreUI StoreUI { get; private set; } = null;
    [field: SerializeField] public InventoryUI InventoryUI { get; private set; } = null;
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
                StoreUI.Initialize();
                InventoryUI.Initialize();
                break;
            case SceneType.Combat:
                BuildingListUI.Initialize();
                EndStageUI.Initialize();
                SpawnBuilding();
                break;
        }
    }
    private void Setup()
    {

    }
}
public partial class UIController : MonoBehaviour // Not CombatScene Property
{
    public void OnOffStore()
    {
        bool isActive = StoreUI.gameObject.activeSelf;
        if (isActive)
            StoreUI.gameObject.SetActive(false);
        else
        {
            StoreUI.RefreshStore();
            StoreUI.gameObject.SetActive(true);
        }
    }
}

public partial class UIController : MonoBehaviour // CombatScene Property
{
    public void RefreshLobbyUI()
    {
        StoreUI.RefreshStore();
        PlayerInfoUI.RefreshPlayerInfoUI();
    }
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