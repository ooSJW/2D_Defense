using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class StageSaveData
{
    public List<StageScoreData> stageScoreDataList = new List<StageScoreData>();
}

[System.Serializable]
public class StageScoreData
{
    public int stageId;
    public int stageScore;

    public StageScoreData(int stageId, int stageScore)
    {
        this.stageId = stageId;
        this.stageScore = stageScore;
    }
}
