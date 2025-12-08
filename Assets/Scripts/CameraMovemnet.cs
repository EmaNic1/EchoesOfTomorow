using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMovemnet : MonoBehaviour
{
    [SerializeField] Transform target; // Object which camera is tracking
    [SerializeField] float smoothSpeed = 5f; // Smoother tracking speed
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10); // Position between camera and target
    //[SerializeField] private Tilemap groundTilemap;

    /// <summary>
    /// The camera tracks the target with 
    /// delay/smoothness without moving abruptly.
    /// </summary>
    void LateUpdate()
    {
        if (target == null) return;
        
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
    
    public void SnapToTarget(Vector3 newTargetPosition)
    {
        if (target == null) return;

        transform.position = newTargetPosition + offset;
    }
}
