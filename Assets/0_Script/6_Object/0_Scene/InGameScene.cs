using System.Collections;
using UnityEngine;

public partial class InGameScene : BaseScene // Data Field
{

}
public partial class InGameScene : BaseScene // Initialize
{
    private void Allocate()
    {

    }
    public override void Initialize()
    {
        base.Initialize();
        Allocate();
        Setup();
        CheckFirstPlay();
    }
    private void Setup()
    {

    }
}

public partial class InGameScene : BaseScene // Property
{
    private void CheckFirstPlay()
    {
        if (SceneName == SceneName.Stage00Scene)
        {
            bool isFirstPlay = PlayerPrefs.GetInt("IsFirstPlay", 1) == 0 ? false : true;
            if (isFirstPlay)
            {
                MainSystem.Instance.UIManager.UIController.ActiveDialogue();
            }
        }
    }
}