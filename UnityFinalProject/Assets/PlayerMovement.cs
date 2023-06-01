using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float Speed => speed;
    public float standingHeight;
    public float crouchingHeight;
    public float groundDrag;
    public LayerMask whatIsGrounded;
    public Transform Orientation;

    private bool grounded;
    private float currentHeight;
    private bool isCrouching = false;
    private CapsuleCollider playerCollider;

    private float hInput;
    private float vInput;
    private Vector3 direction;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentHeight = standingHeight;
        playerCollider = GetComponent<CapsuleCollider>();
        DetectPlayerHeight();
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, currentHeight * 0.5f + 0.2f, whatIsGrounded);

        PlayerInput();
        speedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleCrouch();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
    }

    private void Move()
    {
        direction = Orientation.forward * vInput + Orientation.right * hInput;
        rb.AddForce(direction.normalized * speed * 10f, ForceMode.Force);
    }

    private void speedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > speed)
        {
            Vector3 limited = flatVel.normalized * speed;
            rb.velocity = new Vector3(limited.x, rb.velocity.y, limited.z);
        }
    }

    private void Jump()
    {
            rb.AddForce(Vector3.up * 3f, ForceMode.Impulse);
    }

    private void ToggleCrouch()
    {
        if (isCrouching)
        {
            // uncrouch
            currentHeight = standingHeight;
            transform.position += Vector3.up * (standingHeight - crouchingHeight);
            playerCollider.height = standingHeight;
            isCrouching = false;
        }
        else
        {
            // crouch
            currentHeight = crouchingHeight;
            transform.position += Vector3.down * (standingHeight - crouchingHeight);
            playerCollider.height = crouchingHeight;
            isCrouching = true;
        }
    }

    private void DetectPlayerHeight()
    {
        // Detect the player's collider height
        playerCollider = GetComponent<CapsuleCollider>();
        float playerHeadHeight = playerCollider.height - playerCollider.center.y;

        // Set the standing and crouching heights
        standingHeight = playerHeadHeight + 0.2f;
        crouchingHeight = playerHeadHeight * 0.5f + 0.2f;

        // Set the initial height to standing height
        currentHeight = standingHeight;
    }

    public float getSpeed()
    {
        return speed;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
}