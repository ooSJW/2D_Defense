using UnityEngine;
using static EnemySpawnData;

public partial class EnemySpawnManager : MonoBehaviour // Data Property
{
    public EnemySpawnController EnemySpawnController { get; private set; } = null;


    private EnemySpawnInformation enemySpawnInformation;
    public EnemySpawnInformation EnemySpawnInformation
    {
        get => enemySpawnInformation;

        private set
        {
            if (enemySpawnInformation == null || !enemySpawnInformation.Equals(value))
            {
                enemySpawnInformation = new EnemySpawnInformation()
                {
                    index = value.index,
                    spawn_group_name = value.spawn_group_name,
                    spawn_enemy_name_array = value.spawn_enemy_name_array,
                    spawn_count_array = value.spawn_count_array,
                    spawn_delay = value.spawn_delay,
                };

            }
        }
    }
}
public partial class EnemySpawnManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {

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

public partial class EnemySpawnManager : MonoBehaviour // Property
{
    public void RefreshSpawnData(string groupName)
    {
        EnemySpawnInformation = MainSystem.Instance.DataManager.EnemySpawnData.GetData(groupName);
    }
}

public partial class EnemySpawnManager : MonoBehaviour // Sign
{
    public void SignupEnemySpawnController(EnemySpawnController enemySpawnController)
    {
        EnemySpawnController = enemySpawnController;
        EnemySpawnController.Initialize();
    }
    public void SigndownEnemySpawnController()
    {
        EnemySpawnController = null;
    }
}