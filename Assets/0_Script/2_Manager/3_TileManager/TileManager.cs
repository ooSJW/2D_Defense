using UnityEngine;

public partial class TileManager : MonoBehaviour // Data Field
{
    public TileController TileController { get; private set; } = null;
}

public partial class TileManager : MonoBehaviour // Initialize
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
public partial class TileManager : MonoBehaviour // Sign
{
    public void SignupTileController(TileController tileController)
    {
        TileController = tileController;
        TileController.Initialize();
    }

    public void SigndownTileController()
    {
        TileController = null;
    }
}