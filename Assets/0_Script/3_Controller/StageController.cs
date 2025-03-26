using UnityEngine;
using static StageData;

public partial class StageController : MonoBehaviour // Data property
{
    private StageInformation stageInformation;
    public StageInformation StageInformation
    {
        get => stageInformation;
        private set
        {
            stageInformation = new StageInformation()
            {
                index = value.index,
                stage_id = value.stage_id,
                last_sub_stage = value.last_sub_stage,
                spawn_group_name_array = value.spawn_group_name_array,
                spawn_group_percent_array = value.spawn_group_percent_array,
                next_stage_delay = value.next_stage_delay,
            };
        }
    }
}
public partial class StageController : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        //TODO TEST
        StageInformation = MainSystem.Instance.DataManager.StageData.GetData(0);
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
public partial class StageController : MonoBehaviour // 
{

}