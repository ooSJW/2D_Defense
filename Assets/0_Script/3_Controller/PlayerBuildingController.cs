using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class PlayerBuildingController : MonoBehaviour // Data Field
{
    private Color canPlaceColor;
    private Color cannotPlaceColor;

    private List<PlayerBuilding> activeBuildingList;
}

public partial class PlayerBuildingController : MonoBehaviour // Data Property
{
    private PlayerBuilding selectedBuilding;
    public PlayerBuilding SelectedBuilding
    {
        get => selectedBuilding;
        set
        {
            if (selectedBuilding != value)
            {
                selectedBuilding = value;
                selectedBuilding.Initialize();
                if (selectedBuilding != null)
                    IsPlacing = true;
            }
        }
    }

    private bool isPlacing;
    public bool IsPlacing
    {
        get => isPlacing;
        private set
        {
            if (isPlacing != value)
                isPlacing = value;

            if (isPlacing)
                SelectedBuilding.DrawAttackRange();
            else
            {
                selectedBuilding.EndDrawAttackRange();
                selectedBuilding = null;
            }
        }
    }
}

public partial class PlayerBuildingController : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        isPlacing = false;
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
public partial class PlayerBuildingController : MonoBehaviour // Main
{
    private void Update()
    {
        StartPlaceBuilding();
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

    public void StartPlaceBuilding()
    {
        if (isPlacing && SelectedBuilding != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            SelectedBuilding.transform.position = mousePosition;

            PlacingBuilding();
        }
    }

    public void PlacingBuilding()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;

            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity);
            if (hit.collider.CompareTag(TagType.Structable.ToString()))
            {
                SelectedBuilding.SetBuildingColor(true);
                if (Input.GetMouseButtonDown(0))
                    PlaceBuilding(hit.collider);
            }
            else
                SelectedBuilding.SetBuildingColor(false);
        }
        else
            SelectedBuilding.SetBuildingColor(false);
    }
    public void PlaceBuilding(Collider2D hitCollider)
    {
        SelectedBuilding.transform.position = hitCollider.bounds.center;
        hitCollider.tag = TagType.UnStructable.ToString();
        IsPlacing = false;
    }
}