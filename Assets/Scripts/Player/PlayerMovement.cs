using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    float crouchTimer = 1;

    private bool isGrounded;

    bool crouching = false;
    bool lerpCrouch = false;
    bool sprinting = false;

    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float x = crouchTimer / 1;
            x *= x;
            if (crouching)
            {
                // lerp = linear interpolation (lineární interpolace = najde bod C mezi dvema body A a B, v nejake dobe t, rozsah je vetšinou t = 0 až 1)
                controller.height = Mathf.Lerp(controller.height, 1, x);
            }

            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, x);
            }

            if (x > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }
    
    //obdrží vstup z InputManager.cs a přiřadí je k character controlleru
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;

        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        controller.Move(playerVelocity * Time.deltaTime);

        //Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if(isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            speed = 8;
        }

        else
        {
            speed = 5;
        }
    }
}
