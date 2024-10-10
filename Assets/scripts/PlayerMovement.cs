using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 45f;
    public float sprintSpeed = 55f; // Fixed the missing semicolon
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Vector3 moveDir;
    public Transform orientation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
        moveDir = (orientation.right * Input.GetAxisRaw("Horizontal") + orientation.forward * Input.GetAxisRaw("Vertical")).normalized;

        
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        
        rb.AddForce(moveDir * 100 * speed * Time.fixedDeltaTime);
    }
}
