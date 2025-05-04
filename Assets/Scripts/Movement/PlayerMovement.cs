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

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Stamina")]
    public float staminaRegain = 1f;
    public float staminaLose = 5f;
    public float maxStamina = 100f;
    public float speedChange = 2f;
    [SerializeField]
    private float stamina;
    [SerializeField]
    private bool isSprinting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        stamina = maxStamina;
        speed = speedDefault;
    }

    void Update()
    {
        PlayerInput();
        SpeedControl();
        rb.linearDamping = drag;

        SprintControl();
        SprintSpeed();

    }
    void FixedUpdate()
    {
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
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
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
    void SprintControl()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina > 0)
            {
                isSprinting = true;
            }
            else
            {
                isSprinting = false;
            }
        }
        else
        {
            isSprinting = false;
        }
    }
    void SprintSpeed()
    {
        if (isSprinting)
        {
            speed = speedDefault * speedChange;
            stamina -= staminaLose * Time.deltaTime;
        }
        else
        {
            speed = speedDefault;
            if (stamina < maxStamina)
            {
                stamina += staminaRegain * Time.deltaTime;
            }
            else
            {
                stamina = maxStamina;
            }
        }
    }
    public float readStamina()
    {
        return stamina;
    }
}
