using UnityEngine;
using UnityEngine.UI;

public partial class OptionUI : MonoBehaviour // Data Field
{
    [SerializeField] private Image sfxBtnImage;
    [SerializeField] private Image bgmBtnImage;
    private bool isActiveSfx;
    private bool isActiveBgm;

    private Sprite onSwitchImage;
    private Sprite offSwitchImage;

    [Header("CombatSceneMember")]
    [SerializeField] private Button lobbyButton;

    [Header("LobbyMember")]
    [SerializeField] private Button dataInitialButton;
    [SerializeField] private Button quitGameButton;
}
public partial class OptionUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        onSwitchImage = Resources.Load<Sprite>("Icon/SwitchOn");
        offSwitchImage = Resources.Load<Sprite>("Icon/SwitchOff");
        gameObject.SetActive(false);

        dataInitialButton.gameObject.SetActive(false);
        quitGameButton.gameObject.SetActive(false);
        lobbyButton.gameObject.SetActive(false);
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        if (!MainSystem.Instance.SoundManager.HasData())
        {
            isActiveSfx = true;
            isActiveBgm = true;
            SaveData();
        }

        SceneType sceneType = MainSystem.Instance.SceneManager.ActiveScene.SceneType;
        switch (sceneType)
        {
            case SceneType.NotCombat:
                dataInitialButton.gameObject.SetActive(true);
                quitGameButton.gameObject.SetActive(true);
                break;
            case SceneType.Combat:
                lobbyButton.gameObject.SetActive(true);
                break;
        }
    }
}
public partial class OptionUI : MonoBehaviour // Property
{
    public void LoadData(bool isActiveSfx, bool isActiveBgm)
    {
        sfxBtnImage.sprite = isActiveSfx ? onSwitchImage : offSwitchImage;
        bgmBtnImage.sprite = isActiveBgm ? onSwitchImage : offSwitchImage;
        this.isActiveSfx = isActiveSfx;
        this.isActiveBgm = isActiveBgm;
    }

    public void OnOffSfx()
    {
        isActiveSfx = !isActiveSfx;
        sfxBtnImage.sprite = isActiveSfx ? onSwitchImage : offSwitchImage;
        SaveData();
    }

    public void OnOffBgm()
    {
        isActiveBgm = !isActiveBgm;
        bgmBtnImage.sprite = isActiveBgm ? onSwitchImage : offSwitchImage;
        SaveData();
    }
    private void SaveData()
    {
        MainSystem.Instance.SoundManager.SaveSoundData(isActiveSfx, isActiveBgm);
    }
}