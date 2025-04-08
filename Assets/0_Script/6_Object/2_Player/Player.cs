using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
                        if (Enum.TryParse<BuildingName>(unlockBuildingArray[i], true, out BuildingName name))
                            UnlockBuilding(name);
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
        if (MainSystem.Instance.PlayerManager.HasData() == false)
            Level = 1;
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
    public void UnlockBuilding(BuildingName buildingName)
    {
        if (!UnlockedBuildingNameList.Contains(buildingName) && buildingName != BuildingName.None)
            UnlockedBuildingNameList.Add(buildingName);
    }

    public void GetExp(int expValue = 0)
    {
        Exp += expValue;
    }

    public void LoadPlayerData(PlayerSaveData playerSaveData)
    {
        Level = playerSaveData.level;
        Exp = playerSaveData.exp;
        foreach (string buildingName in playerSaveData.unlocked_building_array)
        {
            if (Enum.TryParse<BuildingName>(buildingName, true, out BuildingName name))
                UnlockBuilding(name);
            else
                Debug.LogWarning($"Name Parse Error[name : {buildingName}]");
        }
    }
}