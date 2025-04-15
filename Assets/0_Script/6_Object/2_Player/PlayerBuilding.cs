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
    private SoundEffectName soundEffectName;
    private float spriteSize;

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
                sound_clip_name = value.sound_clip_name,
            };
            TotalCostValue += value.cost;
        }
    }

    private int level;
    public int Level
    {
        get => level;
        private set
        {
            if (level != value && value > 0 && value <= PlayerBuildingInformation.max_level)
            {
                level = value;
                currentIndex = level > 0 ? level - 1 : 0;
                PlayerBuildingAttackDetector.UpdateAttackRange();
                PlayerBuildingCombat.UpdateAttackDelay();
                DrawAttackRange();
            }

        }
    }
    private int currentIndex;
    public int CurrentIndex { get => currentIndex; }

    private bool isActive;
    public bool IsActive
    {
        get => isActive;
        set
        {
            if (isActive != value)
                isActive = value;
        }
    }

    private int totalCostValue;
    public int TotalCostValue { get => totalCostValue; private set => totalCostValue = value; }
}


public partial class PlayerBuilding : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        if (Enum.TryParse<BuildingName>(name, true, out buildingName))
        {
            PlayerBuildingInformation = MainSystem.Instance.DataManager.PlayerBuildingData.GetData(buildingName);
            soundEffectName = Enum.Parse<SoundEffectName>(playerBuildingInformation.sound_clip_name);
        }
        else
            Debug.LogWarning($"Parse Error Check [{name}] prefab Name or Enum field");

        spriteSize = attackRangespriteRenderer.bounds.size.x;
        attackRangespriteRenderer.enabled = false;
        PauseParticle();
    }
    public void Initialize()
    {
        Allocate();
        PlayerBuildingCombat.Initialize(this);
        PlayerBuildingAttackDetector.Initialize(this);
        PlayerBuildingAnimation.Initialize(this);
        Setup();
    }
    private void Setup()
    {
        Level = 1;
        IsActive = false;
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
        if (IsActive)
        {
            PlayerBuildingCombat.Progress();
        }
    }

    private void LateUpdate()
    {
        if (IsActive)
        {
            PlayerBuildingAnimation.LateProgress();
        }
    }
}

public partial class PlayerBuilding : MonoBehaviour // Property
{
    public void PlaceBuilding()
    {
        IsActive = true;
    }
    public bool CanUpgrad()
    {
        if (PlayerBuildingInformation.max_level > Level)
            return true;
        else
            return false;
    }

    public void Upgrad()
    {
        Level++;
        TotalCostValue += PlayerBuildingInformation.upgrad_cost;
    }

    public void PlayParticle()
    {
        muzzleParticle.Play();
        MainSystem.Instance.SoundManager.SoundController.PlaySoundSffect(soundEffectName);
    }
    public void PauseParticle()
    {
        muzzleParticle.Stop(true);
    }

    public void DrawAttackRange()
    {
        float attackRange = PlayerBuildingInformation.attack_range_array[currentIndex];
        float scaleValue = (attackRange * 2) / spriteSize;

        attackRangespriteRenderer.enabled = true;
        attackRangespriteRenderer.transform.localScale = new Vector3(scaleValue, scaleValue, 1);

    }
    public int GetResellCost()
    {
        float reSellPercent = PlayerBuildingInformation.resell_cost_percent;
        int reSellCost = (int)(TotalCostValue * reSellPercent);

        return reSellCost;
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