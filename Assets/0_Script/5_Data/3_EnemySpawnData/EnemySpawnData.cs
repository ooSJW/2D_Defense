using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public partial class EnemySpawnData // Information
{
    [System.Serializable]
#pragma warning disable CS0659 // 형식은 Object.Equals(object o)를 재정의하지만 Object.GetHashCode()를 재정의하지 않습니다.
    public class EnemySpawnInformation : BaseInformation
#pragma warning restore CS0659 // 형식은 Object.Equals(object o)를 재정의하지만 Object.GetHashCode()를 재정의하지 않습니다.
    {
        public string spawn_group_name;

        public string[] spawn_enemy_name_array;
        public float[] spawn_percent_array;

        public int spawn_count;
        public float spawn_delay;

        public override bool Equals(object obj)
        {
            if (obj is EnemySpawnInformation other)
            {
                return
                    index == other.index &&
                    spawn_group_name == other.spawn_group_name &&
                    spawn_count == other.spawn_count &&
                    spawn_delay == other.spawn_delay &&
                    spawn_enemy_name_array.SequenceEqual(other.spawn_enemy_name_array) &&
                    spawn_percent_array.SequenceEqual(other.spawn_percent_array);
            }
            else
                return false;
        }
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