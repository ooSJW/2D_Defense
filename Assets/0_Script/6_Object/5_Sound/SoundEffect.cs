using System;
using System.Collections;
using UnityEngine;

public partial class SoundEffect : MonoBehaviour // Data Field
{
    [SerializeField] private AudioSource audioSource;
    private Action<SoundEffect> onFinished;
}
public partial class SoundEffect : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        audioSource.playOnAwake = false;
        audioSource.volume = 0.3f;
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
public partial class SoundEffect : MonoBehaviour // Property
{
    public void PlaySoundEffect(AudioClip clip, Action<SoundEffect> callBack = null)
    {
        onFinished = callBack;
        audioSource.clip = clip;
        audioSource.Play();
        StartCoroutine(WaitAndReturn(clip.length));
    }

    private IEnumerator WaitAndReturn(float duration)
    {
        yield return new WaitForSeconds(duration);
        onFinished?.Invoke(this);
    }
}