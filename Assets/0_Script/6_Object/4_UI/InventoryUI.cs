using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PlayerBuildingData;

public partial class InventoryUI : MonoBehaviour // Data Field
{
    private Queue<StoreBuildingUI> buildingUIQueue;
    [SerializeField] private RectTransform contentTransform;
}
public partial class InventoryUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        buildingUIQueue = new Queue<StoreBuildingUI>();
        gameObject.SetActive(false);
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
public partial class InventoryUI : MonoBehaviour // 
{
    public void StoreObjectFilter()
    {
        BuildingName[] names = MainSystem.Instance.PlayerManager.Player.UnlockedBuildingNameList.ToArray();
        for (int i = 0; i < names.Length; i++)
        {
            PlayerBuildingInformation info =
                 MainSystem.Instance.DataManager.PlayerBuildingData.GetData(names[i]);
            SpawnStoreObject(info);
        }

    }
    private void SpawnStoreObject(PlayerBuildingInformation info)
    {
        StoreBuildingUI storeBuildingUI = MainSystem.Instance.PoolManager.Spawn("StoreBuildingUI", contentTransform).GetComponent<StoreBuildingUI>();
        storeBuildingUI.Initialize(info, false);
        buildingUIQueue.Enqueue(storeBuildingUI);
    }

    public void OnOffInventory()
    {
        bool isActive = gameObject.activeSelf;
        if (isActive)
        {
            while (buildingUIQueue.Count > 0)
            {
                StoreBuildingUI ui = buildingUIQueue.Dequeue();
                MainSystem.Instance.PoolManager.Despawn(ui.gameObject);
            }
        }
        else
            StoreObjectFilter();

        gameObject.SetActive(!isActive);
    }
}