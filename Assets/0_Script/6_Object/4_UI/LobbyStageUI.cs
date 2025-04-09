using System.Collections.Generic;
using UnityEngine;
using static StageData;

public partial class LobbyStageUI : MonoBehaviour // Data Field
{
    [SerializeField] private RectTransform contentTransform;
    private List<StageUI> stageUIList;
}
public partial class LobbyStageUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        stageUIList = new List<StageUI>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
        SpawnStageUI();
    }
    private void Setup()
    {

    }
}
public partial class LobbyStageUI : MonoBehaviour // Property
{
    public void SpawnStageUI()
    {
        for (int i = 0; i < MainSystem.Instance.DataManager.StageData.GetStageCount(); i++)
        {
            StageInformation info = MainSystem.Instance.DataManager.StageData.GetData(i);
            StageUI ui = MainSystem.Instance.PoolManager.Spawn("StageUI", contentTransform).GetComponent<StageUI>();
            bool isUnlock = false;

            if (MainSystem.Instance.StageManager.GetNextStageToUnlock() > i)
                isUnlock = true;

            ui.Initialize(info, isUnlock);
            stageUIList.Add(ui);
        }
    }
}