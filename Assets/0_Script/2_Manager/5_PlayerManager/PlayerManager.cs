using System.IO;
using System.Linq;
using UnityEngine;

public partial class PlayerManager : MonoBehaviour // Data Field
{
    public Player Player { get; private set; } = null;


    private PlayerSaveData saveData;
    private string savePath;
}

public partial class PlayerManager : MonoBehaviour // Data Property
{
    private int coin;
    public int Coin
    {
        get => coin;
        private set
        {
            if (coin != value)
            {
                if (coin > value) // spendCoin
                {
                    coin = value;
                    SaveData();
                }
                else
                    coin = value;
            }
        }
    }
}

public partial class PlayerManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        savePath = Path.Combine(Application.persistentDataPath, "PlayerSaveData.json");
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
    public void SpendCoin(int costValue)
    {
        if (coin >= costValue)
            Coin -= costValue;
    }

    public void GetReawrd(int coinValue = 0, int expValue = 0)
    {
        Coin += coinValue;
        Player.GetExp(expValue);
    }
}

public partial class PlayerManager : MonoBehaviour // Data Property
{
    public void SaveData()
    {
        if (saveData == null)
            saveData = new PlayerSaveData();

        saveData.level = Player.Level;
        saveData.exp = Player.Exp;
        saveData.unlocked_building_array = Player.UnlockedBuildingNameList.Select(building => building.ToString()).ToArray();
        saveData.coin = coin;

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
            saveData = new PlayerSaveData();
            return false;
        }
    }

    public void LoadData()
    {
        string json = File.ReadAllText(savePath);
        saveData = JsonUtility.FromJson<PlayerSaveData>(json);
        Player.LoadPlayerData(saveData);
    }

    public void InitialData()
    {
        if (File.Exists(savePath))
            File.Delete(savePath);
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