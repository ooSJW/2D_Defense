using UnityEngine;

public partial class MouseOverHighlight : MonoBehaviour // Data Field
{
    private SpriteRenderer highlightRenderer;
    private GameObject currentHighlightObject;
    private Color originColor;
    [SerializeField] private LayerMask structableLayer;
}
public partial class MouseOverHighlight : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        originColor = Color.white;
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
public partial class MouseOverHighlight : MonoBehaviour // 
{
    private void Start()
    {
        Initialize();
    }
    private void Update()
    {
        DetectedMouse();
    }
    private void DetectedMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, structableLayer);

        if (hit)
        {
            SpriteRenderer renderer = hit.collider.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                if (highlightRenderer != renderer)
                {
                    if (highlightRenderer != null)
                    {
                        highlightRenderer.color = originColor;
                    }
                    renderer.color = Color.yellow;
                    highlightRenderer = renderer;
                }
            }
        }
        else
        {
            if (highlightRenderer != null)
            {
                highlightRenderer.color = originColor;
                highlightRenderer = null;
            }
        }
    }
}