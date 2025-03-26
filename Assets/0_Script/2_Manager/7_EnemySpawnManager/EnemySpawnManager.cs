using UnityEngine;

public partial class EnemySpawnManager : MonoBehaviour // Data Field
{
    public EnemySpawnController EnemySpawnController { get; private set; } = null;
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
public partial class EnemySpawnManager : MonoBehaviour // Sign
{
    //TODO TEST
    public Enemy SpawnEnemy(string enemyName, Vector3 spawnPosition)
    {
        GameObject gameObject = MainSystem.Instance.PoolManager.Spawn(enemyName, null, spawnPosition);
        Enemy enemy = gameObject.GetComponent<Enemy>();
        enemy.Initialize();
        return enemy;
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