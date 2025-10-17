using UnityEngine;

public class CameraMovemnet : MonoBehaviour
{
    [SerializeField] Transform target; // Object which camera is tracking
    [SerializeField] float smoothSpeed = 5f; // Smoother tracking speed
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10); // Position between camera and target

    /// <summary>
    /// The camera tracks the target with 
    /// delay/smoothness without moving abruptly.
    /// </summary>
    void FixedUpdate()
    {
        if (target == null) // If target is null, the method exits.
            return;

        // Determines where the camera should be based on the target and offset.
        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            offset.z
        );

        // Gradually moves the camera towards the desired position using linear interpolation with a divider
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
