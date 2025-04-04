using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class PlayerBuildingData  // Data Field
{
    [System.Serializable]
    public class PlayerBuildingInformation : BaseInformation
    {
        public string name;
        public string ui_name;
        public string description;
        public string usable_bullet_name;
        public int[] damage_array;
        public float[] attack_range_array;
        public float[] attack_delay_array;
        public string building_type;
        public int cost;
        public int upgrad_cost;
        public float resell_cost_percent;
        public int max_level;
        public int unlock_level;
        public int unlock_cost;
    }
}


public partial class PlayerBuildingData  // Data Field
{
    private Dictionary<string, PlayerBuildingInformation> playerBuildingInfoDicit;
}


public partial class PlayerBuildingData  // Initialize
{
    private void Allocate()
    {
        playerBuildingInfoDicit = new Dictionary<string, PlayerBuildingInformation>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        MainSystem.Instance.DataManager.LoadCsv<PlayerBuildingInformation>("PlayerBuildingData", playerBuildingInfoDicit);
    }
}
public partial class PlayerBuildingData  // Property
{
    public PlayerBuildingInformation GetData(BuildingName buildingName)
    {
        string name = buildingName.ToString();
        return playerBuildingInfoDicit.FirstOrDefault(elem => elem.Value.name == name).Value;
    }
}