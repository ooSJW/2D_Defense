using UnityEngine;

public partial class SoundController : MonoBehaviour // Data Field
{
    [field: SerializeField] public SoundEffect SoundEffect { get; private set; } = null;
    [field: SerializeField] public BackgroundMusic BackgroundMusic { get; private set; } = null;
}
public partial class SoundController : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();

        SoundEffect.Initialize();
        BackgroundMusic.Initialize();
    }
    private void Setup()
    {

    }
}
public partial class SoundController : MonoBehaviour // 
{

}