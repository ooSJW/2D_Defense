using UnityEngine;
using UnityEngine.SceneManagement;

public partial class MainSystem : GenericSingleton<MainSystem> // Data Field
{
    public SceneManager SceneManager { get; private set; } = null;
    public DataManager DataManager { get; private set; } = null;
    public PoolManager PoolManager { get; private set; } = null;
}
public partial class MainSystem : GenericSingleton<MainSystem>// Initialize
{
    private void Allocate()
    {
        SceneManager = gameObject.AddComponent<SceneManager>();
        DataManager = gameObject.AddComponent<DataManager>();
        PoolManager = gameObject.AddComponent<PoolManager>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();

        SceneManager.Initialize();
        DataManager.Initialize();
        PoolManager.Initialize();
    }
    private void Setup()
    {

    }
}
public partial class MainSystem : GenericSingleton<MainSystem> // Property
{
    public void MainSystemStart()
    {
        Initialize();
        SceneManager.LoadScene(SceneName.LobbyScene);
    }
}