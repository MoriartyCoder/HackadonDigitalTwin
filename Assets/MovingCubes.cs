using UnityEngine;

public class CubeController : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Color originalColor;
    private Renderer cubeRenderer;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        originalColor = cubeRenderer.material.color;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) // Right-click
        {
            offset = transform.position - GetMouseWorldPos();
            isDragging = true;
            
            ChangeColor(Color.green);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        ChangeColor(originalColor);
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 targetPos = GetMouseWorldPos() + offset;
            transform.position = new Vector3(targetPos.x, transform.position.y, targetPos.z);
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }

        return Vector3.zero;
    }

    void ChangeColor(Color newColor)
    {
        cubeRenderer.material.color = newColor;
    }
}