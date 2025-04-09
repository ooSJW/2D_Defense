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
                currentSubStageIndex = value;
                RefreshInfoUI();
                if (MainSystem.Instance.StageManager.StageInformation.last_sub_stage >= currentSubStageIndex)
                    StartCoroutine(WaitForNexStage());
                else
                    StartCoroutine(EndStage(true));
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
    }
    private void Setup()
    {

    }
}
public partial class StageController : MonoBehaviour // Property
{
    public void RefreshInfoUI()
    {
        try
        {
            MainSystem.Instance.UIManager.UIController.StageInfoUI.RefreshInfoUI();
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
        yield return new WaitForSeconds(delayTime);
        MainSystem.Instance.EnemySpawnManager.EnemySpawnController.SetSpawnEnemy(true);
        yield break;
    }
    public IEnumerator EndStage(bool isClear)
    {
        yield return new WaitForSeconds(MainSystem.Instance.StageManager.StageInformation.stage_start_delay);

        MainSystem.Instance.UIManager.UIController.EndStage(isClear);


        MainSystem.Instance.StageManager.SaveStageScore(MainSystem.Instance.StageManager.StageInformation.stage_id, GetScore());
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