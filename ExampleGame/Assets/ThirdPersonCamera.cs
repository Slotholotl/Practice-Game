using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Assign the Player
    public Vector3 offset = new Vector3(0, 2, -5);
    public float rotationSpeed = 2f;
    public float minDistance = 1f; // Minimum allowed distance from player
    public float maxDistance = 5f; // Default camera distance
    public float collisionRadius = 0.3f; // Radius for SphereCast
    public LayerMask collisionLayers; // Set to Default or Everything (Exclude Player)

    private float yaw = 0f;
    private float pitch = 0f;
    private float currentDistance;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentDistance = maxDistance;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Get Mouse Input
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        // Desired camera position
        Vector3 desiredPosition = target.position + Quaternion.Euler(pitch, yaw, 0) * offset.normalized * maxDistance;

        // Use SphereCast for better collision detection
        RaycastHit hit;
        if (Physics.SphereCast(target.position + Vector3.up * 1.5f, collisionRadius, desiredPosition - target.position, out hit, maxDistance, collisionLayers))
        {
            currentDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            currentDistance = maxDistance;
        }

        // Apply camera position with adjusted collision distance
        transform.position = target.position + Quaternion.Euler(pitch, yaw, 0) * offset.normalized * currentDistance;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
