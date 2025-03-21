using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public partial class TileController : MonoBehaviour // Data Field
{
    [SerializeField] private List<TileGroup> tileGroupList;
}

public partial class TileController : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        if (tileGroupList != null)
        {
            if (tileGroupList.Count == 0)
                Debug.Log("tileGroup.Count == 0");

            for (int i = 0; i < tileGroupList.Count; i++)
            {
                tileGroupList[i].Initialize();
            }
        }
        else
            Debug.Log("TileGroup Is Null");

    }
}
public partial class TileController : MonoBehaviour // 
{

}