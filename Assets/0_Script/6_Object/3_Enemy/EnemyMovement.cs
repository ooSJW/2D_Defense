using UnityEngine;
using UnityEngine.AI;

public partial class EnemyMovement : MonoBehaviour // Data Field
{
    [SerializeField] private NavMeshAgent agent;
    private Enemy enemy;
    private NavMeshPath path;
    private Vector3 nextWayPoint;
    private int currentPathIndex;
    [SerializeField] private float moveSpeed; //TODO TEST
}
public partial class EnemyMovement : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        agent.updateUpAxis = false;
        path = new NavMeshPath();
        currentPathIndex = 0;

        if (enemy.TargetPosition != null)
        {
            if (!agent.CalculatePath(enemy.TargetPosition, path))
                Debug.LogWarning("Path Is Null");
        }
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
public partial class EnemyMovement : MonoBehaviour // Main
{
    public void Progress()
    {
        Movement();
    }
}
public partial class EnemyMovement : MonoBehaviour // Property
{
    private void Movement()
    {
        if (path != null && path.corners.Length > 0)
        {
            if (currentPathIndex < path.corners.Length)
            {
                nextWayPoint = path.corners[currentPathIndex];

                if (Vector3.Distance(transform.position, nextWayPoint) < 0.5f)
                    currentPathIndex++;
                transform.position = Vector3.MoveTowards(transform.position, nextWayPoint, moveSpeed * Time.deltaTime);
            }
        }
    }

    public Vector3 GetNextWayPoint()
    {
        return nextWayPoint;
    }
}