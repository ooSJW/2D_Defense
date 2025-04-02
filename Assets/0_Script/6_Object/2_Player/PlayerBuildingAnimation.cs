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
        if (target == null)
            target = MainSystem.Instance.SceneManager.ActiveScene.EnemySpawn.transform;
        transform.LookAt(target);
    }
}