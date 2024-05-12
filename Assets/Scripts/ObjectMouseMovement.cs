using UnityEngine;

public class ObjectMouseMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to control the speed of movement

    private Transform parentTransform;
    private Bounds parentBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        // Get the parent's transform
        parentTransform = transform.parent;

        // Calculate the bounding box of the parent object
        parentBounds = CalculateBounds(parentTransform.gameObject);

        // Get the width and height of the child object
        Renderer renderer = GetComponent<Renderer>();
        objectWidth = renderer.bounds.size.x;
        objectHeight = renderer.bounds.size.y;
    }

    void Update()
    {
        // Get mouse movement along the X and Y axes
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate movement vector based on mouse movement relative to parent's rotation
        Vector3 movement = new Vector3(mouseX, mouseY, 0f);

        // Apply parent's rotation to the movement vector
        movement = parentTransform.rotation * movement;

        // Scale movement by moveSpeed and Time.deltaTime
        movement *= moveSpeed * Time.deltaTime;

        // Calculate the new position relative to parent's position
        Vector3 newPosition = transform.localPosition + movement;

        // Clamp the new position to stay within the bounding box of the parent
        float minX = parentBounds.min.x + objectWidth / 2f;
        float maxX = parentBounds.max.x - objectWidth / 2f;
        float minY = parentBounds.min.y + objectHeight / 2f;
        float maxY = parentBounds.max.y - objectHeight / 2f;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Apply the clamped position to the object's local position
        transform.localPosition = newPosition;
    }

    private Bounds CalculateBounds(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        Bounds bounds = new Bounds(renderers[0].bounds.center, Vector3.zero);
        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
        return bounds;
    }
}