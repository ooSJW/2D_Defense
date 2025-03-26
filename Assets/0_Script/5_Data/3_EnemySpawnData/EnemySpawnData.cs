using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class EnemySpawnData // Information
{
    [System.Serializable]
    public class EnemySpawnInformation : BaseInformation
    {
        public string spawn_group_name;

        public string[] spawn_enemy_name_array;
        public float[] spawn_percent_array;

        public int spawn_count;
        public float spawn_delay;
    }
}
public partial class EnemySpawnData // Data Field
{
    private Dictionary<string, EnemySpawnInformation> enemySpawnInformationDict;
}

public partial class EnemySpawnData // Initialize
{
    private void Allocate()
    {
        enemySpawnInformationDict = new Dictionary<string, EnemySpawnInformation>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        MainSystem.Instance.DataManager.LoadCsv<EnemySpawnInformation>("EnemySpawnData", enemySpawnInformationDict);
    }
}

public partial class EnemySpawnData // Property
{
    public EnemySpawnInformation GetData(string groupName)
    {
        return enemySpawnInformationDict.FirstOrDefault(elem => elem.Value.spawn_group_name.Equals(groupName)).Value;
    }
    public string[] GetKeys()
    {
        return enemySpawnInformationDict.Keys.ToArray();
    }

}