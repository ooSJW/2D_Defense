using UnityEngine;
using UnityEngine.SceneManagement;

public partial class MainSystem : GenericSingleton<MainSystem> // Data Field
{
    public SceneManager SceneManager { get; private set; } = null;
    public DataManager DataManager { get; private set; } = null;
    public PoolManager PoolManager { get; private set; } = null;
    public StageManager StageManager { get; private set; } = null;
    public TileManager TileManager { get; private set; } = null;
    public PlayerManager PlayerManager { get; private set; } = null;
    public PlayerBuildingManager PlayerBuildingManager { get; private set; } = null;
    public EnemySpawnManager EnemySpawnManager { get; private set; } = null;
    public EnemyManager EnemyManager { get; private set; } = null;
    public UIManager UIManager { get; private set; } = null;
}
public partial class MainSystem : GenericSingleton<MainSystem>// Initialize
{
    private void Allocate()
    {
        SceneManager = gameObject.AddComponent<SceneManager>();
        DataManager = gameObject.AddComponent<DataManager>();
        PoolManager = gameObject.AddComponent<PoolManager>();
        StageManager = gameObject.AddComponent<StageManager>();
        TileManager = gameObject.AddComponent<TileManager>();
        PlayerManager = gameObject.AddComponent<PlayerManager>();
        PlayerBuildingManager = gameObject.AddComponent<PlayerBuildingManager>();
        EnemySpawnManager = gameObject.AddComponent<EnemySpawnManager>();
        EnemyManager = gameObject.AddComponent<EnemyManager>();
        UIManager = gameObject.AddComponent<UIManager>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();

        SceneManager.Initialize();
        DataManager.Initialize();
        PoolManager.Initialize();
        StageManager.Initialize();
        TileManager.Initialize();
        PlayerManager.Initialize();
        PlayerBuildingManager.Initialize();
        EnemySpawnManager.Initialize();
        EnemyManager.Initialize();
        UIManager.Initialize();
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
        SceneManager.LoadScene(SceneName.InGameScene);
    }
}