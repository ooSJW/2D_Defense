using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static PlayerBuildingData;

public partial class StoreUI : MonoBehaviour // Data Field
{
    private List<StoreBuildingUI> buildingUIList;
    [SerializeField] private RectTransform contentTransform;
}

public partial class StoreUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        buildingUIList = new List<StoreBuildingUI>();
        gameObject.SetActive(false);
    }
    public void Initialize()
    {
        Allocate();
        Setup();
        StoreObjectFilter();
    }
    private void Setup()
    {

    }
}

public partial class StoreUI : MonoBehaviour // Property
{

    public void OnOffStore()
    {
        bool isActive = gameObject.activeSelf;
        if (isActive)
            gameObject.SetActive(false);
        else
        {
            RefreshStore();
            gameObject.SetActive(true);
        }
    }

    public void StoreObjectFilter()
    {
        string[] allBuildingName = MainSystem.Instance.DataManager.PlayerBuildingData.GetAllBuildingName();
        for (int i = 0; i < allBuildingName.Length; i++)
        {
            if (Enum.TryParse<BuildingName>(allBuildingName[i], true, out BuildingName name))
            {
                PlayerBuildingInformation info =
                     MainSystem.Instance.DataManager.PlayerBuildingData.GetData(name);
                if (info.unlock_level != 0 && info.unlock_cost != 0)
                    SpawnStoreObject(info);
            }
        }

    }

    private void SpawnStoreObject(PlayerBuildingInformation info)
    {
        StoreBuildingUI storeBuildingUI = MainSystem.Instance.PoolManager.Spawn("StoreBuildingUI", contentTransform).GetComponent<StoreBuildingUI>();
        storeBuildingUI.Initialize(info, true);
        buildingUIList.Add(storeBuildingUI);
    }

    public void RefreshStore()
    {
        for (int i = 0; i < buildingUIList.Count; i++)
        {
            buildingUIList[i].RefreshUI();
        }
    }
}