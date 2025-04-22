using NUnit.Framework;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

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

    public void PlaySoundSffect(SoundEffectName clipName)
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