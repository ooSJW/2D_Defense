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
    public void ChangeUI()
    {
        // TODO : UIø° StageManager.stageInfo.stage_id+" - "+currentStageIndex «•√‚
    }
}

public partial class StageController : MonoBehaviour // Coroutine
{
    public IEnumerator WaitForNexStage()
    {
        // TODO : 
        float delayTime = MainSystem.Instance.StageManager.StageInformation.stage_start_delay;
        yield return new WaitForSeconds(delayTime);
        MainSystem.Instance.EnemySpawnManager.EnemySpawnController.SetSpawnEnemy(true);
        yield break;
    }
    public IEnumerator EndStage(bool isClear)
    {
        yield return new WaitForSeconds(MainSystem.Instance.StageManager.StageInformation.stage_start_delay);

        MainSystem.Instance.UIManager.UIController.EndStage(isClear);

        yield break;
    }
}