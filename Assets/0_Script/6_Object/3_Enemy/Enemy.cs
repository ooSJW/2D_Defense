using UnityEngine;

public partial class Enemy : MonoBehaviour // Data Property
{
    [field: SerializeField] public EnemyMovement EnemyMovement { get; private set; } = null;
    [field: SerializeField] public EnemyAnimation EnemyAnimation { get; private set; } = null;

    private Vector3 targetPosition;
    public Vector3 TargetPosition { get => targetPosition; private set => targetPosition = value; }
}
public partial class Enemy : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        TargetPosition = MainSystem.Instance.SceneManager.ActiveScene.GetEnemyTargetPosition();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
        EnemyMovement.Initialize(this);
        EnemyAnimation.Initialize(this);
    }
    private void Setup()
    {

    }
}
public partial class Enemy : MonoBehaviour // Main
{
    private void Update()
    {
        EnemyMovement.Progress();
    }
    private void LateUpdate()
    {
        EnemyAnimation.LateProgress();
    }
}