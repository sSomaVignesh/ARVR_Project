using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f; // Adjust this value to control mouse sensitivity
    void Start()
    {
        // Lock the cursor at the start of the game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private float verticalRotation = 0f;

    void Update()
    {
        // Get input from the keyboard
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;

        // Move the player
        transform.Translate(movement);

        // Handle mouse input for camera panning
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; // Adjust sensitivity here

        // Rotate the player based on mouse input
        transform.Rotate(Vector3.up * mouseX);

        // Handle mouse input for head movement (up and down)
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity; // Adjust sensitivity here

        // Update the vertical rotation for the camera
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Adjust the range based on your preference

        // Apply the rotations to the camera and player separately
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, transform.localRotation.eulerAngles.y, 0f);
    }
}
