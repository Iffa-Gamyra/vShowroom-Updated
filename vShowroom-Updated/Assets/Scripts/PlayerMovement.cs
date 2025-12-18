using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    public Transform targetObject; // Reference to the target object
    public bool recenterToTarget = false; // Toggle for recentering feature

    private Vector3 movement;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        Vector3 currentRotation = transform.localRotation.eulerAngles;
        xRotation = currentRotation.x;
        yRotation = currentRotation.y;

        if (xRotation > 180) xRotation -= 360;
    }

    void Update()
    {
        // Movement Input
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

        // Move the player
        Vector3 move = transform.right * movement.x + transform.forward * movement.z;
        transform.position += move * moveSpeed * Time.deltaTime;

        // Reset to start position and rotation
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
            Vector3 startEulerAngles = startRotation.eulerAngles;
            xRotation = startEulerAngles.x;
            yRotation = startEulerAngles.y;
        }

        // Mouse Look Input
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button pressed
        {
            if (recenterToTarget && targetObject != null)
            {
                RecenterViewToTarget();
            }

            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
            Cursor.visible = false; // Hide the cursor
        }
        else if (Input.GetMouseButtonUp(1)) // Right mouse button released
        {
            Cursor.lockState = CursorLockMode.None; // Free the cursor
            Cursor.visible = true; // Show the cursor
        }

        if (Input.GetMouseButton(1)) // Check if right mouse button is held down
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            yRotation += mouseX;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Apply rotation
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }

    private void RecenterViewToTarget()
    {
        Vector3 directionToTarget = targetObject.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        xRotation = targetRotation.eulerAngles.x;
        yRotation = targetRotation.eulerAngles.y;

        // Adjust xRotation if it's outside of -90 to 90 range
        if (xRotation > 180) xRotation -= 360;
    }
}
