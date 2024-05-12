using UnityEngine;

public class ObjectMouseMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to control the speed of movement

    void Update()
    {
        // Get mouse movement along the X and Y axes
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate movement vector based on mouse movement
        Vector3 movement = new Vector3(mouseX, mouseY, 0f) * moveSpeed * Time.deltaTime;

        // Apply movement to the object's position
        transform.Translate(movement);
    }
}