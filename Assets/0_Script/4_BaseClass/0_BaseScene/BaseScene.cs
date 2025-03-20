using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public partial class BaseScene : MonoBehaviour // Data Field
{
    private SceneName sceneName;
    public SceneName SceneName { get => sceneName; }

    public List<GameObject> poolableObjectList;
}
public partial class BaseScene : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        if (!Enum.TryParse<SceneName>(name, out sceneName))
            Debug.LogWarning($"Scene Name Parse Error / [name : {name}]");
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        MainSystem.Instance.PoolManager.Register();
    }
}
public partial class BaseScene : MonoBehaviour // Main
{
    private void Awake()
    {
        MainSystem.Instance.SceneManager.SignupActiveScene(this);
    }
}