using UnityEngine;

public partial class PlayerManager : MonoBehaviour // Data Field
{
    public PlayerController PlayerController { get; private set; } = null;
}
public partial class PlayerManager : MonoBehaviour // Initialize
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
public partial class PlayerManager : MonoBehaviour // Sign
{
    public void SignupPlayerController(PlayerController playerController)
    {
        PlayerController = playerController;
        PlayerController.Initialize();
    }
    public void SigndownPlayerController()
    {
        PlayerController = null;
    }
}