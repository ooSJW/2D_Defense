using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class EndStageUI : MonoBehaviour // Data Field
{
    [SerializeField] private TextMeshProUGUI stageMessage;
    [SerializeField] private Button lobbyButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextStageButton;
}
public partial class EndStageUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        retryButton.gameObject.SetActive(false);
        nextStageButton.gameObject.SetActive(false);
        gameObject.SetActive(false);
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
public partial class EndStageUI : MonoBehaviour // Property
{
    public void InitialEndStageUI(bool isClear)
    {
        if (isClear)
        {
            stageMessage.text = "Stage Clear!";
            retryButton.gameObject.SetActive(false);

            if (MainSystem.Instance.StageManager.IsLastStage)
                nextStageButton.gameObject.SetActive(false);
            else
                nextStageButton.gameObject.SetActive(true);
        }
        else
        {
            stageMessage.text = "Stage Failed";
            retryButton.gameObject.SetActive(true);
            nextStageButton.gameObject.SetActive(false);
        }
        OnOffEndStageUI();
    }

    public void OnOffEndStageUI()
    {
        bool isActive = gameObject.activeSelf;
        gameObject.SetActive(!isActive);
    }
}