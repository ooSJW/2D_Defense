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
        for (int i = 0; i < spawnInfo.spawn_count; i++)
        {
            int randomEnemy = Random.Range(0, spawnInfo.spawn_enemy_name_array.Length);
            Enemy enemy = MainSystem.Instance.PoolManager.SpawnEnemy(spawnInfo.spawn_enemy_name_array[randomEnemy], transform, transform.position);
            MainSystem.Instance.EnemyManager.SignupEnemy(enemy);
            yield return new WaitForSeconds(spawnInfo.spawn_delay);
        }

        MainSystem.Instance.EnemySpawnManager.EnemySpawnController.SetSpawnEnemy(false);
        yield break;
    }
}