using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerBuildingData;

public partial class PlayerBuilding : MonoBehaviour // Data Field
{
    [field: SerializeField] public PlayerBuildingAttackDetector PlayerBuildingAttackDetector { get; private set; } = null;
    [field: SerializeField] public PlayerBuildingCombat PlayerBuildingCombat { get; private set; } = null;
    [field: SerializeField] public PlayerBuildingAnimation PlayerBuildingAnimation { get; private set; } = null;

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
                usable_bullet_name = value.usable_bullet_name,
                damage_array = value.damage_array,
                attack_delay_array = value.attack_delay_array,
                attack_range_array = value.attack_range_array,
                building_type = value.building_type,
                cost = value.cost,
                resell_cost_percent = value.resell_cost_percent,
                max_level = value.max_level,
                upgrad_cost = value.upgrad_cost,
                unlock_cost = value.unlock_cost,
                unlock_level = value.unlock_level,
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
    public int CurrentIndex { get => currentIndex; }

    private bool isPlacing;
    public bool IsPlacing
    {
        get => isPlacing;
        set
        {
            if (isPlacing != value)
                isPlacing = value;
        }
    }

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
        IsPlacing = true;
    }
    public void Initialize()
    {
        Allocate();
        Setup();
        PlayerBuildingCombat.Initialize(this);
        PlayerBuildingAttackDetector.Initialize(this);
        PlayerBuildingAnimation.Initialize(this);
    }
    private void Setup()
    {

    }
}

public partial class PlayerBuilding : MonoBehaviour // Main
{
    private void OnEnable()
    {
        Enemy.OnEnemyDeath += PlayerBuildingCombat.RemoveEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= PlayerBuildingCombat.RemoveEnemy;
    }

    private void Update()
    {
        if (!IsPlacing)
        {
            PlayerBuildingCombat.Progress();
        }
    }

    private void LateUpdate()
    {
        if (!IsPlacing)
        {
            PlayerBuildingAnimation.LateProgress();
        }
    }
}

public partial class PlayerBuilding : MonoBehaviour // Property
{
    public void PlaceBuilding()
    {
        IsPlacing = false;
    }
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
        float spriteSize = attackRangespriteRenderer.bounds.size.x;
        float scaleValue = (attackRange * 2) / spriteSize;

        attackRangespriteRenderer.transform.localScale = new Vector3(scaleValue, scaleValue, 1);
        attackRangespriteRenderer.enabled = true;

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