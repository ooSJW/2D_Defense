using System;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;

public partial class SoundManager : MonoBehaviour // Data Field
{
    public SoundController SoundController { get; private set; } = null;

    private SoundSaveData saveData;
    private string savePath;
}

public partial class SoundManager : MonoBehaviour // Data Property
{
    private bool isActiveSfx;
    public bool IsActiveSfx
    {
        get => isActiveSfx;
        private set
        {
            if (isActiveSfx != value)
            {
                isActiveSfx = value;
                SoundController.SetIsActiveSfx(isActiveSfx);
            }
        }
    }

    private bool isActiveBgm;
    public bool IsActiveBgm
    {
        get => isActiveBgm;
        private set
        {
            if (isActiveBgm != value)
            {
                isActiveBgm = value;
                SoundController.SetIsActiveBgm(isActiveBgm);
            }
        }
    }
}

public partial class SoundManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        savePath = Path.Combine(Application.persistentDataPath, "SoundSaveData.json");
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        HasData();
    }
}

public partial class SoundManager : MonoBehaviour // DataProperty
{
    public void SaveSoundData(bool sfx, bool bgm)
    {
        if (saveData == null)
            saveData = new SoundSaveData();

        saveData.isActiveSfx = sfx ? 1 : 0;
        saveData.isActiveBgm = bgm ? 1 : 0;

        IsActiveSfx = sfx;
        IsActiveBgm = bgm;

        SaveData();
    }
    public void SaveData()
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
    }

    public bool HasData()
    {
        if (File.Exists(savePath))
        {
            LoadData();
            return true;
        }
        else
        {
            saveData = new SoundSaveData();
            return false;
        }
    }

    public void LoadData()
    {
        string json = File.ReadAllText(savePath);
        saveData = JsonUtility.FromJson<SoundSaveData>(json);
        isActiveSfx = Convert.ToBoolean(saveData.isActiveSfx);
        isActiveBgm = Convert.ToBoolean(saveData.isActiveBgm);
    }

    public void InitialData()
    {
        if (File.Exists(savePath))
            File.Delete(savePath);
    }
}
public partial class SoundManager : MonoBehaviour // Sign
{
    public void SignupSoundColtroller(SoundController soundController)
    {
        SoundController = soundController;
        SoundController.Initialize();
    }

    public void SigndownSoundController()
    {
        SoundController = null;
    }
}