using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public Transform playerCam;        // Camera or reference point for where the object will float
    public Transform holdPosition;     // The position in front of the player where the object will float
    public float pickUpDistance = 1f;  // Max distance from which you can pick up objects (1 meter)
    public float moveForce = 500f;     // The force applied to move the object to the hold position
    public float maxDistanceMultiplier = 1f;  // Used to dampen force when close to the hold position
    private GameObject heldObject;     // The object the player is holding
    private Rigidbody heldObjectRb;    // Rigidbody of the held object

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Left-click to pick up an object
        {
            TryPickUpObject();
        }

        if (Input.GetMouseButtonUp(0))  // Release the object when left-click is released
        {
            if (heldObject != null)
            {
                DropObject();
            }
        }

        if (heldObject != null)  // Move object to the hold position while it's picked up
        {
            MoveObjectToHoldPosition();
        }
    }

    void TryPickUpObject()
    {
        // Raycast from the camera to detect objects in front of the player
        RaycastHit hit;
        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, pickUpDistance))
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("PickUp"))  // Ensure the object is tagged as "PickUp"
            {
                PickUpObject(hit.collider.gameObject);  // Pick up the object if within distance
            }
        }
    }

    void PickUpObject(GameObject obj)
    {
        heldObject = obj;
        heldObjectRb = heldObject.GetComponent<Rigidbody>();

        if (heldObjectRb != null)
        {
            heldObjectRb.useGravity = false;     // Disable gravity so it floats
            heldObjectRb.drag = 10;              // Increase drag to make movement smoother
            heldObjectRb.constraints = RigidbodyConstraints.FreezeRotation;  // Prevent it from rotating
        }
    }

    void MoveObjectToHoldPosition()
    {
        // Calculate the direction from the object to the hold position
        Vector3 directionToHoldPosition = holdPosition.position - heldObject.transform.position;
        
        // Calculate the force to apply, which scales down as the object gets closer to the hold position
        float distance = directionToHoldPosition.magnitude;
        float scaledForce = moveForce * Mathf.Clamp(distance * maxDistanceMultiplier, 0.1f, 1f);

        // Apply force towards the hold position
        heldObjectRb.AddForce(directionToHoldPosition.normalized * scaledForce, ForceMode.Force);
    }

    void DropObject()
    {
        if (heldObjectRb != null)
        {
            heldObjectRb.useGravity = true;      // Re-enable gravity
            heldObjectRb.drag = 1;               // Reset drag to its original value
            heldObjectRb.constraints = RigidbodyConstraints.None;  // Remove any constraints
        }

        heldObject = null;  // Clear reference to the held object
    }
}
