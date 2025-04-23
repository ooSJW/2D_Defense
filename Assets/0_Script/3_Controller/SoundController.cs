using NUnit.Framework;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct SFXData
{
    public SoundEffectName soundEffectName;
    public AudioClip audioClip;
}

public partial class SoundController : MonoBehaviour // Data Field
{
    [SerializeField] private GameObject sfxObject;
    [field: SerializeField] public BackgroundMusic BackgroundMusic { get; private set; } = null;


    [SerializeField] private List<SFXData> sfxDataList;
    private Dictionary<SoundEffectName, AudioClip> sfxDict;
    private Queue<SoundEffect> sfxPool;
    [SerializeField] private int poolSize;

    private bool isActiveSfx;
    private bool isActiveBgm;
}
public partial class SoundController : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        sfxDict = new Dictionary<SoundEffectName, AudioClip>();
        sfxPool = new Queue<SoundEffect>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
        InitSfxDict();
        InitSfxPool();

        BackgroundMusic.Initialize(isActiveBgm);
    }
    private void Setup()
    {
        isActiveSfx = MainSystem.Instance.SoundManager.IsActiveSfx;
        isActiveBgm = MainSystem.Instance.SoundManager.IsActiveBgm;
    }
}
public partial class SoundController : MonoBehaviour // Property
{
    public void AddButtonSound()
    {
        Button[] button = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        for (int i = 0; i < button.Length; i++)
        {
            SoundEffectName sound = SoundEffectName.None;
            if (button[i].CompareTag("BuyBuilding"))
                sound = SoundEffectName.BuyBuilding;
            else
                sound = SoundEffectName.Click;

            // 클로저 캡처 이슈가 발생할 수 있기에 지역 변수에 값 복사 후 사용.
            // 클로저 캡처 : 람다가 sound변수를 참조 하는 방식으로 작동해서 그렇다는데...
            // 그냥 사용해도 문제 없었지만 혹시 모를 버그 방지를 위해 해당 방법을 기용함.
            SoundEffectName captureSound = sound;
            button[i].onClick.AddListener(() => PlaySoundEffect(captureSound));
        }
    }
    private void InitSfxDict()
    {
        foreach (SFXData sfxData in sfxDataList)
        {
            sfxDict[sfxData.soundEffectName] = sfxData.audioClip;
        }
    }

    private void InitSfxPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            sfxPool.Enqueue(CreateNewSoundEffect());
        }
    }

    public void PlaySoundEffect(SoundEffectName clipName)
    {
        if (isActiveSfx)
        {
            if (sfxDict.ContainsKey(clipName))
            {
                SoundEffect sfx = sfxPool.Count > 0 ? sfxPool.Dequeue() : CreateNewSoundEffect();
                sfx.gameObject.SetActive(true);
                sfx.PlaySoundEffect(sfxDict[clipName], ReturnToPool);
            }
        }
    }

    private SoundEffect CreateNewSoundEffect()
    {
        SoundEffect sfx = Instantiate(sfxObject, transform).GetComponent<SoundEffect>();
        sfx.Initialize();
        sfx.gameObject.SetActive(false);
        return sfx;
    }

    private void ReturnToPool(SoundEffect sfx)
    {
        sfx.gameObject.SetActive(false);
        sfxPool.Enqueue(sfx);
    }

    public void SetIsActiveSfx(bool sfx)
    {
        isActiveSfx = sfx;
    }

    public void SetIsActiveBgm(bool bgm)
    {
        isActiveBgm = bgm;

        if (isActiveBgm)
            BackgroundMusic.BackgroundMusicStart();
        else
            BackgroundMusic.BackgroundMusicStop();
    }
}