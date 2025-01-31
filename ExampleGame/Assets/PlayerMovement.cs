using UnityEngine;
using UnityEngine.UI;  // Add this to access the UI Text element

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;

    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Camera Reference")]
    public Transform cameraTransform; // Assign CameraFollow in Inspector

    private float speed;
    private bool isGrounded;

    [Header("Donut Collecting")]
    public int donutCount = 0;  // Counter for collected donuts
    public Text donutCountText;  // Reference to the UI Text element

    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = walkSpeed;
    }

    void Update()
    {
        // Ground Check
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Get Movement Input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Move relative to camera direction
        Vector3 move = cameraTransform.forward * moveZ + cameraTransform.right * moveX;
        move.y = 0; // Prevent unintended vertical movement

        // Rotate Player
        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Apply movement
        controller.Move(move.normalized * speed * Time.deltaTime);

        // Sprinting
        speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Apply Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Collect Donuts on E key press
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check if there is a nearby collectible donut
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Donut"))
                {
                    CollectDonut(hitCollider.gameObject);
                }
            }
        }

        // Update the UI Text to show the donut count
        donutCountText.text = "Donuts Collected: " + donutCount;
    }

    // Method to collect the donut
    void CollectDonut(GameObject donut)
    {
        donutCount++;  // Increase the donut count
        Destroy(donut);  // Remove the collected donut
    }
}
