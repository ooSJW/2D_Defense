using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class PlayerData  // Information
{
    [System.Serializable]
    public class PlayerInformation : BaseInformation
    {
        public int level;
        public int max_exp;
        public int max_level;
        public string[] unlocked_building_array;
    }
}


public partial class PlayerData  // Data Field
{
    private Dictionary<string, PlayerInformation> playerInformationDict;
}


public partial class PlayerData  // Initialize
{
    private void Allocate()
    {
        playerInformationDict = new Dictionary<string, PlayerInformation>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        MainSystem.Instance.DataManager.LoadCsv<PlayerInformation>("PlayerData", playerInformationDict);
    }
}
public partial class PlayerData  // Property
{
    public PlayerInformation GetData(int level)
    {
        return playerInformationDict.FirstOrDefault(elem => elem.Value.level == level).Value;
    }
}