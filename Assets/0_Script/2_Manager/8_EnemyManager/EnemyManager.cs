using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyManager : MonoBehaviour // Data Field
{
    public EnemyController EnemyController { get; private set; } = null;
    public List<Enemy> AllFieldEnemyList { get; private set; } = null;
}
public partial class EnemyManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        AllFieldEnemyList = new List<Enemy>();
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
public partial class EnemyManager : MonoBehaviour // Sign
{
    public void SignupEnemyController(EnemyController enemyController)
    {
        EnemyController = enemyController;
        EnemyController.Initialize();
    }

    public void SigndownEnemyController()
    {
        EnemyController = null;
    }

    public void SignupEnemy(Enemy enemy)
    {
        AllFieldEnemyList.Add(enemy);
        enemy.Initialize();
    }

    public void SigndownEnemy(Enemy enemy)
    {
        // TODO TEST
        MainSystem.Instance.PoolManager.DespawnEnemy(enemy);
        //
        AllFieldEnemyList.Remove(enemy);
        EnemyController.CheckStageState();
    }
}