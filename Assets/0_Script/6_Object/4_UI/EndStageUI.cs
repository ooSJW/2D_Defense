using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static StageData;

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
        lobbyButton.onClick.AddListener(GobackLobby);
        retryButton.onClick.AddListener(RetryGame);
        nextStageButton.onClick.AddListener(GoNextStage);
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        retryButton.gameObject.SetActive(false);
        nextStageButton.gameObject.SetActive(false);
        gameObject.SetActive(false);
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

    private void RetryGame()
    {
        StageInformation info = MainSystem.Instance.StageManager.StageInformation;
        string stageName = string.Format("Stage{0:D2}Scene", info.stage_id);
        if (Enum.TryParse(stageName, true, out SceneName sceneName))
            MainSystem.Instance.SceneManager.LoadScene(sceneName);
        else
            Debug.LogWarning($"StageName Parse Error [name {stageName}]");
    }

    private void GobackLobby()
    {
        MainSystem.Instance.SceneManager.LoadScene(SceneName.LobbyScene);
    }

    private void GoNextStage()
    {
        StageInformation info = MainSystem.Instance.StageManager.StageInformation;
        int nextStageId = info.stage_id + 1;
        MainSystem.Instance.StageManager.ChangeStage(nextStageId);
        string stageName = string.Format("Stage{0:D2}Scene", nextStageId);
        if (Enum.TryParse(stageName, true, out SceneName sceneName))
            MainSystem.Instance.SceneManager.LoadScene(sceneName);
        else
            Debug.LogWarning($"StageName Parse Error [name {stageName}]");
    }

}