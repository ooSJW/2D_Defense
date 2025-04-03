using UnityEngine;

public partial class PlayerBuildingAttackDetector : MonoBehaviour // Data Field
{
    private PlayerBuilding playerBuilding;
    [SerializeField] private CircleCollider2D circleCollider;
}
public partial class PlayerBuildingAttackDetector : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        UpdateAttackRange();
        circleCollider.isTrigger = true;
    }
    public void Initialize(PlayerBuilding playerBuildingValue)
    {
        playerBuilding = playerBuildingValue;
        Allocate();
        Setup();
    }
    private void Setup()
    {

    }
}
public partial class PlayerBuildingAttackDetector : MonoBehaviour // Property
{
    public void UpdateAttackRange()
    {
        circleCollider.radius = playerBuilding.PlayerBuildingInformation.attack_range_array[playerBuilding.CurrentIndex];
    }

    public void StartTargetSerching()
    {
        circleCollider.enabled = true;
    }
}

public partial class PlayerBuildingAttackDetector : MonoBehaviour // Trigger Event
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerBuilding.PlayerBuildingCombat.enemyTransformList.Add(collision.transform);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Transform enemyTransform = collision.transform;
        if (playerBuilding.PlayerBuildingCombat.enemyTransformList.Contains(enemyTransform))
            playerBuilding.PlayerBuildingCombat.enemyTransformList.Remove(enemyTransform);
    }
}