using UnityEngine;

public partial class EnemyAnimation : MonoBehaviour // Data Field
{
    private Enemy enemy;
}
public partial class EnemyAnimation : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        Vector3 scale = transform.localScale;
        scale.x = 1;
        transform.localScale = scale;
    }
    public void Initialize(Enemy enemyValue)
    {
        enemy = enemyValue;
        Allocate();
        Setup();
    }
    private void Setup()
    {

    }
}
public partial class EnemyAnimation : MonoBehaviour // Main
{
    public void LateProgress()
    {
        FlipX();
    }
}
public partial class EnemyAnimation : MonoBehaviour // Property
{
    private void FlipX()
    {
        Vector3 currentPosition = transform.position;
        Vector3 nextWayPoint = enemy.EnemyMovement.GetNextWayPoint();

        Vector3 scale = transform.localScale;
        if (currentPosition.x > nextWayPoint.x)
            scale.x = 1;
        else if (currentPosition.x < nextWayPoint.x)
            scale.x = -1;

        transform.localScale = scale;
    }
}