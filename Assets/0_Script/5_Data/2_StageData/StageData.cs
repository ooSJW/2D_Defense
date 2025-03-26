using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class StageData // Information
{
    [System.Serializable]
    public class StageInformation : BaseInformation
    {
        public int stage_id;
        public int last_sub_stage;

        public string[] spawn_group_name_array;
        public float[] spawn_group_percent_array;
        public float next_stage_delay;
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