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
        // TODO 마우스의 Raycast와 상호작용 하지 않도록 만들어야함, 아마 배치 후 collider를 켜면 공격이 시작되며 마우스의 rayCast와 충돌해
        // 공격 범위 내에 다른 포탑을 배치할 수 없게될거임.
    }
}

public partial class PlayerBuildingAttackDetector : MonoBehaviour // Trigger Event
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagType.Enemy.ToString()))
        {
            playerBuilding.PlayerBuildingCombat.enemyTransformList.Add(collision.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagType.Enemy.ToString()))
        {
            Transform enemyTransform = collision.transform;
            if (playerBuilding.PlayerBuildingCombat.enemyTransformList.Contains(enemyTransform))
                playerBuilding.PlayerBuildingCombat.enemyTransformList.Remove(enemyTransform);
        }
    }
}