using UnityEngine;

public partial class BackgroundMusic : MonoBehaviour // Data Field
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;
}
public partial class BackgroundMusic : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        audioSource.clip = clip;
        audioSource.volume = 0.5f;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }
    public void Initialize(bool isActive)
    {
        Allocate();
        Setup();

        if (isActive)
            BackgroundMusicStart();
        else
            BackgroundMusicStop();
    }
    private void Setup()
    {

    }
}
public partial class BackgroundMusic : MonoBehaviour // Property
{
    public void BackgroundMusicStart()
    {
        audioSource.Play();
    }

    public void BackgroundMusicStop()
    {
        audioSource.Stop();
    }
}