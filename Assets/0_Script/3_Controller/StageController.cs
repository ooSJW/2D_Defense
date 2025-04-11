using System.Collections;
using UnityEngine;
using static StageData;

public partial class StageController : MonoBehaviour // Data property
{
    private int currentSubStageIndex;
    public int CurrentSubStageIndex
    {
        get => currentSubStageIndex;
        set
        {
            if (currentSubStageIndex != value && stageHp > 0)
            {
                int lastStageIndex = MainSystem.Instance.StageManager.StageInformation.last_sub_stage;
                if (lastStageIndex + 1 < value)
                    return;
                currentSubStageIndex = value;
                if (lastStageIndex >= currentSubStageIndex)
                {
                    RefreshInfoUI();
                    StartCoroutine(WaitForNexStage());
                }
                else
                {
                    RefreshInfoUI(true);
                    StartCoroutine(EndStage(true));
                }
            }
        }
    }

    private int stageHp;
    public int StageHp
    {
        get => stageHp;
        set
        {
            if (value > 0)
                stageHp = value;
            else if (stageHp != 0)
            {
                stageHp = 0;
                StartCoroutine(EndStage(false));
            }
            RefreshInfoUI();
        }
    }

}
public partial class StageController : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        StageHp = MainSystem.Instance.StageManager.StageInformation.stage_hp;
    }
    public void Initialize()
    {
        Allocate();
        Setup();
        MainSystem.Instance.StageManager.HasData();
    }
    private void Setup()
    {

    }
}
public partial class StageController : MonoBehaviour // Property
{
    public void RefreshInfoUI(bool isClear = false)
    {
        try
        {
            MainSystem.Instance.UIManager.UIController.StageInfoUI.RefreshInfoUI(isClear);
        }
        catch
        {

        }
    }
}

public partial class StageController : MonoBehaviour // Coroutine
{
    public IEnumerator WaitForNexStage()
    {
        float delayTime = MainSystem.Instance.StageManager.StageInformation.stage_start_delay;
        yield return StartCoroutine(MainSystem.Instance.UIManager.UIController.StageInfoUI.Countdown(delayTime));
        MainSystem.Instance.EnemySpawnManager.EnemySpawnController.SetSpawnEnemy(true);
        yield break;
    }
    public IEnumerator EndStage(bool isClear)
    {

        MainSystem.Instance.PlayerManager.GetReawrd((int)(MainSystem.Instance.StageManager.InGameCoin * 0.5f), 0);
        MainSystem.Instance.PlayerManager.SavePlayerData();
        MainSystem.Instance.DataManager.SaveData();


        StageInformation info = MainSystem.Instance.StageManager.StageInformation;
        if (isClear)
            MainSystem.Instance.StageManager.SaveStageScore(info.stage_id, GetScore());

        yield return MainSystem.Instance.UIManager.UIController.StageInfoUI.EndStage(isClear, info.stage_start_delay, info.stage_id);

        MainSystem.Instance.UIManager.UIController.EndStage(isClear);
        yield break;
    }

    private int GetScore()
    {
        StageInformation info = MainSystem.Instance.StageManager.StageInformation;

        int score = 0;
        if (stageHp / info.stage_hp > 0.7f)
            score = 3;
        else if (stageHp / info.stage_hp > 0.5f)
            score = 2;
        else
            score = 1;

        return score;
    }
}