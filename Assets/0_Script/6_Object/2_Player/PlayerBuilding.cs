using UnityEngine;

public partial class PlayerBuilding : MonoBehaviour // Data Field
{
    [SerializeField] private ParticleSystem muzzleParticle;
}
public partial class PlayerBuilding : MonoBehaviour // Initialize
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
public partial class PlayerBuilding : MonoBehaviour // 
{
    public void PlayParticle()
    {
        muzzleParticle.Play();
    }
    public void PauseParticle()
    {
        muzzleParticle.Stop(true);
    }
}