using UnityEngine;

public partial class EnemyManager : MonoBehaviour // Data Field
{
    public EnemyController EnemyController { get; private set; } = null;
}
public partial class EnemyManager : MonoBehaviour // Initialize
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
}