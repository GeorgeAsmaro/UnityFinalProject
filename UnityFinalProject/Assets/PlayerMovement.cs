using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    public Transform Orientation;

    public float height;
    public float groundDrag;

    public LayerMask whatIsGrounded;
    bool grounded;

    float hInput;
    float vInput;

    Vector3 direction;

    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update() {
        //Debug.Log(rb.velocity);
        grounded = Physics.Raycast(transform.position, Vector3.down, height * 0.5f + 0.2f, whatIsGrounded);

        PlayerInput();
        speedControl();

        if(grounded) {
            rb.drag = groundDrag; 
        }
        else {
            rb.drag = 0;
        }
    }

    void FixedUpdate() {
        Move();
    }

    void PlayerInput() {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
    }

    void Move() {
        direction = Orientation.forward * vInput + Orientation.right * hInput;
        rb.AddForce(direction.normalized * speed * 10f, ForceMode.Force);
    }

    void speedControl() {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > speed) {
            Vector3 limited = flatVel.normalized * speed;
            rb.velocity = new Vector3(limited.x, rb.velocity.y,limited.z);
        }

    }
}
