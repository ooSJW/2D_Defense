using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public partial class StageData // Information
{
    [System.Serializable]
#pragma warning disable CS0659 // 형식은 Object.Equals(object o)를 재정의하지만 Object.GetHashCode()를 재정의하지 않습니다.
    public class StageInformation : BaseInformation
#pragma warning restore CS0659 // 형식은 Object.Equals(object o)를 재정의하지만 Object.GetHashCode()를 재정의하지 않습니다.
    {
        public int stage_id;
        public int last_sub_stage;

        public string[] spawn_group_name_array;
        public float[] spawn_group_percent_array;
        public float stage_start_delay;

        public override bool Equals(object obj)
        {
            if (obj is StageInformation other)
            {
                return
                    index == other.index &&
                    stage_id == other.stage_id &&
                    last_sub_stage == other.last_sub_stage &&
                    stage_start_delay == other.stage_start_delay &&
                    spawn_group_name_array.SequenceEqual(other.spawn_group_name_array) &&
                    spawn_group_percent_array.SequenceEqual(other.spawn_group_percent_array);
            }
            else
                return false;
        }
    }
}

public partial class StageData // Data Field
{
    private Dictionary<string, StageInformation> stageInformationDict;
}

public partial class StageData // Initialize
{
    private void Allocate()
    {
        stageInformationDict = new Dictionary<string, StageInformation>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        MainSystem.Instance.DataManager.LoadCsv<StageInformation>("StageData", stageInformationDict);
    }
}

public partial class StageData // Perperty
{
    public StageInformation GetData(int currentStage)
    {
        return stageInformationDict.FirstOrDefault(elem => elem.Value.stage_id == currentStage).Value;
    }
}