using UnityEngine;

public partial class PlayerBuildingAnimation : MonoBehaviour // Data Field
{
    private PlayerBuilding playerBuilding;

    [SerializeField] private Animator animator;
}
public partial class PlayerBuildingAnimation : MonoBehaviour // Initialize
{
    private void Allocate()
    {

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


public partial class PlayerBuildingAnimation : MonoBehaviour // Main
{
    public void LateProgress()
    {
        LookTarget();
    }
}


public partial class PlayerBuildingAnimation : MonoBehaviour // Property
{
    public void AttackTrigger()
    {
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Attack");
    }

    private void LookTarget()
    {
        Transform target = playerBuilding.PlayerBuildingCombat.Target;
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float smoothAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle + 180f, Time.deltaTime * 10f);
            transform.rotation = Quaternion.Euler(0, 0, smoothAngle);
        }
    }
}