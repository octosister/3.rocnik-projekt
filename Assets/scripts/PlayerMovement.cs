using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 45f;
    public float sprintSpeed = 55f;
    public float crouchSpeed = 25f;
    public float crouchHeight = 0.5f;
    private float originalHeight;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Vector3 moveDir;
    public Transform orientation;
    public CapsuleCollider playerCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalHeight = playerCollider.height;
    }

    private void FixedUpdate()
    {
        moveDir = (orientation.right * Input.GetAxisRaw("Horizontal") + orientation.forward * Input.GetAxisRaw("Vertical")).normalized;

        float speed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
            playerCollider.height = crouchHeight;
        }
        else
        {
            playerCollider.height = originalHeight;
        }

        rb.AddForce(moveDir * 100 * speed * Time.fixedDeltaTime);
    }
}
