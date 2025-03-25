using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerBuildingController : MonoBehaviour // Data Field
{
    private Color canPlaceColor;
    private Color cannotPlaceColor;

    private List<PlayerBuilding> activeBuildingList;
}
public partial class PlayerBuildingController : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        activeBuildingList = new List<PlayerBuilding>();

        canPlaceColor = Color.white;
        canPlaceColor = Color.red;
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
public partial class PlayerBuildingController : MonoBehaviour // Property
{
    public void BuildingPreview(PlayerBuilding building, Vector2 position, bool canPlace = false)
    {
        SpriteRenderer sprite = building.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            building.transform.position = position;
            sprite.color = canPlace ? canPlaceColor : cannotPlaceColor;
        }
    }
}