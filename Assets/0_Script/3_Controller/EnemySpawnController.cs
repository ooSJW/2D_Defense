using UnityEngine;
using static EnemySpawnData;

public partial class EnemySpawnController : MonoBehaviour // Data Field
{
    private EnemySpawnInformation enemySpawnInformation;
    public EnemySpawnInformation EnemySpawnInformation
    {
        get => enemySpawnInformation;

        private set
        {
            enemySpawnInformation = new EnemySpawnInformation()
            {
                index = value.index,
                spawn_group_name = value.spawn_group_name,
                spawn_enemy_name_array = value.spawn_enemy_name_array,
                spawn_percent_array = value.spawn_percent_array,
                spawn_count = value.spawn_count,
                spawn_delay = value.spawn_delay,
            };
            // TODO Spawn
        }
    }
}
public partial class EnemySpawnController : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        EnemySpawnInformation = MainSystem.Instance.DataManager.EnemySpawnData.GetData("GroupA");
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
public partial class EnemySpawnController : MonoBehaviour // 
{

}