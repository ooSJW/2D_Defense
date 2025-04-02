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
        // TODO ���콺�� Raycast�� ��ȣ�ۿ� ���� �ʵ��� ��������, �Ƹ� ��ġ �� collider�� �Ѹ� ������ ���۵Ǹ� ���콺�� rayCast�� �浹��
        // ���� ���� ���� �ٸ� ��ž�� ��ġ�� �� ���Եɰ���.
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