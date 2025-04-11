using System;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;

public partial class UIButtonEvent : MonoBehaviour // Property
{
    public void OnOffbuildingListUI()
    {
        MainSystem.Instance.UIManager.UIController.BuildingListUI.OnOffbuildingListUI();
    }

    public void ChangeScene(int sceneValue)
    {
        SceneName sceneName = (SceneName)sceneValue;
        MainSystem.Instance.SceneManager.LoadScene(sceneName);
    }


    public void SelectBuilding(BuildingName buildingName)
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        PlayerBuilding building = MainSystem.Instance.PlayerBuildingManager.PlayerBuildingController.SelectedBuilding;

        if (building != null && !building.IsActive)
        {
            MainSystem.Instance.PoolManager.Despawn(building.gameObject);
            MainSystem.Instance.PlayerBuildingManager.PlayerBuildingController.SelectedBuilding = null;
        }
        building = MainSystem.Instance.PoolManager.Spawn(buildingName.ToString(), null, position).GetComponent<PlayerBuilding>();
        building.Initialize();
        MainSystem.Instance.PlayerBuildingManager.PlayerBuildingController.SelectedBuilding =
            building;
    }

    public void StageStart()
    {
        MainSystem.Instance.StageManager.StageController.CurrentSubStageIndex++;
    }

    public void OnOffStore()
    {
        MainSystem.Instance.UIManager.UIController.StoreUI.OnOffStore();
    }

    public void OnOffInventory()
    {
        MainSystem.Instance.UIManager.UIController.InventoryUI.OnOffInventory();
    }

    public void OnOffSfx()
    {
        MainSystem.Instance.UIManager.UIController.OptionUI.OnOffSfx();
    }

    public void OnOffBgm()
    {
        MainSystem.Instance.UIManager.UIController.OptionUI.OnOffBgm();
    }

    public void ResetAllJsonFiles()
    {
        MainSystem.Instance.PlayerManager.InitialData();
        MainSystem.Instance.StageManager.InitialData();
        MainSystem.Instance.SoundManager.InitialData();
        PlayerPrefs.DeleteAll();
        QuitGame();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

}