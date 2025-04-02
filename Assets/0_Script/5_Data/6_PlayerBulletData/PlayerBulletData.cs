using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class PlayerBulletData  // Information
{
    [System.Serializable]
    public class PlayerBulletInformation : BaseInformation
    {
        public string name;
        public string bullet_type;
        public float damage_range;
        public int max_hit_object_count;
    }
}

public partial class PlayerBulletData  // Data Field
{
    private Dictionary<string, PlayerBulletInformation> playerBulletInformationDict;
}

public partial class PlayerBulletData  // Initialize
{
    private void Allocate()
    {
        playerBulletInformationDict = new Dictionary<string, PlayerBulletInformation>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        MainSystem.Instance.DataManager.LoadCsv<PlayerBulletInformation>("PlayerBulletData", playerBulletInformationDict);
    }
}

public partial class PlayerBulletData  // Property
{
    public PlayerBulletInformation GetData(BulletName bulletName)
    {
        return playerBulletInformationDict.FirstOrDefault(elem => elem.Value.name == bulletName.ToString()).Value;
    }
}