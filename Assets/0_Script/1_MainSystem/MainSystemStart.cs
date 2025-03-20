using UnityEngine;

public partial class MainSystemStart : MonoBehaviour // Main
{
    private void Awake()
    {
        MainSystem.Instance.MainSystemStart();
    }
}
