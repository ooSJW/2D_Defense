using UnityEngine;
using static PlayerBuildingData;

public partial class PlayerBuilding : MonoBehaviour // Data Field
{
    [SerializeField] private ParticleSystem muzzleParticle;
}

public partial class PlayerBuilding : MonoBehaviour // Data Property
{
    private PlayerBuildingInformation playerBuildingInformation;
    public PlayerBuildingInformation PlayerBuildingInformation
    {
        get => playerBuildingInformation;
        private set
        {
            playerBuildingInformation = new PlayerBuildingInformation()
            {
                index = value.index,
                name = value.name,
                ui_name = value.ui_name,
                description = value.description,
                damage_array = value.damage_array,
                attack_delay_array = value.attack_delay_array,
                attack_range_array = value.attack_range_array,
                building_type = value.building_type,
                cost = value.cost,
                resell_cost_percent = value.resell_cost_percent,
                max_level = value.max_level,
                upgrad_cost = value.upgrad_cost,
            };
        }
    }
}


public partial class PlayerBuilding : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        PlayerBuildingInformation = MainSystem.Instance.DataManager.PlayerBuildingData.GetData(name);
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