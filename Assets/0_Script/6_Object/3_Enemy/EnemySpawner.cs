using System.Collections;
using UnityEngine;
using static EnemySpawnData;

public partial class EnemySpawner : MonoBehaviour // Data Field
{

}
public partial class EnemySpawner : MonoBehaviour // Initialize
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
public partial class EnemySpawner : MonoBehaviour // Property
{
    public IEnumerator SpawnEnemies()
    {
        EnemySpawnInformation spawnInfo = MainSystem.Instance.EnemySpawnManager.EnemySpawnInformation;
        for (int i = 0; i < spawnInfo.spawn_enemy_name_array.Length; i++)
        {
            for (int j = 0; j < spawnInfo.spawn_count_array[i]; j++)
            {
                Enemy enemy = MainSystem.Instance.PoolManager.SpawnEnemy(spawnInfo.spawn_enemy_name_array[i], transform, transform.position);
                MainSystem.Instance.EnemyManager.SignupEnemy(enemy);
                yield return new WaitForSeconds(spawnInfo.spawn_delay);
            }
        }
        MainSystem.Instance.EnemySpawnManager.EnemySpawnController.SetSpawnEnemy(false);
        yield break;
    }
}