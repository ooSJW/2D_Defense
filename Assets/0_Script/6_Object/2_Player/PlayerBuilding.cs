using System;
using UnityEngine;
using static PlayerBuildingData;

public partial class PlayerBuilding : MonoBehaviour // Data Field
{
    [field: SerializeField] public PlayerBuildingAttackDetector PlayerBuildingAttackDetector { get; private set; } = null;
    [SerializeField] private ParticleSystem muzzleParticle;
    [SerializeField] private SpriteRenderer buildingSpriteRenderer;
    [SerializeField] private SpriteRenderer attackRangespriteRenderer;

    private BuildingName buildingName;
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

    private int level;
    public int Level
    {
        get => level;
        private set
        {
            if (level != value && value > 0 && value < PlayerBuildingInformation.max_level)
            {
                currentIndex = level > 0 ? level - 1 : 0;
            }

        }
    }
    private int currentIndex;
}


public partial class PlayerBuilding : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        if (Enum.TryParse<BuildingName>(name, true, out buildingName))
            PlayerBuildingInformation = MainSystem.Instance.DataManager.PlayerBuildingData.GetData(buildingName);
        else
            Debug.LogWarning($"Parse Error Check [{name}] prefab Name or Enum field");

        attackRangespriteRenderer.enabled = false;
        PauseParticle();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
        PlayerBuildingAttackDetector.Initialize(this);
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

    public void DrawAttackRange()
    {
        float attackRange = PlayerBuildingInformation.attack_range_array[currentIndex];
        attackRangespriteRenderer.transform.localScale = new Vector3(attackRange * 2, attackRange * 2, 1);
        attackRangespriteRenderer.enabled = true;

    }

    public float GetCurrentAttackRange()
    {
        return PlayerBuildingInformation.attack_range_array[currentIndex];
    }

    public void EndDrawAttackRange()
    {
        attackRangespriteRenderer.enabled = false;
    }

    public void SetBuildingColor(bool structableValue)
    {
        buildingSpriteRenderer.color = structableValue ? Color.white : Color.red;
    }
}