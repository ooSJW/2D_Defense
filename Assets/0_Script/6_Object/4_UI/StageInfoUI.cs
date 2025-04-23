using System.Collections;
using TMPro;
using UnityEngine;

public partial class StageInfoUI : MonoBehaviour // Data Field
{
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI hpText;

    [SerializeField] private TextMeshProUGUI stageMessageText;
}
public partial class StageInfoUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        stageMessageText.gameObject.SetActive(false);
    }
    public void Initialize()
    {
        Allocate();
        Setup();
        RefreshInfoUI();
    }
    private void Setup()
    {

    }
}
public partial class StageInfoUI : MonoBehaviour // Property
{
    public void RefreshInfoUI(bool isClear = false)
    {
        if (isClear)
            stageText.text = $"Stage{MainSystem.Instance.StageManager.StageInformation.stage_id} CLEAR!";
        else
            stageText.text = $"Stage{MainSystem.Instance.StageManager.StageInformation.stage_id}- {MainSystem.Instance.StageManager.StageController.CurrentSubStageIndex}";

        coinText.text = MainSystem.Instance.StageManager.InGameCoin.ToString();
        hpText.text = MainSystem.Instance.StageManager.StageController.StageHp.ToString();
    }
}
public partial class StageInfoUI : MonoBehaviour // Corutine
{
    public IEnumerator Countdown(float countdown)
    {
        stageMessageText.gameObject.SetActive(true);

        for (int i = (int)countdown; i > 0; i--)
        {
            stageMessageText.text = $"다음 웨이브 시작까지 {i}";
            stageMessageText.alpha = 1f;
            yield return new WaitForSeconds(1f);
        }

        stageMessageText.text = "적이 몰려옵니다";
        yield return new WaitForSeconds(1f);

        stageMessageText.alpha = 0f;
        stageMessageText.gameObject.SetActive(false);
    }

    public IEnumerator EndStage(bool isClear, float delayTime, int stageId)
    {
        stageMessageText.gameObject.SetActive(true);

        string message = string.Empty;
        if (isClear)
            message = $"Stage{stageId} Clear!";
        else
            message = "Stage Failed..";

        stageMessageText.text = message;
        stageMessageText.alpha = 1f;
        yield return new WaitForSecondsRealtime(delayTime);

        stageMessageText.alpha = 0f;
        stageMessageText.gameObject.SetActive(false);
    }

}