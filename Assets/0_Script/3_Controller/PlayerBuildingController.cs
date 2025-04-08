using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class PlayerBuildingController : MonoBehaviour // Data Field
{
    private Color canPlaceColor;
    private Color cannotPlaceColor;

    private List<PlayerBuilding> activeBuildingList;
    [SerializeField] private LayerMask structableLayer;
    [SerializeField] private LayerMask notWlakableLayer;
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
                if (selectedBuilding != null)
                {
                    selectedBuilding.Initialize();
                    IsPlacing = true;
                }
                else
                    IsPlacing = false;
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
                selectedBuilding.DrawAttackRange();
            else
            {
                if (selectedBuilding != null)
                    selectedBuilding.EndDrawAttackRange();
                SelectedBuilding = null;
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

            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, structableLayer);
            if (hit)
            {
                SelectedBuilding.SetBuildingColor(true);
                if (Input.GetMouseButtonDown(0))
                {
                    if (MainSystem.Instance.StageManager.SpendCoin(SelectedBuilding.PlayerBuildingInformation.cost))
                        PlaceBuilding(hit.collider);
                    else
                    {
                        MainSystem.Instance.PoolManager.Despawn(SelectedBuilding.gameObject);
                        IsPlacing = false;
                    }
                }
            }
            else
            {
                SelectedBuilding.SetBuildingColor(false);
                if (Input.GetMouseButtonDown(0))
                {
                    MainSystem.Instance.PoolManager.Despawn(SelectedBuilding.gameObject);
                    IsPlacing = false;
                }
            }
        }
        else
            SelectedBuilding.SetBuildingColor(false);
    }
    public void PlaceBuilding(Collider2D hitCollider)
    {
        SelectedBuilding.transform.position = hitCollider.bounds.center;
        SelectedBuilding.transform.SetParent(hitCollider.transform, true);
        hitCollider.gameObject.layer = LayerMask.NameToLayer("NotWalkable");
        SelectedBuilding.PlaceBuilding();
        activeBuildingList.Add(SelectedBuilding);
        IsPlacing = false;
    }

    public void ResellBuilding()
    {
        float reSellPercent = SelectedBuilding.PlayerBuildingInformation.resell_cost_percent;
        int reSellCost = (int)(SelectedBuilding.TotalCostValue * reSellPercent);

        SelectedBuilding.transform.parent.gameObject.layer = LayerMask.NameToLayer("Structable");
        activeBuildingList.Remove(SelectedBuilding);
        MainSystem.Instance.PoolManager.Despawn(SelectedBuilding.gameObject);
        MainSystem.Instance.StageManager.GetCoin(reSellCost);  // TODO 만들기만했음, 호출해서 실질적 사용 해야함
    }
}