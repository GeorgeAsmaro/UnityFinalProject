using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        characterMovement();
        cameraControl();
        manageGravity();
    }

    public float speed = 6f;
    public CharacterController controller;

    public void characterMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);
    }


    [SerializeField] private float sens = 2;
    [SerializeField] private bool invertVertical;
    public Transform cam;
    private float xRotation;

    public void cameraControl()
    {
        float mouseX = Input.GetAxis("Mouse X") * sens;
        float mouseY = Input.GetAxis("Mouse Y") * sens;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);//clamp the vertical rotation

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);//rotates cam not player
        transform.Rotate(Vector3.up * mouseX);
    }

    [SerializeField] private float jumpHeight = 300;
    private float gravity = 9.81f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    private float groundDistance = 1.4f;
    private bool isGrounded;

    Vector3 gravityVelocity;

    public void manageGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);//check if we are grounded

        if (isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            gravityVelocity.y = Mathf.Sqrt(jumpHeight / (gravity * 2));
        }

        gravityVelocity.y -= gravity * Time.deltaTime;

        controller.Move(gravityVelocity * Time.deltaTime);
    }
}

