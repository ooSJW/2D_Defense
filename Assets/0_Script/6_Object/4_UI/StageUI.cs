using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static StageData;

public partial class StageUI : MonoBehaviour // Data Field
{
    private StageInformation stageInformation;
    [SerializeField] private GameObject lockObject;
    [SerializeField] private Button goStageButton;
    [SerializeField] private Image mapImage;

    [SerializeField] private TextMeshProUGUI stageNameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private bool isUnlock;
    public bool IsUnlock
    {
        get => isUnlock;
        private set
        {
            if (isUnlock != value)
            {
                isUnlock = value;
                if (isUnlock)
                {
                    goStageButton.interactable = true;
                    lockObject.SetActive(false);
                }
                else
                {
                    goStageButton.interactable = false;
                    lockObject.SetActive(true);
                }
            }
        }
    }
}
public partial class StageUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        goStageButton.interactable = false;
        lockObject.SetActive(true);
        goStageButton.onClick.AddListener(LoadStage);
    }
    public void Initialize(StageInformation stageInformationValue, bool isUnlockValue)
    {
        stageInformation = stageInformationValue;
        Allocate();
        Setup();
        IsUnlock = isUnlockValue;
        RefreshUI();
        // TODO TEST
        print("TEST");
        goStageButton.interactable = true;
    }
    private void Setup()
    {

    }
}
public partial class StageUI : MonoBehaviour // Property
{
    public void RefreshUI()
    {
        stageNameText.text = $"Stage{stageInformation.stage_id}";
        descriptionText.text = $"StageHp : {stageInformation.stage_hp}\n지급 골드 : {stageInformation.initial_coin}\n몬스터 웨이브 : {stageInformation.last_sub_stage}";
    }

    private void LoadStage()
    {
        MainSystem.Instance.StageManager.ChangeStage(stageInformation.stage_id);
        string stageName = string.Format("Stage{0:D2}Scene", stageInformation.stage_id);
        if (Enum.TryParse(stageName, true, out SceneName sceneName))
            MainSystem.Instance.SceneManager.LoadScene(sceneName);
        else
            Debug.LogWarning($"StageName Parse Error [name {stageName}]");
    }
}