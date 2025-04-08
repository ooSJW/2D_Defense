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

    [Header("LobbyMember")]
    [SerializeField] private GameObject dataInitialUI;
    [SerializeField] private GameObject quitGameUI;
}
public partial class OptionUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        onSwitchImage = Resources.Load<Sprite>("Icon/SwitchOn");
        offSwitchImage = Resources.Load<Sprite>("Icon/SwitchOff");
        gameObject.SetActive(false);
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
            // TODO 25 04 04 소리 끄기 켜기 데이터 세이브로드, 플ㄹ레이어 세이브 로드 만들었음, 
            // 생각해보고 구조 이상하면 다시 만들기 
            // 이상없을 시 스테이지 기본 지급 골드 작성 및 enemy dropReward 구현, 건물 배치 시 재화 차감
            // 포탑 해금 시 저장 및 재화 차감 구현 해야함
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
        MainSystem.Instance.SoundManager.SaveData(isActiveSfx, isActiveBgm);
    }
}