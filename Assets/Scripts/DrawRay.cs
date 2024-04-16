using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawRay : MonoBehaviour
{
    public Transform rayOrigin;
    
    private Vector3 direction;
    
    public float rayLength = 5f;
    
    public Color rayColor = Color.red;

    private LineRenderer lineRenderer;

    void Start()
    {
        direction = rayOrigin.forward;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.color = rayColor; // Set color of the Line Renderer's material
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (rayOrigin == null)
            return;

        // Set line positions
        lineRenderer.SetPosition(0, rayOrigin.position);
        lineRenderer.SetPosition(1, rayOrigin.position + direction.normalized * rayLength);
    }
}