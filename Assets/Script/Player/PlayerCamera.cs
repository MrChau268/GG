using UnityEngine;


public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;              // Player to follow
    public Vector3 offset = new Vector3(0, 5, -7);
    public float smoothSpeed = 0.125f;
    public float mouseSensitivity = 2f;

    private float yaw = 0f;   // horizontal rotation
    private float pitch = 20f; // vertical rotation

    void LateUpdate()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -40f, 60f); // stop camera flipping

        // Rotate only the camera
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.LookAt(target); // keep focus on the player
    }
}
