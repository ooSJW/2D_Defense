using UnityEngine;
using UnityEngine.EventSystems;

public partial class CameraDrag : MonoBehaviour // Data Field
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;
    [SerializeField] private float dragSpeed;

    private Vector3 lastMousePosition;
    private bool isDragging = false;
}
public partial class CameraDrag : MonoBehaviour // Main
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButton(1) && isDragging)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 delta = Input.mousePosition - lastMousePosition;
                Vector3 move = new Vector3(-delta.x, -delta.y, 0) * dragSpeed * Time.deltaTime;

                Vector3 newPosition = mainCamera.transform.position + move;

                newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
                newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

                mainCamera.transform.position = newPosition;

                lastMousePosition = Input.mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }
    }
}
