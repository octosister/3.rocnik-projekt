using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public Transform playerCam;        // Camera or reference point for where the object will float
    public Transform holdPosition;     // The position in front of the player where the object will float
    public float pickUpDistance = 1f;  // Max distance from which you can pick up objects (1 meter)
    public float moveSpeed = 10f;      // Speed at which the object moves towards the hold position

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
        // Move the object smoothly towards the hold position
        Vector3 moveDirection = holdPosition.position - heldObject.transform.position;
        heldObjectRb.velocity = moveDirection * moveSpeed;  // Move towards holdPosition using velocity
    }

    void DropObject()
    {
        if (heldObjectRb != null)
        {
            heldObjectRb.useGravity = true;      // Re-enable gravity
            heldObjectRb.drag = 1;               // Reset drag to its original value
            heldObjectRb.constraints = RigidbodyConstraints.None;  // Remove any constraints
            heldObjectRb.velocity = Vector3.zero;  // Stop any movement to avoid shooting the object away
        }

        heldObject = null;  // Clear reference to the held object
    }
}
