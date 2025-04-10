using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class StageSaveData
{
    public Dictionary<int, int> stageScoreDict = new Dictionary<int, int>();
    public List<KeyValuePair<int, int>> stageScoreList = new List<KeyValuePair<int, int>>();
    // TODO ���� �ҷ����� �߶���
    // 04 11 �������
    public void FromDictionary()
    {
        stageScoreList = stageScoreDict.ToList();
    }

    public void ToDictionary()
    {
        stageScoreDict = stageScoreList.ToDictionary(pair => pair.Key, pair => pair.Value);
    }
}
