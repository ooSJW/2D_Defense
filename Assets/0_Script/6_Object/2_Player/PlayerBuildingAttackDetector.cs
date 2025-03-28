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
        circleCollider.enabled = false;
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
        circleCollider.radius = playerBuilding.GetCurrentAttackRange();
    }

    public void StartTargetSerching()
    {
        circleCollider.enabled = true;
        // TODO ���콺�� Raycast�� ��ȣ�ۿ� ���� �ʵ��� ��������, �Ƹ� ��ġ �� collider�� �Ѹ� ������ ���۵Ǹ� ���콺�� rayCast�� �浹��
        // ���� ���� ���� �ٸ� ��ž�� ��ġ�� �� ���Եɰ���.
    }
}