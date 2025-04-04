using System;
using System.IO;
using UnityEngine;

public partial class UIManager : MonoBehaviour // Data Field
{
    public UIController UIController { get; private set; }

    private UISaveData saveData;
    private string savePath;
}
public partial class UIManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        savePath = Path.Combine(Application.persistentDataPath, "UISaveData.json");
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
public partial class UIManager : MonoBehaviour // Data Property
{
    public void SaveData(bool sfx, bool bgm)
    {
        if (saveData == null)
            saveData = new UISaveData();

        saveData.isActiveSfx = sfx ? 1 : 0;
        saveData.isActiveBgm = bgm ? 1 : 0;

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
            saveData = new UISaveData();
            return false;
        }
    }

    public void LoadData()
    {
        string json = File.ReadAllText(savePath);
        saveData = JsonUtility.FromJson<UISaveData>(json);
        UIController.OptionUI.LoadData(Convert.ToBoolean(saveData.isActiveSfx), Convert.ToBoolean(saveData.isActiveBgm));
    }

    public void InitialData()
    {
        if (File.Exists(savePath))
            File.Delete(savePath);
    }
}

public partial class UIManager : MonoBehaviour // Sign
{
    public void SignupUIController(UIController uiController)
    {
        UIController = uiController;
        UIController.Initialize();
    }
    public void SigndownUIContoller()
    {
        UIController = null;
    }
}