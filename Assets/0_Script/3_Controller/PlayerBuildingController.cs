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
    [SerializeField] private LayerMask playerBuildingLayer;
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
                if (selectedBuilding != null)
                    selectedBuilding.EndDrawAttackRange();

                if (value != null)
                {
                    if (!value.IsActive)
                        IsPlacing = true;
                    value.DrawAttackRange();
                }
                else
                    IsPlacing = false;

                selectedBuilding = value;
                MainSystem.Instance.UIManager.UIController.PlayerBuildingInfoUI.ChangePlayerBuilding(selectedBuilding);
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
            {
                isPlacing = value;
                if (!isPlacing)
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
        SelectBuilding();
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

    public void SelectBuilding()
    {
        if (!IsPlacing)
        {
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, playerBuildingLayer);
                if (hit)
                {
                    SelectedBuilding = hit.collider.GetComponent<PlayerBuilding>();
                }
                else
                {
                    SelectedBuilding = null;
                }
            }
        }
    }

    public void StartPlaceBuilding()
    {
        if (IsPlacing && SelectedBuilding != null)
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
                        // SelectedBuilding이 cotroller.activeBuildingList에 없을 경우 비활
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
        int reSellCost = SelectedBuilding.GetResellCost();

        SelectedBuilding.transform.parent.gameObject.layer = LayerMask.NameToLayer("Structable");
        activeBuildingList.Remove(SelectedBuilding);
        MainSystem.Instance.PoolManager.Despawn(SelectedBuilding.gameObject);
        MainSystem.Instance.StageManager.GetCoin(reSellCost);
        SelectedBuilding = null;
    }

    public void UpgradBuilding()
    {
        int upgradCost = SelectedBuilding.PlayerBuildingInformation.upgrad_cost;

        if (SelectedBuilding.CanUpgrad())
        {
            if (MainSystem.Instance.StageManager.SpendCoin(upgradCost))
            {
                SelectedBuilding.Upgrad();
            }
        }
    }
}