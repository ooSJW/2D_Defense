using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static PlayerData;

public partial class Player : MonoBehaviour // Data Field
{
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; } = null;
    public List<BuildingName> UnlockedBuildingNameList { get; private set; } = null;
}

public partial class Player : MonoBehaviour // Data Property
{
    private PlayerInformation playerInformation;
    public PlayerInformation PlayerInformation
    {
        get => playerInformation;
        private set
        {
            playerInformation = new PlayerInformation()
            {
                index = value.index,
                level = value.level,
                max_exp = value.max_exp,
                max_level = value.max_level,
                unlocked_building_array = value.unlocked_building_array,
            };
            MaxExp = value.max_exp;
            maxLevel = value.max_level;
        }
    }

    private int level;
    public int Level
    {
        get => level;
        private set
        {
            if (level != value)
            {
                if (maxLevel != null && value > maxLevel)
                    return;

                level = value;
                try
                {
                    PlayerInformation = MainSystem.Instance.DataManager.PlayerData.GetData(level);
                    string[] unlockBuildingArray = PlayerInformation.unlocked_building_array;
                    for (int i = 0; i < unlockBuildingArray.Length; i++)
                    {
                        UnlockBuilding(unlockBuildingArray[i]);
                    }
                }
                catch
                {
                    Debug.LogWarning($"PlayerInformation Is Null\nPlayerInfoDict Not Contains Key [{level}]");
                }
            }
        }
    }

    private int exp;
    public int Exp
    {
        get => exp;
        private set
        {
            if (exp != value)
            {
                exp = value;
                while (exp >= maxExp)
                {
                    exp -= MaxExp;
                    Level++;
                }
            }
        }
    }

    private int maxExp;
    public int MaxExp { get => maxExp; private set => maxExp = value; }

    private int? maxLevel;
}


public partial class Player : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        UnlockedBuildingNameList = new List<BuildingName>();
        Level = PlayerPrefs.GetInt("PlayerLevel", 1);
    }
    public void Initialize()
    {
        Allocate();
        Setup();
        PlayerInput.Initialize(this);
    }
    private void Setup()
    {

    }
}
public partial class Player : MonoBehaviour // Property
{
    public void UnlockBuilding(string buildingName)
    {
        BuildingName building = BuildingName.None;
        if (Enum.TryParse<BuildingName>(buildingName, true, out building))
        {
            if (!UnlockedBuildingNameList.Contains(building) && building != BuildingName.None)
                UnlockedBuildingNameList.Add(building);
        }
    }
}