using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speedDefault;
    public float drag;
    public Transform orientation;
    [SerializeField]
    private float speed;
    public Animator animator;
    public bool movementEnabled = true;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        speed = speedDefault;
    }

    void Update()
    {
        if (movementEnabled)
        {
            PlayerInput();
            SpeedControl();
            rb.linearDamping = drag;
        }
        else
        {
            rb.linearDamping = 0f; // Disable drag when movement is not enabled
            rb.linearVelocity = Vector3.zero; // Stop the player from moving
        }

    }
    void FixedUpdate()
    {
        if (!movementEnabled)
        {
            animator.SetFloat("speed", 0f);
            return; 
        }
        MovePlayer();
        animator.SetFloat("speed", rb.linearVelocity.magnitude);
    }

    void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }

    void MovePlayer()
    {
        // Płaska wersja forward i right (ignoruj Y)
        Vector3 flatForward = Vector3.ProjectOnPlane(orientation.forward, Vector3.up).normalized;
        Vector3 flatRight = Vector3.ProjectOnPlane(orientation.right, Vector3.up).normalized;

        moveDirection = flatForward * verticalInput + flatRight * horizontalInput;
        rb.AddForce(moveDirection * speed * 10f, ForceMode.Force);
    }
    void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (flatVelocity.magnitude > speed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * speed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z); 
        }
    }
}
