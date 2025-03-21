using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public partial class TileGroup : MonoBehaviour // Data Field
{

}
public partial class TileGroup : MonoBehaviour // Initialize
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
        for (int i = 0; i < transform.childCount; i++)
        {
            SpriteRenderer renderer = transform.GetChild(i).GetComponent<SpriteRenderer>();
            renderer.sortingOrder = (int)(transform.position.y * -1);
        }
    }
}
public partial class TileGroup : MonoBehaviour // 
{

}