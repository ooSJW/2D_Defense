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
}