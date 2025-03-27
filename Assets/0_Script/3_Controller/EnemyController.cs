using UnityEngine;

public partial class EnemyController : MonoBehaviour // Data Field
{

}
public partial class EnemyController : MonoBehaviour // Initialize
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
public partial class EnemyController : MonoBehaviour // Property
{
    public void CheckStageState()
    {
        if (!MainSystem.Instance.EnemySpawnManager.EnemySpawnController.SpawnEnemy)
        {
            if (MainSystem.Instance.EnemyManager.AllFieldEnemyList.Count == 0)
                MainSystem.Instance.StageManager.StageController.CurrentSubStageIndex++;
        }
    }
}