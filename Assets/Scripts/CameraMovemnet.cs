using UnityEngine;

public class CameraMovemnet : MonoBehaviour
{
    [SerializeField] Transform target;           // veikėjas (Player)
    [SerializeField] float smoothSpeed = 5f;     // kaip greitai kamera prisitaiko
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10); // kad kamera būtų už veikėjo

    void FixedUpdate()
    {
        if (target == null)
            return;

        // Nauja pozicija tik X ir Y ašyse (Z lieka -10)
        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            offset.z
        );

        // Švelnus judėjimas
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
