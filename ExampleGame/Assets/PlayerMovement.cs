using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float jumpHeight = 2f; // Height of the jump
    public float gravity = -9.81f; // Gravity force
    public float mouseSensitivity = 300f; // Sensitivity of mouse
    public Transform cameraTransform; // Reference to the camera

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f; // Tracks vertical rotation of the camera

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("No CharacterController found on the GameObject!");
        }

        if (cameraTransform == null)
        {
            Debug.LogError("No cameraTransform assigned! Please drag your camera into the script's cameraTransform field.");
        }

        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (controller == null || cameraTransform == null)
        {
            return; // Exit if setup is incomplete
        }

        // Handle looking around (camera rotation)
        HandleMouseLook();

        // Handle movement
        HandleMovement();
    }

    void HandleMouseLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically (clamped)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent flipping the camera
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    void HandleMovement()
    {
        // Ground check
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset velocity when grounded
        }

        // Movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Move relative to the player's forward direction
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Apply movement
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
