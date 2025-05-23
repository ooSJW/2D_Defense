using NUnit.Framework;
using System;
using System.IO;
using UnityEngine;
using static StageData;
using static UnityEngine.Rendering.DebugUI;

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
                    map_icon_path = value.map_icon_path,
                    initial_coin = value.initial_coin,
                    spawn_group_name_array = value.spawn_group_name_array,
                    spawn_group_percent_array = value.spawn_group_percent_array,
                    stage_start_delay = value.stage_start_delay,
                    is_last_stage = value.is_last_stage,
                };
            }
                SetStage();
        }
    }

    public bool IsLastStage { get; private set; } = false;

    private int inGameCoin;
    public int InGameCoin
    {
        get => inGameCoin;
        private set
        {
            if (inGameCoin != value)
            {
                inGameCoin = value;
                StageController.RefreshInfoUI();
            }
        }
    }
}
public partial class StageManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        savePath = Path.Combine(Application.persistentDataPath, "StageSaveData.json");
    }
    public void Initialize()
    {
        Allocate();
        Setup();
        HasData();
    }
    private void Setup()
    {

    }
}
public partial class StageManager : MonoBehaviour // Property
{
    private void GetRandomGroup()
    {
        float random = UnityEngine.Random.Range(0f, 1f);
        for (int i = 0; i < stageInformation.spawn_group_percent_array.Length; i++)
        {
            if (random <= stageInformation.spawn_group_percent_array[i])
            {
                MainSystem.Instance.EnemySpawnManager.RefreshSpawnData(stageInformation.spawn_group_name_array[i]);
                break;
            }
        }
    }

    public void GetCoin(int coinAmount)
    {
        InGameCoin += coinAmount;
    }
    public bool SpendCoin(int coinAmount)
    {
        if (InGameCoin >= coinAmount)
        {
            InGameCoin -= coinAmount;
            return true;
        }
        return false;
    }
    public void ChangeStage(int stageIndex)
    {
        StageIndex = stageIndex;
    }

    public void SetStage()
    {
        IsLastStage = Convert.ToBoolean(stageInformation.is_last_stage);
        int nameArrayLength, percentArrayLength;
        inGameCoin = stageInformation.initial_coin;
        nameArrayLength = stageInformation.spawn_group_name_array.Length;
        percentArrayLength = stageInformation.spawn_group_percent_array.Length;
        if (nameArrayLength == percentArrayLength)
            GetRandomGroup();
        else
            Debug.LogWarning($"Check arrayLength\n[name_array.Length] : {nameArrayLength}\n[percent_array.Length] : {percentArrayLength}");
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

public partial class StageManager : MonoBehaviour // Data
{

    private string savePath;
    private StageSaveData saveData;

    public void SaveStageScore(int stageId, int stageScore)
    {
        StageScoreData existingData = saveData.stageScoreDataList.Find(elem => elem.stageId == stageId);
        if (existingData != null)
        {
            if (existingData.stageScore < stageScore)
                existingData.stageScore = stageScore;
        }
        else
            saveData.stageScoreDataList.Add(new StageScoreData(stageId, stageScore));

        SaveData();
    }

    public bool IsClearStage(int stageId)
    {
        return saveData.stageScoreDataList.Find(elem => elem.stageId == stageId) != null;
    }

    public int GetNextStageToUnlock()
    {
        return saveData.stageScoreDataList.Count + 1;
    }

    public int LoadStageScore(int stageId)
    {
        StageScoreData data = saveData.stageScoreDataList.Find(elem => elem.stageId == stageId);
        return data != null ? data.stageScore : 0;
    }


    public void SaveData()
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);
    }

    public bool HasData()
    {
        if (File.Exists(savePath))
        {
            LoadData();
            return true;
        }
        else
        {
            saveData = new StageSaveData();
            return false;
        }
    }

    public void LoadData()
    {
        string json = File.ReadAllText(savePath);
        saveData = JsonUtility.FromJson<StageSaveData>(json);
    }

    public void InitialData()
    {
        if (File.Exists(savePath))
            File.Delete(savePath);
    }
}