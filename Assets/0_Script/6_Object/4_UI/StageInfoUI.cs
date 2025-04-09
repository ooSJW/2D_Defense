using TMPro;
using UnityEngine;

public partial class StageInfoUI : MonoBehaviour // Data Field
{
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI hpText;

}
public partial class StageInfoUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {

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
public partial class StageInfoUI : MonoBehaviour // 
{
    public void RefreshInfoUI()
    {
        stageText.text = $"Stage{MainSystem.Instance.StageManager.StageInformation.stage_id}- {MainSystem.Instance.StageManager.StageController.CurrentSubStageIndex}";
        coinText.text = MainSystem.Instance.StageManager.InGameCoin.ToString();
        hpText.text = MainSystem.Instance.StageManager.StageController.StageHp.ToString();
    }
}