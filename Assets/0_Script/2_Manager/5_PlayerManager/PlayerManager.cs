using System.IO;
using UnityEngine;

public partial class PlayerManager : MonoBehaviour // Data Field
{
    public Player Player { get; private set; } = null;

 
    private PlayerSaveData saveData;
    private string savePath;
}
public partial class PlayerManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        savePath = Path.Combine(Application.persistentDataPath, "PlayerSaveData.json");
        LoadData();
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
public partial class PlayerManager : MonoBehaviour // Property
{
    public void SaveData()
    {
        saveData.level = Player.Level;
        saveData.exp = Player.Exp;
        saveData.unlocked_building_array = Player.PlayerInformation.unlocked_building_array;
        // TODO : saveData.coin == 게임 통합 재화, 스테이지에서 사용할 재화는 stageManager나 controller에 따로 작성할거(csv포함);
        // TODO 25 04 04 여기부터 ㄱㄱ
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
    }

    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<PlayerSaveData>(json);
        }
        else
            saveData = new PlayerSaveData();
    }
}


public partial class PlayerManager : MonoBehaviour // Sign
{
    public void SignupPlayer(Player player)
    {
        Player = player;
        Player.Initialize();
    }
    public void SigndownPlayer()
    {
        Player = null;
    }
}