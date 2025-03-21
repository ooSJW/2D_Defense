using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class BaseScene : MonoBehaviour // Data Field
{
    private SceneName sceneName;
    public SceneName SceneName { get => sceneName; }

    public List<GameObject> poolableObjectList;

    [Header("InGameScene Member")]
    [SerializeField] private TileController tileController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerBuildingController playerBuildingController;
    [SerializeField] private EnemySpawnController enemySpawnController;
    [SerializeField] private EnemyController enemyController;


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
        MainSystem.Instance.PoolManager.Register();
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

            return;
        }

        currentSceneName = notCombatScene.FirstOrDefault(elem => elem == SceneName);
        if (currentSceneName != SceneName.None) // Not CombatScene Initialzie
        {

            return;
        }

        currentSceneName = combatScene.FirstOrDefault(elem => elem == SceneName);
        if (currentSceneName != SceneName.None) // CombatScene Initialize
        {
            MainSystem.Instance.TileManager.SignupTileController(tileController);
            MainSystem.Instance.PlayerManager.SignupPlayerController(playerController);
            MainSystem.Instance.PlayerBuildingManager.SignupPlayerBuildingController(playerBuildingController);
            MainSystem.Instance.EnemySpawnManager.SignupEnemySpawnController(enemySpawnController);
            MainSystem.Instance.EnemyManager.SignupEnemyController(enemyController);

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