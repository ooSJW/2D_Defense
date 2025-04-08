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
            // TODO 25 04 04 �Ҹ� ���� �ѱ� ������ ���̺�ε�, �ä����̾� ���̺� �ε� �������, 
            // �����غ��� ���� �̻��ϸ� �ٽ� ����� 
            // �̻���� �� �������� �⺻ ���� ��� �ۼ� �� enemy dropReward ����, �ǹ� ��ġ �� ��ȭ ����
            // ��ž �ر� �� ���� �� ��ȭ ���� ���� �ؾ���
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