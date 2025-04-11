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

    }
}
public partial class SoundManager : MonoBehaviour // Sign
{
    public void SaveSoundData(bool sfx, bool bgm)
    {
        if (saveData == null)
            saveData = new SoundSaveData();

        saveData.isActiveSfx = sfx ? 1 : 0;
        saveData.isActiveBgm = bgm ? 1 : 0;

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
        MainSystem.Instance.UIManager.UIController.OptionUI.LoadData(Convert.ToBoolean(saveData.isActiveSfx), Convert.ToBoolean(saveData.isActiveBgm));
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