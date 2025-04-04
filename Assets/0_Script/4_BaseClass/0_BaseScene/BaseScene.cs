using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class BaseScene : MonoBehaviour // Data Field
{
    private SceneName sceneName;
    public SceneName SceneName { get => sceneName; }
    public SceneType SceneType { get; private set; }

    public List<GameObject> poolableObjectList;
    public List<Enemy> poolableEnemyList;
    private Player player;

    [Header("InGameScene Member")]
    [SerializeField] private StageController stageController;
    [SerializeField] private TileController tileController;
    [SerializeField] private PlayerBuildingController playerBuildingController;
    [SerializeField] private EnemySpawnController enemySpawnController;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private UIController uiController;

    [field: SerializeField] public GameObject EnemyTarget { get; private set; }
    [field: SerializeField] public GameObject EnemySpawn { get; private set; }

}
public partial class BaseScene : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        if (!Enum.TryParse<SceneName>(name, out sceneName))
            Debug.LogWarning($"Scene Name Parse Error / [name : {name}]");
    }
    public virtual void Initialize()
    {
        Allocate();
        Setup();

        SceneInitialize();
    }
    private void Setup()
    {

    }
}

public partial class BaseScene : MonoBehaviour // Private Property
{
    private void SceneInitialize()
    {
        SceneName[] initializeScene = { SceneName.InitializeScene, SceneName.LoadingScene };
        SceneName[] notCombatScene = { SceneName.LobbyScene };
        SceneName[] combatScene = { SceneName.InGameScene };

        SceneName currentSceneName = initializeScene.FirstOrDefault(elem => elem == SceneName);

        if (currentSceneName != SceneName.None) // Check Initialize Scene 
        {
            SceneType = SceneType.Initialize;
            return;
        }

        MainSystem.Instance.PoolManager.Register();
        player = MainSystem.Instance.PoolManager.Spawn("Player").GetComponent<Player>();

        currentSceneName = notCombatScene.FirstOrDefault(elem => elem == SceneName);
        if (currentSceneName != SceneName.None) // Not CombatScene Initialzie
        {
            SceneType = SceneType.NotCombat;
            MainSystem.Instance.PlayerManager.SignupPlayer(player);
            MainSystem.Instance.UIManager.SignupUIController(uiController);
            return;
        }

        currentSceneName = combatScene.FirstOrDefault(elem => elem == SceneName);
        if (currentSceneName != SceneName.None) // CombatScene Initialize
        {
            SceneType = SceneType.Combat;
            MainSystem.Instance.StageManager.SignupStageController(stageController);
            MainSystem.Instance.TileManager.SignupTileController(tileController);
            MainSystem.Instance.PlayerManager.SignupPlayer(player);
            MainSystem.Instance.PlayerBuildingManager.SignupPlayerBuildingController(playerBuildingController);
            MainSystem.Instance.EnemySpawnManager.SignupEnemySpawnController(enemySpawnController);
            MainSystem.Instance.EnemyManager.SignupEnemyController(enemyController);
            MainSystem.Instance.UIManager.SignupUIController(uiController);
            MainSystem.Instance.StageManager.StageController.CurrentSubStageIndex++;
            return;
        }

        Debug.LogWarning($"Check SceneName [currentSceneName : {name}]");
    }
}

public partial class BaseScene : MonoBehaviour // Main
{
    private void Awake()
    {
        MainSystem.Instance.SceneManager.SignupActiveScene(this);
    }
}