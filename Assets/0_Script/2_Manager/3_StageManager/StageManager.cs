using UnityEngine;

public partial class StageManager : MonoBehaviour // Data Field
{
    public StageController StageController { get; private set; } = null;
}
public partial class StageManager : MonoBehaviour // Initialize
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
public partial class StageManager : MonoBehaviour // Sign
{
    public void SignupStageController(StageController stageController)
    {
        StageController = stageController;
        StageController.Initialize();
    }

    public void SigndownStageController()
    {
        StageController = null;
    }
}