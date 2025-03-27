using UnityEngine;

public partial class SceneManager : MonoBehaviour // Data Field
{
    public BaseScene ActiveScene { get; private set; }
    public SceneName LoadSceneName { get; private set; }
}
public partial class SceneManager : MonoBehaviour // Initialize
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


public partial class SceneManager : MonoBehaviour // Property
{
    public void LoadScene(SceneName loadSceneName)
    {
        LoadSceneName = loadSceneName;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName.LoadingScene.ToString());
    }
}


public partial class SceneManager : MonoBehaviour // Sign
{
    public void SignupActiveScene(BaseScene activeScene)
    {
        ActiveScene = activeScene;
        ActiveScene.Initialize();
    }
    public void SigndownActiveScene()
    {
        ActiveScene = null;
    }
}