using System;
using System.IO;
using UnityEngine;

public partial class UIButtonEvent : MonoBehaviour // Data Field 
{
}
public partial class UIButtonEvent : MonoBehaviour // Property
{
    public void OnOffbuildingListUI()
    {
        MainSystem.Instance.UIManager.UIController.BuildingListUI.OnOffbuildingListUI();
    }

    public void ChangeScene(int sceneValue)
    {
        MainSystem.Instance.SceneManager.LoadScene(SceneName.LobbyScene);
    }

    public void SelectBuilding(BuildingName buildingName)
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        PlayerBuilding building = MainSystem.Instance.PlayerBuildingManager.PlayerBuildingController.SelectedBuilding;

        if (building != null)
        {
            MainSystem.Instance.PoolManager.Despawn(building.gameObject);
            MainSystem.Instance.PlayerBuildingManager.PlayerBuildingController.SelectedBuilding = null;
        }

        MainSystem.Instance.PlayerBuildingManager.PlayerBuildingController.SelectedBuilding =
            MainSystem.Instance.PoolManager.Spawn(buildingName.ToString(), null, position).GetComponent<PlayerBuilding>();
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
        MainSystem.Instance.UIManager.InitialData();

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