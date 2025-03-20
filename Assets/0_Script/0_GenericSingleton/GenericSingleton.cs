using UnityEngine;

public partial class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour // Data Field
{
    private static T instance;
    public static T Instance { get { Initialize(); return instance; } }
}
public partial class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour // Initialize
{
    private static void Initialize()
    {
        if (instance == null)
        {
            instance = GameObject.FindFirstObjectByType<T>();
            if (instance == null)
            {
                GameObject gameObject = new GameObject() { name = $"[GenericSingleton] : {typeof(T)}" };
                instance = gameObject.AddComponent<T>();
                GameObject.DontDestroyOnLoad(instance.gameObject);
            }
        }
    }

}