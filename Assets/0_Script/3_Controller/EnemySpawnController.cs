using UnityEngine;
using static EnemySpawnData;

public partial class EnemySpawnController : MonoBehaviour // Data Field
{
    [SerializeField] private EnemySpawner enemySpawner;
}

public partial class EnemySpawnController : MonoBehaviour // Data Field
{
    private bool spawnEnemy;
    public bool SpawnEnemy
    {
        get => spawnEnemy;
        private set
        {
            if (spawnEnemy != value)
            {
                spawnEnemy = value;
                if (spawnEnemy)
                {
                    StartCoroutine(enemySpawner.SpawnEnemies());
                }
            }
        }
    }
}


public partial class EnemySpawnController : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();

        enemySpawner.Initialize();
    }
    private void Setup()
    {

    }
}
public partial class EnemySpawnController : MonoBehaviour // Property
{
    public void SetSpawnEnemy(bool value)
    {
        SpawnEnemy = value;

        if (!spawnEnemy)
            MainSystem.Instance.EnemyManager.EnemyController.CheckStageState();
    }
}