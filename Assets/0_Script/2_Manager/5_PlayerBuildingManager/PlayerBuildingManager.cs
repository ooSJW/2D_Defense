using UnityEngine;

public partial class PlayerBuildingManager : MonoBehaviour // Data Field
{
    public PlayerBuildingController PlayerBuildingController { get; private set; } = null;
}
public partial class PlayerBuildingManager : MonoBehaviour // Initialize
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
public partial class PlayerBuildingManager : MonoBehaviour // Sign
{
    public void SignupPlayerBuildingController(PlayerBuildingController playerBuildingController)
    {
        PlayerBuildingController = playerBuildingController;
        PlayerBuildingController.Initialize();
    }
    public void SigndownPlayerBuildingController()
    {
        PlayerBuildingController = null;
    }
}