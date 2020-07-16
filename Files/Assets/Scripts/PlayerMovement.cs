using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 12f;
    public float SpeedinAir = 6f;
    public float gravity = -9.81f;
    public float grounddsitance = 0.4f;
    public float jumpheight = 3f;
    public float SprintSpeed = 16f;
    private float normalspeed = 12f;

    public Transform groundcheck;
    public LayerMask groundmask;
    public CharacterController controller;
    private bool isSneak = false;

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundcheck.position, grounddsitance, groundmask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (isGrounded)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * Speed * Time.deltaTime);

        }
        else
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * SpeedinAir * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.F))
        {
            Speed = SprintSpeed;
        }
        else
        {
            Speed = normalspeed;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isSneak)
            {
                transform.localScale = new Vector3(1f, 1.6f, 1f);
                isSneak = false;
            }
            else
            {
                transform.localScale = new Vector3(1f, 1.1f, 1f);
                isSneak = true;
            }
        }
    }
    private void Start()
    {
        normalspeed = Speed;
    }

}
