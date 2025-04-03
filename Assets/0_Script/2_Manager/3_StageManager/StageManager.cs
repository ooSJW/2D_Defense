using UnityEngine;
using static StageData;

public partial class StageManager : MonoBehaviour // Data Property
{
    public StageController StageController { get; private set; } = null;

    private int stageIndex;
    public int StageIndex
    {
        get => stageIndex;
        private set
        {
            try
            {
                StageInformation = MainSystem.Instance.DataManager.StageData.GetData(value);
                stageIndex = value;
            }
            catch
            {
                Debug.LogWarning("StageIndex is Invalid");
            }
        }
    }

    private StageInformation stageInformation;
    public StageInformation StageInformation
    {
        get => stageInformation;
        private set
        {
            if (stageInformation == null || !stageInformation.Equals(value))
            {
                stageInformation = new StageInformation()
                {
                    index = value.index,
                    stage_id = value.stage_id,
                    last_sub_stage = value.last_sub_stage,
                    stage_hp = value.stage_hp,
                    spawn_group_name_array = value.spawn_group_name_array,
                    spawn_group_percent_array = value.spawn_group_percent_array,
                    stage_start_delay = value.stage_start_delay,
                };
                int nameArrayLength, percentArrayLength;
                nameArrayLength = stageInformation.spawn_group_name_array.Length;
                percentArrayLength = stageInformation.spawn_group_percent_array.Length;
                if (nameArrayLength == percentArrayLength)
                    GetRandomGroup();
                else
                    Debug.LogWarning($"Check arrayLength\n[name_array.Length] : {nameArrayLength}\n[percent_array.Length] : {percentArrayLength}");
            }
        }
    }

}
public partial class StageManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        StageIndex = 0;
        print(StageInformation.index);
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
public partial class StageManager : MonoBehaviour // Property
{
    private void GetRandomGroup()
    {
        float random = Random.Range(0f, 1f);
        for (int i = 0; i < stageInformation.spawn_group_percent_array.Length; i++)
        {
            if (random <= stageInformation.spawn_group_percent_array[i])
            {
                MainSystem.Instance.EnemySpawnManager.RefreshSpawnData(stageInformation.spawn_group_name_array[i]);
                break;
            }
        }
    }
}
public partial class StageManager : MonoBehaviour // Sign
{
    public void SignupStageController(StageController stageController)
    {
        StageController = stageController;
        StageController.Initialize();
    }

    public void SigndownStageController()
    {
        StageController = null;
    }
}